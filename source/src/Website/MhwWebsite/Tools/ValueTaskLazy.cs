namespace MhwWebsite.Tools;

public sealed class ValueTaskLazy<T>
{
    private readonly Lazy<ValueTask<T>> lazy;

    public ValueTaskLazy(Func<ValueTask<T>> asyncFactory, bool startImmediately = false)
    {
        this.lazy = new Lazy<ValueTask<T>>(asyncFactory);
        if (startImmediately)
            _ = this.lazy.Value;
    }

    public bool IsValueCreated
    {
        get
        {
            if (!this.lazy.IsValueCreated)
                return false;
            return this.lazy.Value.IsCompleted;
        }
    }

    public ValueTask<T> Task
    {
        get
        {
            return this.lazy.Value;
        }
    }

    public ValueTask<T> GetValueAsync()
    {
        return this.lazy.Value;
    }
}
