using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IContextStorage<T> where T : IContext
{
    void Set(T context);
    T? Get();
    T GetRequired();
}
