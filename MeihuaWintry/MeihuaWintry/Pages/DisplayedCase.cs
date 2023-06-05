using MeihuaWintry.Services.CaseStorage;
using System.Diagnostics;
using YiJingFramework.Annotating.Zhouyi;
using YiJingFramework.Annotating.Zhouyi.Entities;
using YiJingFramework.EntityRelationships.MostAccepted.GuaDerivingExtensions;
using YiJingFramework.PrimitiveTypes;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount;

namespace MeihuaWintry.Pages;

internal sealed class DisplayedCase
{
    private static GuaTrigram GetTrigram(int number)
    {
        number = (number % 8 + 8) % 8;
        var line1 = (Yinyang)(number % 2);
        number >>= 1;
        var line2 = (Yinyang)(number % 2);
        number >>= 1;
        var line3 = (Yinyang)(number % 2);
        Debug.Assert(number is 0 or 1);
        return new(line1, line2, line3);
    }

    public DisplayedCase(StoredCase c, ZhouyiStore zhouyi)
    {
        this.InnerCase = c;

        if (!c.Time.HasValue)
            c.Time = new DateTime().Ticks;
        this.WesternTime = new(c.Time.Value);

        if (this.WesternTime.Hour == 23)
            this.NongliTime = new(this.WesternTime.Add(new TimeSpan(1, 0, 0)));
        else
            this.NongliTime = new(this.WesternTime);

        if (!c.Lower.HasValue)
            c.Lower = 0;
        this.Lower = c.Lower.Value;

        if (!c.Upper.HasValue)
            c.Upper = 0;
        this.Upper = c.Upper.Value;

        if (!c.Line.HasValue)
            c.Line = 0;
        this.Lower = c.Line.Value;

        this.Original = zhouyi.GetHexagram(
            new(GetTrigram(this.Lower).Concat(GetTrigram(this.Upper))));
        this.Overlapping = zhouyi.GetHexagram(this.Original.Painting.Hugua());
        this.Changed = zhouyi.GetHexagram(this.Original.Painting.ReverseLines(this.Line));
    }
    public StoredCase InnerCase { get; }

    public string Name
    {
        get
        {
            return string.IsNullOrWhiteSpace(this.InnerCase.CaseName) ?
                "无名称占例" : this.InnerCase.CaseName;
        }
        set
        {
            this.InnerCase.CaseName = value.Trim();
        }
    }

    public DateTime WesternTime { get; }
    public string WesternTimeDisplay
    {
        get
        {
            return $"{this.WesternTime:yyyy-MM-dd HH:mm:ss}";
        }
    }
    public Lunar.Lunar NongliTime { get; }
    public string NongliLunarTimeDisplay
    {
        get
        {
            return $"{this.NongliTime.YearZhi}年" +
                $"{this.NongliTime.MonthInChinese}月" +
                $"{this.NongliTime.DayInChinese}" +
                $"{this.NongliTime.TimeZhi}时";
        }
    }
    public string NongliSolarTimeDisplay
    {
        get
        {
            return $"{this.NongliTime.YearInGanZhiByLiChun} " +
                $"{this.NongliTime.MonthInGanZhi} " +
                $"{this.NongliTime.DayInGanZhi} " +
                $"{this.NongliTime.TimeInGanZhi}";
        }
    }

    public int Upper { get; }
    public int Lower { get; }
    public int Line { get; }

    public ZhouyiHexagram Original { get; }
    public ZhouyiHexagram Overlapping { get; }
    public ZhouyiHexagram Changed { get; }

    public string Comment
    {
        get
        {
            return this.InnerCase.Comment ?? "";
        }
        set
        {
            this.InnerCase.Comment = value;
        }
    }
}
