using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCrudApiDapper.Models
{
    public class tblBook
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public string PublisherName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<int> Rating { get; set; }
        public string Language { get; set; }
        public Nullable<int> PublicationYear { get; set; }
        public string ISBN13 { get; set; }
        public string ISBN10 { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public string Details { get; set; }
    }
}
