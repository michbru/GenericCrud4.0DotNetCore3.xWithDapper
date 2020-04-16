
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using GenericCrudApiDapper.Models;
using GenericCrudApiDapper.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace GenericCrudApiDapper.Services
{
    public class GenericService 
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;


        public GenericService(AppDbContext appDbContext, IConfiguration configuration)
        {
            this._appDbContext = appDbContext;
           this._configuration = configuration;
        }

        public async Task<IEnumerable<dynamic>> GetItemsAsync(string entName)
           //   public dynamic getItems(string entName)
        {

            var query = @"SELECT * from " + entName;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>(query).ConfigureAwait(false);
                return result;
            }
        }

        public async Task<dynamic> GetItemsSQLAsync(APISend values)

        {
            string _entity = values.p_entity;
            string _entity_sql = values.p_entity_sql;
            string _sqlWhere = values.p_sqlWhere;
            string _sqlOrder = values.p_sqlOrder;

            string sql;
            if (string.IsNullOrEmpty(_entity_sql) == true) { _entity_sql = _entity; };

            sql = "select * from " + _entity_sql + " where " + _sqlWhere + _sqlOrder;


            var query = sql;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>(query).ConfigureAwait(false);
                return result;

            }
        }

        public dynamic UpdateItem(APISendSave values)
        {

            var updateRecord = values.record;
            var updateRecordId = System.Convert.ToInt32(values.p_recId);

            string _entity = values.p_entity;

            var ctx = this._appDbContext;

            //get entity stub
            var assembly = Assembly.GetExecutingAssembly();
            Type type = ctx.GetType().Assembly.GetExportedTypes().FirstOrDefault(t => t.Name == _entity);
            var entityStub = Activator.CreateInstance(type);

            //map posted dynamic record to entity stub
            dynamic typedEntityRecord = DbContextExtensions.GetUpdateAddObject(updateRecord, entityStub);

            //refernce to dbcontext with entity
            var contextWithEntity = DbContextExtensions.GetEntityContextByEntityName(this._appDbContext, _entity);

            dynamic updateRecReturn;
            if (values.p_recId == "0")
            {
                updateRecReturn = contextWithEntity.Add(typedEntityRecord);

            }
            else
            {
                updateRecReturn = contextWithEntity.Update(typedEntityRecord);
            }

            ctx.SaveChanges();
            ctx.Dispose();

            var e = updateRecReturn.Entity;
            return e;


        }


        public dynamic DeleteItem(APISendSave values)
        {
            var updateRecord = values.record;

            string _entity = values.p_entity;

            var query = @"Delete from " + _entity + " where " + values.p_primeKey + " = " + values.p_recId;

            using (var connection = new SqlConnection("data source=s10.winhost.com;initial catalog=DB_14781_schools;persist security info=True;user id=DB_14781_schools_user;password=microwave1;MultipleActiveResultSets=True"))
            {
                connection.Open();
                var result =  connection.Query<dynamic>(query);
                return updateRecord;
            }
        }

    }
}
