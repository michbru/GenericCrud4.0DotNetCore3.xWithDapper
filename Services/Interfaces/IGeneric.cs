using GenericCrudApiDapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCrudApiDapper.Services.Interfaces
{
    public interface IGenericService
    {

        Task<IEnumerable<dynamic>> getItemsAsync(string entName);
        Task<dynamic> getItemsSQLAsync(APISend values);
        dynamic updateItem(APISendSave values);
        dynamic deleteItem(APISendSave values);


    }
}
