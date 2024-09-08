using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using YiJingFramework.Annotating.Zhouyi;
using YiJingFramework.Annotating.Zhouyi.Entities;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount;

var storeUri = "https://yueyinqiu.github.io/my-yijing-annotation-stores/975345ca/2023-08-02-1.json";
using var httpClient = new HttpClient();
var store = await httpClient.GetFromJsonAsync<ZhouyiStore>(storeUri);
Debug.Assert(store is not null);

store.Title = $"{store.Title} For MeihuaWintry-Website";

store.Tags.Clear();
store.Tags.Add(
    $"此注解仓库专门为 MeihuaWintry-Website 进行过调整，可能缺少部分信息。" +
    $"原仓库：{storeUri}");

for (int i = 0; i < 64; i++)
{
    var gua = GuaHexagram.Parse(Convert.ToString(i, 2).PadLeft(6, '0'));
    var hexagram = store.GetHexagram(gua);
    hexagram.Wenyan = null;
    store.UpdateStore(hexagram);
}

store.UpdateStore(new Shuogua());
store.UpdateStore(new Xugua());
store.UpdateStore(new Zagua());
store.UpdateStore(new Xici());

Directory.CreateDirectory("./out");
await File.WriteAllTextAsync($"./out/zhouyi.json",
    store.SerializeToJsonString(new JsonSerializerOptions()
    {
        WriteIndented = true
    }));
