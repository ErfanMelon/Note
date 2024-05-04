using Microsoft.AspNetCore.Identity;
using MyNoteApi.Data;
using MyNoteApi.Models.Entities.User;
using MyNoteApi.Repositories.Interfaces.Email;
using MyNoteApi.Repositories.Interfaces.User;

namespace MyNoteApi.Repositories.Services.User;

public partial class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly AppDbContext _context;

    public UserService(UserManager<AppUser> userManager, IConfiguration configuration, IEmailService emailService, AppDbContext context)
    {
        _userManager = userManager;
        _configuration = configuration;
        _emailService = emailService;
        _context = context;
    }
}