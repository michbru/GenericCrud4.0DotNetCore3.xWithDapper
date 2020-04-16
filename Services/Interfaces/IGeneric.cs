using GenericCrudApiDapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCrudApiDapper.Services.Interfaces
{
    public interface IGenericService
    {
        Task<IEnumerable<dynamic>> GetItemsAsync(string entName);
        Task<dynamic> GetItemsSQLAsync(APISend values);
        dynamic UpdateItem(APISendSave values);
        dynamic DeleteItem(APISendSave values);
    }
}
