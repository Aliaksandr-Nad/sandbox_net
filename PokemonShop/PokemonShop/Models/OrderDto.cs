using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PokemonShop.Models
{
    public class OrderDto
    {
        [DisplayName("Guid")]
        public Guid Guid { get; set; } // = Guid.NewGuid();

        [DisplayName("Name")]
        public string Name { get; set; }
        
        [StringLength(50)]
        [Display(Name = "Email address")]
        public string Email { get; set; }
        
        [StringLength(50)]
        [Display(Name = "Telephone Number")]
        public string Telephone { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
    }
}