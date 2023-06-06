using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using YiJingFramework.Annotating.Zhouyi;
using YiJingFramework.Annotating.Zhouyi.Entities;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount;
using ZhouyiStoreCleaner;

const string defaultLink =
    "https://yueyinqiu.github.io/my-yijing-annotation-stores/a1a07175/2023-06-06-1.json";

Console.Write($"Enter the link of annotation file ({defaultLink} by default): ");
var link = Console.ReadLine();
if (string.IsNullOrWhiteSpace(link))
    link = defaultLink;

var uri = new Uri(link);

using var c = new HttpClient();
var store = await c.GetFromJsonAsync<ZhouyiStore>(uri);
Debug.Assert(store is not null);

store.Tags.Clear();
store.Tags.Add("此注解仓库专门为冬日梅花网页版进行过调整，可能缺少部分信息。");

store.UpdateStore(new Shuogua());
store.UpdateStore(new Xugua());
store.UpdateStore(new Zagua());
store.UpdateStore(new Xici());

foreach (var (k, i) in IndexesInChinese.Dictionary)
{
    var hexagram = store.GetHexagramByIndex(k);
    if (hexagram is null)
        continue;

    hexagram.Index = i.ToString();
    store.UpdateStore(hexagram);
}

for (var i = 0; i < 64; i++)
{
    var gua = GuaHexagram.Parse(Convert.ToString(i, 2).PadLeft(6, '0'));
    var hexagram = store.GetHexagram(gua);
    hexagram.Index = int.Parse(hexagram.Index!).ToString("00");
    hexagram.Wenyan = null;
    store.UpdateStore(hexagram);
}

using var output = new FileStream("out.json", FileMode.Create);
await JsonSerializer.SerializeAsync(output, store, new JsonSerializerOptions() {
    WriteIndented = true,
    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
});
