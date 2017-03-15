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
    public class MenteeFeedback
    {
        [Required]
        [Key]
        [Index(IsClustered = false)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        

        public string MenteeName { get; set; }

        public string MentorName { get; set; }

        public int Attendance { get; set; }

        public int Rating { get; set; }

        public string FeedbackMessage { get; set; }

        public virtual User User { get; set; }

    }
}
