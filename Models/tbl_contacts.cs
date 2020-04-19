using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCrudApiDapper.Models
{
    public class tbl_contacts
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string url { get; set; }
        public string notes { get; set; }
    }
}
