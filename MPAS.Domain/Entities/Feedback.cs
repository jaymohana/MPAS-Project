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
    public class Feedback
    {
        [Required]
        [Key]
        [Index(IsClustered = false)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required]

        public string MentorName { get; set; } 


        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; } 

        public string SessionNumber { get; set; } 

        public string MeetingLength { get; set; } 

        public string Venue { get; set; } 
   
        public string MenteeName { get; set; } 

        public string MeetingSummary { get; set; }

        public string WorriedStatus { get; set; }

        public string AdditionalInfo { get; set; }

        public virtual User User { get; set; }
    }
}
