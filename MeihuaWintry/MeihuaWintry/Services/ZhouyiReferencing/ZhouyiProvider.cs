using MeihuaWintry.Serialization;
using MeihuaWintry.Tools;
using System.Net.Http.Json;
using System.Text.Json;
using YiJingFramework.Annotating.Zhouyi;
using YiJingFramework.Annotating.Zhouyi.Entities;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount;

namespace MeihuaWintry.Services.ZhouyiReferencing;

public sealed class ZhouyiProvider
{
    private readonly ValueTaskLazy<ZhouyiStore> zhouyiStore;
    public ZhouyiProvider(string baseAddress)
    {
        this.zhouyiStore = new ValueTaskLazy<ZhouyiStore>(async () => {
            using var client = new HttpClient() {
                BaseAddress = new(baseAddress)
            };

            var result = await client.GetFromJsonAsync(
                "zhouyi.json",
                ZhouyiStoreContext.Default.ZhouyiStore);
            return result ?? throw new JsonException(
                "Failed to load the annotation store of Zhouyi.");
        }, true);
    }

    public async ValueTask<ZhouyiStore> GetStoreAsync()
    {
        return await this.zhouyiStore.GetValueAsync();
    }

    public async ValueTask<ZhouyiTrigram> GetTextsAsync(GuaTrigram trigram)
    {
        var store = await this.zhouyiStore.GetValueAsync();
        return store.GetTrigram(trigram);
    }

    public async ValueTask<ZhouyiHexagram> GetTextsAsync(GuaHexagram hexagram)
    {
        var store = await this.zhouyiStore.GetValueAsync();
        return store.GetHexagram(hexagram);
    }
}
