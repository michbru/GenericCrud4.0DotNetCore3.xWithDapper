using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCrudApiDapper
{
    public static class DbContextExtensions
    {


        public static dynamic GetEntityContextByEntityName(this DbContext _context, String table)
        {
            Type tableType = _context.GetType().Assembly.GetExportedTypes().FirstOrDefault(t => t.Name == table);
            var ObjectContext = _context.GetEntityContextByEntityType(tableType);
            return ObjectContext;
        }
        public static IQueryable<Object> GetEntityContextByEntityType(this DbContext _context, Type t)
        {
            return (IQueryable<Object>)_context.GetType().GetMethod("Set").MakeGenericMethod(t).Invoke(_context, null);
        }

        public static dynamic GetUpdateAddObject(dynamic updateRecord, dynamic entityStub)
        {

            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.FromObject(updateRecord, new Newtonsoft.Json.JsonSerializer { NullValueHandling = NullValueHandling.Ignore });
            System.Type returnType = entityStub.GetType();
            dynamic typedPopulatedObject = jObject.ToObject(returnType, new Newtonsoft.Json.JsonSerializer { NullValueHandling = NullValueHandling.Ignore });

            return typedPopulatedObject;
        }
    }
}
