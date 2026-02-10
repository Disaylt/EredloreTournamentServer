using Application.Interfaces;

namespace Application.Helpers;

public class ContextStorage<T> : IContextStorage<T> where T : IContext
{
    private T? _value;

    public T? Get() => _value;

    public T GetRequired() => _value ?? throw new NullReferenceException();

    public void Set(T context)
    {
        if(_value is not null)
        {
            throw new ArgumentException("The value is already set");
        }

        _value = context;
    }
}
