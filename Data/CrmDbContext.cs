using Ah_webApiBackend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Ah_webApiBackend.Data
{
    /// <summary>
    /// </summary>
    public class CrmDbContext : DbContext
    {
        /// <summary>
        /// </summary>
        /// <param name="options"></param>
        public CrmDbContext(DbContextOptions<CrmDbContext> options) : base(options)
        {
        }

        // DbSet properties for each of your models
        /// <summary>
        /// </summary>
        public DbSet<Investor> Investors { get; set; }

        /// <summary>
        ///     Investors
        /// </summary>
        public DbSet<Borrower> Borrowers { get; set; }

        /// <summary>
        ///     Borrowers
        /// </summary>
        public DbSet<Loan> Loans { get; set; }

        /// <summary>
        ///     Loans
        /// </summary>
        public DbSet<Investment> Investments { get; set; }

        public DbSet<User> Users { get; set; }

    }
}