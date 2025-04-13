using System.Data.Entity;
using EF6Test.Entities;

namespace EF6Test.Context
{
    public class BloggingContext : DbContext
    {
        public BloggingContext() : base("BloggingContext") { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().ToTable("Blogs", "dbo").HasKey(p => p.BlogId);
            modelBuilder.Entity<Blog>().Property(p => p.BlogId).HasColumnType("int").IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Blog>().Property(p => p.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Blog>().HasMany(p => p.Posts).WithRequired(x => x.Blog).HasForeignKey(x => x.BlogId);

            modelBuilder.Entity<Post>().ToTable("Posts", "dbo").HasKey(p => p.PostId);
            modelBuilder.Entity<Post>().Property(p => p.PostId).HasColumnType("int").IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Post>().Property(p => p.Title).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Post>().Property(p => p.Content).HasColumnType("varchar").HasMaxLength(4096).IsOptional();
            modelBuilder.Entity<Post>().Property(a => a.Role.Value).HasColumnType("int").IsRequired();

            modelBuilder.ComplexType<Role>().Ignore(r => r.Name);
        }
    }
}