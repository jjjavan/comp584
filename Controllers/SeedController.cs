using System.Security;
using Ah_webApiBackend.Data;
using Ah_webApiBackend.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


namespace Ah_webApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly CrmDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SeedController(CrmDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet("import-investors")]
        public async Task<IActionResult> ImportInvestors()
        {
            if (!_env.IsDevelopment()) throw new SecurityException("Not Allowed");

            var filePath = Path.Combine(_env.ContentRootPath, "Data", "Source", "InvestorsFile.xlsx");

            if (!System.IO.File.Exists(filePath))
                return NotFound($"File not found at {filePath}");

            using var stream = System.IO.File.OpenRead(filePath);
            using var excelPackage = new ExcelPackage(stream);

            var worksheet = excelPackage.Workbook.Worksheets[0]; // First sheet
            var rowCount = worksheet.Dimension.End.Row;

            var investorsByEmail = _context.Investors
                .AsNoTracking()
                .ToDictionary(x => x.Email, StringComparer.OrdinalIgnoreCase);

            int recordsAdded = 0;

            for (var row = 2; row <= rowCount; row++)
            {
                var firstName = worksheet.Cells[row, 6].GetValue<string>(); // Column 6 = First Name
                var lastName = worksheet.Cells[row, 7].GetValue<string>();  // Column 7 = Last Name
                var phone = worksheet.Cells[row, 9].GetValue<string>();     // Column 9 = Phone
                var email = worksheet.Cells[row, 10].GetValue<string>();    // Column 10 = Email
                var accountNumber = worksheet.Cells[row, 8].GetValue<string>(); // Column 8 = Account Number

                if (string.IsNullOrWhiteSpace(email) || investorsByEmail.ContainsKey(email))
                    continue;

                var investor = new Investor
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Phone = phone,
                    Email = email,
                    AccountNumber = accountNumber
                };

                _context.Investors.Add(investor);
                investorsByEmail[email] = investor;
                recordsAdded++;
            }

            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Imported {recordsAdded} investors from Excel." });
        }
        [HttpGet("seed-admin")]
        public async Task<IActionResult> SeedAdminUser()
        {
            if (!_env.IsDevelopment()) throw new SecurityException("Not Allowed");

            if (_context.Users.Any(u => u.Username == "admin"))
                return BadRequest("Admin user already exists.");

            var user = new User
            {
                Username = "admin",
                PasswordHash = HashPassword("password"),
                Role = "Admin"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Admin user created.");
        }

        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

    }

}
