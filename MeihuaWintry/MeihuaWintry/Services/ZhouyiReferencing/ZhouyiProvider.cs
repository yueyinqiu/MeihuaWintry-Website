using MeihuaWintry.Serialization;
using MeihuaWintry.Tools;
using System.Net.Http.Json;

namespace MeihuaWintry.Services.ZhouyiReferencing;

public sealed class ZhouyiProvider
{
    private readonly ValueTaskLazy<PreloadedZhouyiStore> zhouyiStore;
    public ZhouyiProvider(string baseAddress)
    {
        this.zhouyiStore = new ValueTaskLazy<PreloadedZhouyiStore>(async () =>
        {
            using var client = new HttpClient()
            {
                BaseAddress = new(baseAddress)
            };
            var location = await client.GetFromJsonAsync(
                "zhouyi-location.json",
                StringContext.Default.String);
            var result = await client.GetFromJsonAsync(
                location,
                ZhouyiStoreContext.Default.ZhouyiStore);
            return new PreloadedZhouyiStore(result ?? new(null));
        }, true);
    }

    public async ValueTask<PreloadedZhouyiStore> GetStoreAsync()
    {
        return await this.zhouyiStore.GetValueAsync();
    }
}
