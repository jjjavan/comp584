namespace Ah_webApiBackend.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// The investor.
/// </summary>
[Table("Investors")]
public class Investor
{
    #region Properties

    /// <summary>
    /// The unique id and primary key for this Investor
    /// </summary>
    [Key]
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Investor first name (UTF8)
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Investor last name (UTF8)
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Investor email (UTF8)
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Investor address (UTF8)
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Investor city (UTF8)
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Investor state (UTF8)
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// Investor postal code (Changed to string to allow leading zeros)
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    /// Investor country 
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Investor phone (Changed to string to handle different formats)
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Investor account number (added for better tracking)
    /// </summary>
    public string? AccountNumber { get; set; }

    #endregion

    /// <summary>
    /// Relationship: An investor can have multiple investments.
    /// </summary>
    public ICollection<Investment>? Investments { get; set; }
}
