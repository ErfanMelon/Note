using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyNoteApi.Models.Entities.Note;
using MyNoteApi.Models.Entities.User;

namespace MyNoteApi.Data;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public AppDbContext(DbContextOptions options) : base(options) { }
    public virtual DbSet<Memo> Memos { get; set; }
    public virtual DbSet<RequestOTP> RequestOTPs { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
}
