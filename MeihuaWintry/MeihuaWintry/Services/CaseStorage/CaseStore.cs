using Blazored.LocalStorage;

namespace MeihuaWintry.Services.CaseStorage;

public sealed class CaseStore
{
    private readonly ISyncLocalStorageService localStorage;
    public CaseStore(ISyncLocalStorageService localStorage)
    {
        this.localStorage = localStorage;
    }

    private const string storagePrefix = $"MeihuaWintry-Cases-";

    public IEnumerable<StoredCase> EnumrateCases()
    {
        foreach (var key in this.localStorage.Keys())
        {
            if (key.StartsWith(storagePrefix))
            {
                var item = this.localStorage.GetItem<StoredCase>(key);
                if (item.LastEditAuto.HasValue)
                    yield return item;
            }
        }
    }

    public StoredCase NewCase()
    {
        for (; ; )
        {
            var id = Guid.NewGuid();
            var key = $"{storagePrefix}{id:N}";
            if (this.localStorage.ContainKey(key))
                continue;

            var c = new StoredCase() {
                IdAuto = id
            };
            this.localStorage.SetItem(key, c);
            return c;
        }
    }

    public StoredCase? GetCase(Guid id)
    {
        var key = $"{storagePrefix}{id:N}";
        if (this.localStorage.ContainKey(key))
            return this.localStorage.GetItem<StoredCase>(key);
        else
            return null;
    }

    public void UpdateCase(StoredCase c)
    {
        var key = $"{storagePrefix}{c.IdAuto:N}";
        c.LastEditAuto = DateTime.Now.Ticks;
        this.localStorage.SetItem(key, c);
    }

    public void RemoveCase(StoredCase c)
    {
        var key = $"{storagePrefix}{c.IdAuto:N}";
        this.localStorage.RemoveItem(key);
    }
}
