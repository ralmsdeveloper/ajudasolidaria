using System;

namespace AjudaSolidaria.Core.Services.Interfaces
{
    public interface IServiceBase<T>
    {
        T GetByKey(Guid key);
        void Add(T entity);
    }
}
