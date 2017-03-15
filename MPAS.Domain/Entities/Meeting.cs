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
    public class Meeting
    {


        [Required]
        [Key]
        [Index(IsClustered = false)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        public string mentorName { get; set; }

        public string venueName { get; set; }
        public DateTime postDate { get; set; }
        public int available { get; set; }
        public string professorID { get; set; }

    }
}
