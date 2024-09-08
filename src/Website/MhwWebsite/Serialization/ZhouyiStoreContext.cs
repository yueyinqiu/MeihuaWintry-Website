using System.Text.Json.Serialization;
using YiJingFramework.Annotating.Zhouyi;

namespace MhwWebsite.Serialization;

[JsonSerializable(typeof(ZhouyiStore))]
public partial class ZhouyiStoreContext : JsonSerializerContext
{
}