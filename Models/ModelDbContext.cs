using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace tuoitrevanduc.Models
{
    public class ModelDbContext : DbContext
    {
        public ModelDbContext(DbContextOptions<ModelDbContext> options) : base(options)
        {
        }
        public DbSet<CategoryZenCourse> CategoryZenCourses { get; set; }
        public DbSet<ZenCourse> ZenCourses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<CategoryPost> CategoryPosts { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<MoveMethod> MoveMethods { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberJoin> MemberJoins { get; set; }
        public DbSet<MemberJoinFamily> MemberJoinFamilies { get; set; }
        public DbSet<Family> Families { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<MemberJoin>()
                .HasKey(mj => new { mj.MemberId, mj.ZenCourseId });

            modelBuilder.Entity<MemberJoinFamily>()
                .HasKey(mjf => new { mjf.MemberId, mjf.ZenCourseId, mjf.FamilyId });

            modelBuilder.Entity<MoveMethod>()
                .HasKey(mm => mm.Id);

            modelBuilder.Entity<CategoryZenCourse>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Family>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Post>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Slider>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<ZenCourse>()
                .HasKey(z => z.Id);

            // Thiết lập các quan hệ nếu cần (ví dụ Foreign Key)
            modelBuilder.Entity<Family>()
                .HasOne<ZenCourse>()
                .WithMany()
                .HasForeignKey(f => f.ZenCourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MemberJoin>()
                .HasOne<Member>()
                .WithMany()
                .HasForeignKey(mj => mj.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MemberJoin>()
                .HasOne<ZenCourse>()
                .WithMany()
                .HasForeignKey(mj => mj.ZenCourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MemberJoin>()
                .HasOne<MoveMethod>()
                .WithMany()
                .HasForeignKey(mj => mj.MoveMethodId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MemberJoinFamily>()
                .HasOne<Member>()
                .WithMany()
                .HasForeignKey(mjf => mjf.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MemberJoinFamily>()
                .HasOne<ZenCourse>()
                .WithMany()
                .HasForeignKey(mjf => mjf.ZenCourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MemberJoinFamily>()
                .HasOne<Family>()
                .WithMany()
                .HasForeignKey(mjf => mjf.FamilyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MoveMethod>()
                .HasOne<CategoryZenCourse>()
                .WithMany()
                .HasForeignKey(mm => mm.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Post>()
                .HasOne<CategoryZenCourse>()
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ZenCourse>()
                .HasOne<CategoryZenCourse>()
                .WithMany()
                .HasForeignKey(z => z.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); 
            
            modelBuilder.Entity<CategoryPost>()
                .HasKey(cp => cp.Id);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.CategoryPost)
                .WithMany(cp => cp.Posts)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }
    }
    public class Archive
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ContentPost { get; set; }
        public string? Link { get; set; }
        public bool? Enable { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateAt { get; set; }
    }
    public class CategoryPost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Thumbnail { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateBy { get; set; }
        public string? Slug { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
    public class CategoryZenCourse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
    public class Family
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? ZenCourseId { get; set; }
        // Nếu dùng Entity Framework và muốn cấu hình quan hệ:
        [ForeignKey("ZenCourseId")]
        public CategoryZenCourse? ZenCourse { get; set; }
    }
    public class Member
    {
        [Key]
        public int Id { get; set; }  // Không dùng Identity vì SQL không khai báo IDENTITY
        [MaxLength(10)]
        public string? Code { get; set; }
        [MaxLength(250)]
        public string? Name { get; set; }
        [MaxLength(250)]
        public string? OrtherName { get; set; }
        [MaxLength(10)]
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [MaxLength(250)]
        public string? Email { get; set; }
        [MaxLength(20)]
        public string? Phone { get; set; }
        [MaxLength(20)]
        public string? OrtherPhone { get; set; }
        [MaxLength(250)]
        public string? Address { get; set; }
        public DateTime? DateCreate { get; set; }
        [MaxLength(20)]
        public string? IdentityNumber { get; set; }
        [MaxLength(250)]
        public string? Facebook { get; set; }
        public string? Image { get; set; }
    }
    public class MemberJoin
    {
        public int MemberId { get; set; }
        public int ZenCourseId { get; set; }
        public bool? StatusConfirm { get; set; }
        public int? MoveMethodId { get; set; }
        public int? JoinWith { get; set; }
        public bool? CheckinStatus { get; set; }
        public bool? BorrowCloth { get; set; }
        public bool? StatusBorrow { get; set; }
        public string? ReceiveObject { get; set; }
        public bool? StatusReceive { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string CarNumber { get; set; } = null!;
        [Column(TypeName = "nvarchar(50)")]
        public string? PositionInCar { get; set; }
        public DateTime? CreateAt { get; set; }
        // Navigation (nếu dùng EF)
        public Member? Member { get; set; }
        public ZenCourse? ZenCourse { get; set; }
    }

    public class MemberJoinFamily
    {
        public int MemberId { get; set; }
        public int ZenCourseId { get; set; }
        public int FamilyId { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateAt { get; set; }
        // Navigation properties (nếu có thể liên kết)
        public Member? Member { get; set; }
        public ZenCourse? ZenCourse { get; set; }
        public Family? Family { get; set; }
    }
    public class MoveMethod
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        // Navigation property nếu có Category riêng
        // public Category? Category { get; set; }
    }
    public class Post
    {
        [Key]
        public int Id { get; set; }  // IDENTITY(1,1)
        public string? Name { get; set; }
        public string? ContentPost { get; set; }  // ntext → string
        public int? CreateBy { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CategoryId { get; set; }
        public string? Thumbnail { get; set; }
        public string? Slug { get; set; }
        public CategoryPost CategoryPost { get; set; }
    }
    public class Slider
    {
        [Key]
        public int Id { get; set; }  // IDENTITY(1,1)
        public string? Image { get; set; }  // nvarchar(max)
        public bool? Enable { get; set; }  // bit
        public int? Position { get; set; }  // int
        public DateTime? CreateAt { get; set; }  // datetime
        public int? CreateBy { get; set; }  // int
    }
    public class User
    {
        [Key]
        public int Id { get; set; }  // IDENTITY(1,1)
        public string? Name { get; set; }  // nvarchar(250)
        public string? OrtherName { get; set; }  // nvarchar(250)
        public string? Username { get; set; }  // nvarchar(250)
        public string? Password { get; set; }  // nvarchar(250)
        public string? Phone { get; set; }  // varchar(20)
        public string? Email { get; set; }  // nvarchar(250)
        public string? Address { get; set; }  // nvarchar(250)
        public bool? Enable { get; set; }  // bit
        public bool? AdminRole { get; set; }  // bit
    }

    public class ZenCourse
    {
        [Key]
        public int Id { get; set; }  // IDENTITY(1,1)
        public string? Name { get; set; }  // nvarchar(250)
        public string? Description { get; set; }  // nvarchar(250)
        public string? Address { get; set; }  // nvarchar(250)
        public string? Location { get; set; }  // nvarchar(250)
        public DateTime? Fromdate { get; set; }  // datetime
        public DateTime? Todate { get; set; }  // datetime
        public DateTime? TimeStart { get; set; }  // datetime
        public decimal? Price { get; set; }  // numeric(18, 0)
        public string? Poster { get; set; }  // nvarchar(max)
        public int? CategoryId { get; set; }  // int
        public DateTime? CreateAt { get; set; }  // datetime
        public int? CreateBy { get; set; }  // int
    }
}
