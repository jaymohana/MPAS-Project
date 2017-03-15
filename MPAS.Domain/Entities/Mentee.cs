using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MPAS.Domain.Entities
{
    public class Mentee
    {


        [Required]
        [Key]
        [Index(IsClustered = false)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public string name { get; set; }

        public virtual User User { get; set; }

    }
}
