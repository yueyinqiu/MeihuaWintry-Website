using System.Text.Json.Serialization;
using YiJingFramework.PrimitiveTypes;

namespace MeihuaWintry.Services.CaseStorage;

public sealed class SolarTime
{
    public Tiangan YearGan { get; set; }
    public Dizhi YearZhi { get; set; }

    public Tiangan MonthGan { get; set; }
    public Dizhi MonthZhi { get; set; }

    public Tiangan DayGan { get; set; }
    public Dizhi DayZhi { get; set; }

    public Tiangan TimeGan { get; set; }
    public Dizhi TimeZhi { get; set; }
}
