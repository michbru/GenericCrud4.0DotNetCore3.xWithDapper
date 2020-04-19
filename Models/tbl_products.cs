using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace GenericCrudApiDapper.Models
{
    public class tbl_products  //: IValidatableObject
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        public decimal Price { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (Price < 5)
            {
                results.Add(new ValidationResult("Price must be greater than 5."));
            }
            if (Price > 100)
            {
                results.Add(new ValidationResult("Price cannot be greater than 100."));
            }

            return results;
        }

    }


}
