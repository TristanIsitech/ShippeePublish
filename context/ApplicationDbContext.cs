using Microsoft.EntityFrameworkCore;

namespace ShippeeAPI.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users  { get; set; }
        public DbSet<Company> Companies  { get; set; } 
        public DbSet<Naf_Section> Naf_Sections  { get; set; } 
        public DbSet<Naf_Division> Naf_Divisions  { get; set; } 
        public DbSet<Skill> Skills  { get; set; } 
        public DbSet<Student_Skill> Student_Skills  { get; set; } 
        public DbSet<Type_User> Type_Users  { get; set; } 
        public DbSet<Effective> Effectives  { get; set; } 
        public DbSet<Diplome> Diplomes { get; set; } 
        public DbSet<Annoucement_State> Annoucement_Status  { get; set; } 
        public DbSet<Annoucement> Annoucements { get; set; } 
        public DbSet<Qualification> Qualifications { get; set; } 
        public DbSet<Favorite> Favorites { get; set; } 
        public DbSet<Recent> Recents { get; set; } 
        public DbSet<Chat_State> Chat_Status { get; set; } 
        public DbSet<Chat> Chats { get; set; } 
        public DbSet<Recent_Search> Recents_Searches { get; set; } 

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student_Skill>()
                .HasKey(x => new { x.id_user, x.id_skill });

            modelBuilder.Entity<Student_Skill>()
                .HasOne(x => x.User)
                .WithMany(x => x.skills)
                .HasForeignKey(x => x.id_user);

            modelBuilder.Entity<Student_Skill>()
                .HasOne(x => x.Skill)
                .WithMany(x => x.students)
                .HasForeignKey(x => x.id_skill);



            modelBuilder.Entity<Qualification>()
                .HasKey(x => new { x.id_annoucement, x.id_skill });

            modelBuilder.Entity<Qualification>()
                .HasOne(x => x.Annoucement)
                .WithMany(x => x.skills)
                .HasForeignKey(x => x.id_annoucement);

            modelBuilder.Entity<Qualification>()
                .HasOne(x => x.Skill)
                .WithMany(x => x.annoucements)
                .HasForeignKey(x => x.id_skill);



            modelBuilder.Entity<Favorite>()
                .HasKey(x => new { x.id_user, x.id_annoucement });

            modelBuilder.Entity<Favorite>()
                .HasOne(x => x.User)
                .WithMany(x => x.favorites_annoucements)
                .HasForeignKey(x => x.id_user);

            modelBuilder.Entity<Favorite>()
                .HasOne(x => x.Annoucement)
                .WithMany(x => x.favorites_users)
                .HasForeignKey(x => x.id_annoucement);



            modelBuilder.Entity<Recent>()
                .HasKey(x => new { x.id_user, x.id_annoucement });

            modelBuilder.Entity<Recent>()
                .HasOne(x => x.User)
                .WithMany(x => x.recents_annoucements)
                .HasForeignKey(x => x.id_user);

            modelBuilder.Entity<Recent>()
                .HasOne(x => x.Annoucement)
                .WithMany(x => x.recents_visits)
                .HasForeignKey(x => x.id_annoucement);
        }
    }
}