// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Borrower.cs" company="">
//   
// </copyright>
// <summary>
//   The borrower.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ah_webApiBackend.Data.Models;

/// <summary>
///     The borrower.
/// </summary>
[Table("Borrowers")]
public class Borrower
{
    /// <summary>
    ///     Gets or sets the id.
    /// </summary>
    [Key]
    [Required]
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the name.
    /// </summary>
    public string? Name { get; set; } = null;

    /// <summary>
    ///     Gets or sets the email.
    /// </summary>
    public string? Email { get; set; } = null;

    /// <summary>
    ///     Gets or sets the phone.
    /// </summary>
    public string? Phone { get; set; } = null;

    /// <summary>
    ///     Gets or sets the address.
    /// </summary>
    public string? Address { get; set; } = null;

    /// <summary>
    ///     Gets or sets the company name.
    /// </summary>
    public string? CompanyName { get; set; } = null; // Company name of the borrower if applicable

    /// <summary>
    ///     Gets or sets the credit rating.
    /// </summary>
    public string? CreditRating { get; set; } = null; // Optional field for borrower credit rating
}