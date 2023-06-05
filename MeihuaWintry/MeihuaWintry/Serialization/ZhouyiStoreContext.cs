using System.Text.Json.Serialization;
using YiJingFramework.Annotating.Zhouyi;

namespace MeihuaWintry.Serialization;

[JsonSerializable(typeof(ZhouyiStore))]
public partial class ZhouyiStoreContext : JsonSerializerContext
{
}