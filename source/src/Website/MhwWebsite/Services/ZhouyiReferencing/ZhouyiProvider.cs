using MhwWebsite.Serialization;
using MhwWebsite.Tools;
using System.Net.Http.Json;

namespace MhwWebsite.Services.ZhouyiReferencing;

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
            var location = await client.GetStringAsync("zhouyi-location.json");
            var result = await client.GetFromJsonAsync(location,
                ZhouyiStoreContext.Default.ZhouyiStore);
            return new PreloadedZhouyiStore(result ?? new(null));
        }, true);
    }

    public async ValueTask<PreloadedZhouyiStore> GetStoreAsync()
    {
        return await this.zhouyiStore.GetValueAsync();
    }
}
