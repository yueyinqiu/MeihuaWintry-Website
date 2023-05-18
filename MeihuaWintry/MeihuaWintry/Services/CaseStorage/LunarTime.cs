using System.Text.Json.Serialization;
using YiJingFramework.PrimitiveTypes;

namespace MeihuaWintry.Services.CaseStorage;

public sealed class LunarTime
{
    // Do not remove JsonIgnore attributes like this.
    // Null value of nullable valuetypes cannot be correctly deserialized.
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dizhi? Year { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Month { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Day { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dizhi? Time { get; set; }
}
