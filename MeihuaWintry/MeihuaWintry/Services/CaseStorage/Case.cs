using System.Text.Json.Serialization;
using YiJingFramework.PrimitiveTypes;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount;

namespace MeihuaWintry.Services.CaseStorage;

public sealed class Case
{
    public Guid IdAuto { get; set; }
    public DateTime LastEditAuto { get; set; }

    public string? CaseName { get; set; }

    public DateTime WesternTime { get; set; }
    public SolarTime? SolarTime { get; set; }
    public SolarTime? LunarTime { get; set; }

    public string? UpperExpression { get; set; }
    public string? LowerExpression { get; set; } 
    public string? LineExpression { get; set; }

    public GuaTrigram? Upper { get; set; }
    public GuaTrigram? Lower { get; set; }
    public int Line { get; set; }

    public string? Comment { get; set; }
}
