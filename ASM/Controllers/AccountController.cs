using ASM.Models;
using ASM.Models.ViewModels.User;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Google;

namespace ASM.Controllers
{
    public class AccountController : Controller
    {
		private readonly IMapper _mapper;
		private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AccountController(IMapper mapper, AppDbContext context, IWebHostEnvironment environment)
		{
			_mapper = mapper;
			_context = context;
            _environment = environment;
        }
        [HttpGet("login")]
        public IActionResult Login(string? returnUrl)
        {
            var model = new UserLoginViewModel { ReturnUrl = returnUrl ?? Url.Content("~/") };
            return View(model);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Tìm người dùng qua Email
            var user = await _context.Users
                 .Where(u => u.Email == model.Email && u.IsActive)
                 .FirstOrDefaultAsync();

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email không tồn tại hoặc tài khoản đã bị khóa.");
                return View(model);
            }

            // Kiểm tra mật khẩu với BCrypt
            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError(string.Empty, "Mật khẩu không chính xác.");
                return View(model);
            }

            // Đăng nhập thành công → lưu thông tin vào Session
            HttpContext.Session.SetInt32("UserID", user.UserID);
            HttpContext.Session.SetString("UserName", user.FullName);
            HttpContext.Session.SetString("UserRole", user.Role.ToString());
            // Nếu là Admin → đưa vào site quản lý
            if (user.Role == UserRole.Admin)
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            // Chuyển hướng tới trang được yêu cầu (nếu có), ngược lại về trang chủ
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
		public IActionResult Register() => View();

		[HttpPost]
		public async Task<IActionResult> Register(UserRegisterViewModel model)
		{
			if (!ModelState.IsValid) return View(model);

			var user = _mapper.Map<User>(model);
			user.Role = UserRole.Customer;
			user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
			user.CreatedAt = DateTime.Now;

			_context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login");
		}
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }

        [HttpGet("account/google-login")]
        public IActionResult GoogleLogin(string returnUrl = "/")
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse", new { returnUrl }) };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        [HttpGet]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
                return RedirectToAction("Login");

            var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
            var name = result.Principal.FindFirst(ClaimTypes.Name)?.Value;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                // Tự động đăng ký nếu chưa tồn tại
                user = new User
                {
                    Email = email,
                    FullName = name ?? email,
                    PasswordHash = string.Empty,
                    Role = UserRole.Customer,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            // Lưu vào session
            HttpContext.Session.SetInt32("UserID", user.UserID);
            HttpContext.Session.SetString("UserName", user.FullName);
            HttpContext.Session.SetString("UserRole", user.Role.ToString());

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("account")]
        public async Task<IActionResult> Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
                return RedirectToAction("Login");

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound();

            var model = new UserProfileViewModel
            {
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                AvatarUrl = user.AvatarUrl,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth
            };

            return View(model);
        }
        [HttpPost("account")]
        public async Task<IActionResult> Profile(UserProfileViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login");

            if (!ModelState.IsValid) return View(model);

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();

            // Cập nhật thông tin cơ bản
            user.FullName = model.FullName;
            user.Phone = model.Phone;
            user.Address = model.Address;
            user.Gender = model.Gender;
            user.DateOfBirth = model.DateOfBirth;

            if (model.Avatar != null && model.Avatar.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "avatars");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(model.Avatar.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Avatar.CopyToAsync(fileStream);
                }

                // Cập nhật avatar URL cho user
                user.AvatarUrl = $"/uploads/avatars/{uniqueFileName}";
            }

            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("UserName", user.FullName);
            TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
            return RedirectToAction("Profile");
        }




    }
}
