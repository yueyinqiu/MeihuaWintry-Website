using MhwWebsite.Services.CaseStorage;
using MhwWebsite.Services.ZhouyiReferencing;
using YiJingFramework.Annotating.Zhouyi.Entities;
using YiJingFramework.EntityRelations.GuaDerivations.Extensions;
using YiJingFramework.PrimitiveTypes;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount;

namespace MhwWebsite.Pages;

internal sealed class DisplayedCase
{
    private static GuaTrigram GetTrigram(int number)
    {
        return ((number % 8 + 8) % 8) switch
        {
            1 => new(Yinyang.Yang, Yinyang.Yang, Yinyang.Yang),
            2 => new(Yinyang.Yang, Yinyang.Yang, Yinyang.Yin),
            3 => new(Yinyang.Yang, Yinyang.Yin, Yinyang.Yang),
            4 => new(Yinyang.Yang, Yinyang.Yin, Yinyang.Yin),
            5 => new(Yinyang.Yin, Yinyang.Yang, Yinyang.Yang),
            6 => new(Yinyang.Yin, Yinyang.Yang, Yinyang.Yin),
            7 => new(Yinyang.Yin, Yinyang.Yin, Yinyang.Yang),
            _ => new(Yinyang.Yin, Yinyang.Yin, Yinyang.Yin),
        };
    }

    public DisplayedCase(StoredCase c, PreloadedZhouyiStore zhouyi)
    {
        this.InnerCase = c;

        if (!c.Time.HasValue)
            c.Time = new DateTime().Ticks;
        this.WesternTime = new(c.Time.Value);

        if (!c.Lower.HasValue)
            c.Lower = 0;
        this.Lower = c.Lower.Value;

        if (!c.Upper.HasValue)
            c.Upper = 0;
        this.Upper = c.Upper.Value;

        if (!c.Line.HasValue)
            c.Line = 0;
        this.Line = c.Line.Value;

        this.Original = zhouyi[new GuaHexagram(GetTrigram(this.Lower).Concat(GetTrigram(this.Upper)))];
        this.Overlapping = zhouyi[this.Original.Painting.Hugua()];

        var changingLine = (this.Line % 6 + 6 - 1) % 6;
        this.Changed = zhouyi[this.Original.Painting.ChangeYaos(changingLine)];
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
            return $"起卦时间：公历{this.WesternTime:yyyy年M月d日H时mm分}";
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
