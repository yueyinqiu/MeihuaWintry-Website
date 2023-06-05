using DynamicExpresso;
using LunarCsharpYiJingFrameworkExtensions;
using System.Diagnostics;
using System.Text;
using YiJingFramework.EntityRelationships.MostAccepted.GuaDerivingExtensions;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount.Extensions;

namespace MeihuaWintry.Pages;

public partial class CaseCreatePage
{
    private bool dateError = false;
    private DateTime? date;

    private DateTime? Date
    {
        get
        {
            return this.date;
        }
        set
        {
            this.dateError = false;
            this.date = value;
        }
    }

    private bool timeError = false;
    private TimeSpan? time;

    private TimeSpan? Time
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
        var dateTime = DateTime.Now;
        this.date = dateTime.Date;
        this.time = dateTime.TimeOfDay;
    }

    private async Task Submit()
    {
        this.dateError = this.Date is null;
        this.timeError = this.Time is null;
        if (this.dateError || this.timeError)
            return;
        Debug.Assert(this.Date is not null);
        Debug.Assert(this.Time is not null);

        var dateTime = this.Date.Value.Add(this.Time.Value);
        var nongliTime = new Lunar.Lunar(
            dateTime.Hour == 23 ? dateTime.Add(new TimeSpan(1, 0, 0)) : dateTime);

        var interpreter = new Interpreter();
        interpreter.SetDefaultNumberType(DefaultNumberType.Decimal);

        interpreter.SetVariable("年", (decimal)nongliTime.YearZhi().Index);
        interpreter.SetVariable("月", (decimal)nongliTime.Month);
        interpreter.SetVariable("日", (decimal)nongliTime.Day);
        interpreter.SetVariable("时", (decimal)nongliTime.TimeZhi().Index);

        int upper, lower, line;
        try
        {
            upper = Convert.ToInt32(interpreter.Eval(this.Upper));
            interpreter.SetVariable("上", (decimal)upper);
        }
        catch
        {
            this.upperError = true;
            return;
        }

        try
        {
            lower = Convert.ToInt32(interpreter.Eval(this.Lower));
            interpreter.SetVariable("下", (decimal)lower);
        }
        catch
        {
            this.lowerError = true;
            return;
        }

        try
        {
            line = Convert.ToInt32(interpreter.Eval(this.Line));
            interpreter.SetVariable("爻", (decimal)line);
        }
        catch
        {
            this.lineError = true;
            return;
        }

        var c = this.CaseStore.NewCase();
        c.Time = dateTime;
        c.CaseName = $"新占例 - {c.IdAuto.ToString("N")[0..8]}";
        c.Upper = upper;
        c.Lower = lower;
        c.Line = line;

        var displayedCase = new DisplayedCase(c, await this.ZhouyiProvider.GetStoreAsync());

        var overlappings = displayedCase.Overlapping.SplitToTrigrams();
        var overlappingUpperZhouyi = await this.ZhouyiProvider.GetTextsAsync(overlappings.upper);
        var overlappingLowerZhouyi = await this.ZhouyiProvider.GetTextsAsync(overlappings.lower);

        c.Comment = new StringBuilder()
            .Append('于')
            .Append(nongliTime.YearZhi)
            .Append('年')
            .Append(nongliTime.MonthInChinese)
            .Append('月')
            .Append(nongliTime.DayInChinese)
            .Append(nongliTime.TimeZhi)
            .Append("时起得此卦。时")
            .Append(dateTime.Year)
            .Append(" 年 ")
            .Append(dateTime.Month)
            .Append(" 月 ")
            .Append(dateTime.Day)
            .Append(" 日 ")
            .Append(dateTime.Hour)
            .Append(" 时 ")
            .Append(dateTime.Minute)
            .Append("分，又纪")
            .Append(nongliTime.YearInGanZhiByLiChun)
            .Append(nongliTime.MonthInGanZhi)
            .Append(nongliTime.DayInGanZhi)
            .Append(nongliTime.TimeInGanZhi)
            .Append('。')
            .AppendLine()
            .Append("其以 ")
            .Append(this.Upper)
            .Append(" 为 ")
            .Append(upper)
            .Append(" 作上卦，以 ")
            .Append(this.Lower)
            .Append(" 为 ")
            .Append(lower)
            .Append(" 作下卦，又以 ")
            .Append(this.Line)
            .Append(" 为 ")
            .Append(line)
            .Append(" 作动爻，得")
            .Append(displayedCase.Original.Name)
            .Append('之')
            .Append(displayedCase.Changed.Name)
            .Append('互')
            .Append(overlappings.upper == overlappings.lower ? "重" : overlappingUpperZhouyi.Name)
            .Append(overlappingLowerZhouyi.Name)
            .Append('。')
            .AppendLine()
            .ToString();
        this.CaseStore.UpdateCase(c);

        this.NavigationManager.NavigateTo($"/cases/{c.IdAuto:N}");
    }
}