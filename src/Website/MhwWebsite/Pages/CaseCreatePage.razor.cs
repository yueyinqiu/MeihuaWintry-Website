using DynamicExpresso;
using System.Diagnostics;
using System.Text;
using YiJingFramework.Nongli.Extensions;
using YiJingFramework.Nongli.Lunar;
using YiJingFramework.Nongli.Solar;

namespace MhwWebsite.Pages;

public partial class CaseCreatePage
{
    private bool timeError = false;
    private DateTime? time;

    private DateTime? Time
    {
        get
        {
            return this.time;
        }
        set
        {
            this.timeError = false;
            this.time = value;
        }
    }

    private string upper = "年+月+日";
    private bool upperError = false;
    private string Upper
    {
        get
        {
            return this.upper;
        }
        set
        {
            this.upperError = false;
            this.upper = value;
        }
    }

    private string lower = "上+时";
    private bool lowerError = false;
    private string Lower
    {
        get
        {
            return this.lower;
        }
        set
        {
            this.lowerError = false;
            this.lower = value;
        }
    }

    private string line = "下";
    private bool lineError = false;
    private string Line
    {
        get
        {
            return this.line;
        }
        set
        {
            this.lineError = false;
            this.line = value;
        }
    }

    protected override void OnInitialized()
    {
        var currentTime = DateTime.Now;
        this.time = new DateTime(currentTime.Year, currentTime.Month,
            currentTime.Day, currentTime.Hour, currentTime.Minute, 0);
    }

    private async Task Submit()
    {
        this.timeError = this.Time is null;
        if (this.timeError)
            return;
        Debug.Assert(this.Time is not null);

        var dateTime = this.Time.Value;
        var year = dateTime.Year;
        if (year <= LunarNian.MinSupportedNian.Year ||
            year <= SolarNian.MinSupportedNian.Year ||
            year >= LunarNian.MaxSupportedNian.Year ||
            year >= SolarNian.MaxSupportedNian.Year)
        {
            this.timeError = true;
            return;
        }
        
        var lunar = LunarDateTime.FromGregorian(dateTime);
        var solar = SolarDateTime.FromGregorian(dateTime);

        var interpreter = new Interpreter();
        _ = interpreter.SetDefaultNumberType(DefaultNumberType.Decimal);

        _ = interpreter.SetVariable("年", (decimal)lunar.Nian.Dizhi.Index);
        _ = interpreter.SetVariable("月", (decimal)lunar.Yue);
        _ = interpreter.SetVariable("日", (decimal)lunar.Ri);
        _ = interpreter.SetVariable("时", (decimal)lunar.Shi.Index);

        int upper, lower, line;
        try
        {
            upper = Convert.ToInt32(interpreter.Eval(this.Upper));
            _ = interpreter.SetVariable("上", (decimal)upper);
        }
        catch
        {
            this.upperError = true;
            return;
        }

        try
        {
            lower = Convert.ToInt32(interpreter.Eval(this.Lower));
            _ = interpreter.SetVariable("下", (decimal)lower);
        }
        catch
        {
            this.lowerError = true;
            return;
        }

        try
        {
            line = Convert.ToInt32(interpreter.Eval(this.Line));
            _ = interpreter.SetVariable("爻", (decimal)line);
        }
        catch
        {
            this.lineError = true;
            return;
        }

        var c = this.CaseStore.NewCase();
        c.Time = dateTime.Ticks;
        c.CaseName = $"新占例 - {c.IdAuto.ToString("N")[0..8]}";
        c.Upper = upper;
        c.Lower = lower;
        c.Line = line;

        var zhouyi = await this.ZhouyiProvider.GetStoreAsync();
        var displayedCase = new DisplayedCase(c, zhouyi);

        var overlappings = displayedCase.Overlapping.SplitToTrigrams();
        var overlappingUpperZhouyi = zhouyi[overlappings.upper];
        var overlappingLowerZhouyi = zhouyi[overlappings.lower];

        c.Comment = new StringBuilder()
            .Append('于')
            .Append(lunar.Nian.ToString("C"))
            .Append('年')
            .Append(lunar.YueInChinese())
            .Append('月')
            .Append(lunar.RiInChinese())
            .Append(lunar.Shi.ToString("C"))
            .Append(dateTime.ToString("时起得此卦。时yyyy年M月d日H时mm分，又纪"))
            .Append(solar.Nian.ToString("C"))
            .Append('年')
            .Append(solar.Yue.ToString("C"))
            .Append('月')
            .Append(solar.Ri.ToString("C"))
            .Append('日')
            .Append(solar.Shi.ToString("C"))
            .AppendLine("时。")
            .Append("其以“")
            .Append(this.Upper)
            .Append("”为")
            .Append(upper)
            .Append("数作上卦，以“")
            .Append(this.Lower)
            .Append("”为")
            .Append(lower)
            .Append("数作下卦，又以“")
            .Append(this.Line)
            .Append("”为")
            .Append(line)
            .Append("数作动爻。得")
            .Append(displayedCase.Original.Name)
            .Append('之')
            .Append(displayedCase.Changed.Name)
            .Append("，互")
            .Append(overlappings.upper == overlappings.lower ? "重" : overlappingUpperZhouyi.Name)
            .Append(overlappingLowerZhouyi.Name)
            .Append('。')
            .AppendLine()
            .ToString();
        this.CaseStore.UpdateCase(c);

        this.NavigationManager.NavigateTo($"/cases/{c.IdAuto:N}");
    }
}