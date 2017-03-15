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
    public class Professor
    {
        //Index id, please check method
        //Provides a unique id automatically increment by one.
         [Required]
         [Key]
         [Index(IsClustered =false)]
         [HiddenInput(DisplayValue=false)]
         public int Id { get; set; }
         [Required]
         [ForeignKey("User")]

      //  [Required]
       // [Key]
       // [Index(IsClustered = false)]
       // [HiddenInput(DisplayValue = false)]
       // public int Id { get; set; }


        public string UserName { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string CategoryID { get; set; }

        public string Email { get; set; }

        public virtual User User { get; set; }
    }
}
