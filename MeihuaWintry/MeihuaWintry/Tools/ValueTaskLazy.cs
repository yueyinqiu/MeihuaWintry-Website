using System;
using System.Threading.Tasks;

namespace MeihuaWintry.Tools;

public sealed class ValueTaskLazy<T>
{
    private readonly Lazy<ValueTask<T>> lazy;

    public ValueTaskLazy(Func<ValueTask<T>> asyncFactory)
    {
        lazy = new Lazy<ValueTask<T>>(asyncFactory);
    }

    public bool IsValueCreated
    {
        get
        {
            if (!lazy.IsValueCreated)
                return false;
            return lazy.Value.IsCompleted;
        }
    }

    public ValueTask<T> Task
    {
        get
        {
            return lazy.Value;
        }
    }

    public ValueTask<T> GetValueAsync()
    {
        return lazy.Value;
    }
}
