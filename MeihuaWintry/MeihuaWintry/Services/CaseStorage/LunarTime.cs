using System.Text.Json.Serialization;
using YiJingFramework.PrimitiveTypes;

namespace MeihuaWintry.Services.CaseStorage;

public sealed class LunarTime
{
    public Dizhi Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public Dizhi Time { get; set; }
}
