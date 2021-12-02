using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        [Column(TypeName ="varchar(50)")]
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        // navigation property
        public Category Category { get; set; }

    }
}
