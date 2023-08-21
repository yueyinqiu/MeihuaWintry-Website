using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using YiJingFramework.Annotating.Zhouyi;
using YiJingFramework.Annotating.Zhouyi.Entities;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount;

const string defaultLink =
    "https://yueyinqiu.github.io/my-yijing-annotation-stores/975345ca/2023-08-02-1.json";

Console.Write($"Enter the link of annotation file ({defaultLink} by default): ");
var link = Console.ReadLine();
if (string.IsNullOrWhiteSpace(link))
    link = defaultLink;

using var httpClient = new HttpClient();
var store = await httpClient.GetFromJsonAsync<ZhouyiStore>(link);
Debug.Assert(store is not null);

store.Title = $"{store.Title} For MeihuaWintry Website";

store.Tags.Clear();
store.Tags.Add(
    $"此注解仓库专门为冬日梅花网页版进行过调整，可能缺少部分信息。" +
    $"原仓库：{link}");

store.UpdateStore(new Shuogua());
store.UpdateStore(new Xugua());
store.UpdateStore(new Zagua());
store.UpdateStore(new Xici());

/*
foreach (var (k, i) in IndexesInChinese.Dictionary)
{
    var hexagram = store.GetHexagramByIndex(k);
    if (hexagram is null)
        continue;

    hexagram.Index = i.ToString();
    store.UpdateStore(hexagram);
}
*/

for (var i = 0; i < 64; i++)
{
    var gua = GuaHexagram.Parse(Convert.ToString(i, 2).PadLeft(6, '0'));
    var hexagram = store.GetHexagram(gua);
    // hexagram.Index = int.Parse(hexagram.Index!).ToString("00");
    hexagram.Wenyan = null;
    store.UpdateStore(hexagram);
}

Directory.CreateDirectory("./out");
var storeName = $"zhouyi-{DateTime.Now:yyyy-MM-dd}.json";
await File.WriteAllTextAsync(
    $"./out/{storeName}",
    store.SerializeToJsonString());
await File.WriteAllTextAsync(
    $"./out/zhouyi-location.json",
    JsonSerializer.Serialize(storeName));
