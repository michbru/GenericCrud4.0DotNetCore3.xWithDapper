using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using GenericCrudApiDapper.Services;
using GenericCrudApiDapper.Models;

namespace GenericCrudApiDapper.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("foo")]
    public class GenericController : ControllerBase
    {
        private readonly GenericService _genericService;

        public GenericController(GenericService genericService) // IConfiguration configuration, 
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<IEnumerable<dynamic>> GetItems(string entName)
        {
            var gen = await _genericService.GetItemsAsync(entName).
                ConfigureAwait(false);
            return gen;

        }
        [HttpPost]
        public async Task<IEnumerable<dynamic>> GetItemsSQL(APISend data)
        {
            var gen = await _genericService.GetItemsSQLAsync(data).ConfigureAwait(false);
            return gen;

        }

        [HttpPost]
        public dynamic UpdateItem(APISendSave data)
        {

            var gen = _genericService.UpdateItem(data);
            return gen; 
  
        }

        [HttpPost]
        public dynamic DeleteItem(APISendSave data)
        {
            var gen = _genericService.DeleteItem(data);
            return gen;

        }

    }
}