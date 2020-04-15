using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCrudApiDapper.Models
{
    public class APISendSave
    {
        public string p_entity { get; set; }
        public string p_recId { get; set; }
        public string p_primeKey { get; set; }
        public object record { get; set; }
    }
    public class APISend
    {

        public string p_entity { get; set; }
        public string p_entity_sql { get; set; }
        public string p_sqlWhere { get; set; }
        public string p_sqlOrder { get; set; }
        public string p_recId { get; set; }
        public dynamic record { get; set; }
    }
}
