using Blazored.LocalStorage;
using MeihuaWintry.Tools;
using Microsoft.JSInterop;
using System.Security.Cryptography;

namespace MeihuaWintry.Services.CaseStorage;

public sealed class CaseStore
{
    private readonly ISyncLocalStorageService localStorage;
    public CaseStore(ISyncLocalStorageService localStorage)
    {
        this.localStorage = localStorage;
    }

    private const string storagePrefix = $"MeihuaWintry-Cases-";

    public IEnumerable<Case> EnumrateCases()
    {
        foreach(var key in localStorage.Keys())
        {
            if(key.StartsWith(storagePrefix))
            {
                yield return localStorage.GetItem<Case>(key);
            }
        }
    }

    public Case NewCase()
    {
        for (; ; )
        {
            var id = Guid.NewGuid();
            var key = $"{storagePrefix}{id:N}";
            if (localStorage.ContainKey(key))
                continue;

            var c = new Case() {
                IdAuto = id,
                LastEditAuto = DateTime.Now
            };
            localStorage.SetItem(key, c);
        }
    }

    public void UpdateCase(Case c)
    {
        var key = $"{storagePrefix}{c.IdAuto:N}";
        localStorage.SetItem(key, c);
    }

    public void RemoveCase(Case c)
    {
        var key = $"{storagePrefix}{c.IdAuto:N}";
        localStorage.RemoveItem(key);
    }
}
