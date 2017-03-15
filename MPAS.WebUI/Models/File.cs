
using MPAS.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using MPAS.Domain.Entities;

namespace ContosoUniversity.Models
{
    public class File 
    {
        public int FileId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileType FileType { get; set; }
        public int Id { get; set; }
        public virtual User User { get; set; }

        public virtual Professor Professor { get; set; }
    }
}