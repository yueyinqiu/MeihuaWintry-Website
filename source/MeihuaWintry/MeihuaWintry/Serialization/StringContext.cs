using System.Text.Json.Serialization;
using YiJingFramework.Annotating.Zhouyi;

namespace MeihuaWintry.Serialization;

[JsonSerializable(typeof(string))]
public partial class StringContext : JsonSerializerContext
{
}