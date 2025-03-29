using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ah_webApiBackend.Data.Models;

[Table("Loans")]
public class Loan
{
    [Key] [Required] public int Id { get; set; }

    public string LoanName { get; set; }

    [Required] public string LoanType { get; set; }

    [Required] public string GrossOrNetFund { get; set; }

    [Required] public decimal LoanAmount { get; set; }

    [Required] public string LoanStatus { get; set; } // C=Current, L=late, D=defaulted

    public DateTime DateFunded { get; set; }
    public int DaysBehind { get; set; }
    public DateTime MaturityDate { get; set; }
    public ICollection<Investment> Investments { get; set; }
}