using Eafit.MarcosYPatrones.Cqrs.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application
{
    public interface IListRepository<TEnum> where TEnum : struct, IConvertible
    {
        Task<IEnumerable<ListItem>> GetAllAsync();
    }
}
