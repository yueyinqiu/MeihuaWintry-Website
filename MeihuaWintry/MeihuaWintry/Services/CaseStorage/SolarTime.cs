using System.Text.Json.Serialization;
using YiJingFramework.PrimitiveTypes;

namespace MeihuaWintry.Services.CaseStorage;

public sealed class SolarTime
{
    // Do not remove JsonIgnore attributes like this.
    // Null value of nullable valuetypes cannot be correctly deserialized.
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Tiangan? YearGan { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dizhi? YearZhi { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Tiangan? MonthGan { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dizhi? MonthZhi { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Tiangan? DayGan { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dizhi? DayZhi { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Tiangan? TimeGan { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dizhi? TimeZhi { get; set; }
}
