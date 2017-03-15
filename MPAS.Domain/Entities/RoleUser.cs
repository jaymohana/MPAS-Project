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
    public class RoleUser
    {


        [Required]
        [Key]
        [Index(IsClustered = false)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }

        public string Role { get; set; }
        

    }
}
