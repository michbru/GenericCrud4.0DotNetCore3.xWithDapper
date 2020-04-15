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
        public async Task<IEnumerable<dynamic>> getItems(string entName)
        {
            var gen = await _genericService.getItemsAsync(entName).ConfigureAwait(false);
            return gen;

        }
        [HttpPost]
        public async Task<IEnumerable<dynamic>> getItemsSQL(APISend data)
        {
            var gen = await _genericService.getItemsSQLAsync(data).ConfigureAwait(false);
            return gen;

        }

        [HttpPost]
        public dynamic updateItem(APISendSave data)
        {

            var gen = _genericService.updateItem(data);
            return gen; 
  
        }

        [HttpPost]
        public dynamic deleteItem(APISendSave data)
        {
            var gen = _genericService.deleteItem(data);
            return gen;

        }

    }
}