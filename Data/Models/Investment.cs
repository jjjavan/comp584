// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Investment.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Investment type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ah_webApiBackend.Data.Models;

/// <summary>
///     The investment.
/// </summary>
[Table("Investments")]
public class Investment
{
    /// <summary>
    /// </summary>
    [Key]
    [Required]
    public int Id { get; set; } // Foreign Key

    public int? InvestorId { get; set; } // Foreign Key

    public int LoanId { get; set; }


    public decimal? Amount { get; set; }


    public DateTime InvestmentDate { get; set; }

    public decimal? ExpectedReturn { get; set; }


    // Navigation property for the related Investor
    // and loan.
    /// <summary>
    /// </summary>
    [ForeignKey("InvestorId")]
    public Investor Investor { get; set; }

    [ForeignKey("LoanId")] public Loan Loan { get; set; }
}