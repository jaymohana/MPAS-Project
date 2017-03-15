using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MPAS.Domain.Entities;

namespace MPAS.Domain.Context
{
    public class EFDbContext : IdentityDbContext<User>
    {
        public EFDbContext()
            : base("EFDbContext", throwIfV1Schema: false)
        {
        }

        public static EFDbContext Create()
        {
            return new EFDbContext();
        }

        //public DbSet<Entity> Entities { get; set; }
        public DbSet<Professor> Professors { get; set; }

        public DbSet<Mentee> Mentees { get; set; }

        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<MenteeFeedback> MenteeFeedbacks { get; set; }

        public DbSet<Meeting> Meetings { get; set; }

        public DbSet<Chatroom> Chatroom { get; set; }

        public DbSet<RoleUser> RoleUsers { get; set; }

    }
}
