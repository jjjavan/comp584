using Ah_webApiBackend.Data;
using Ah_webApiBackend.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ah_webApiBackend.Controllers;

/// <summary>
///     The loan controller.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class LoanController : ControllerBase
{
    ///summary
    ///Db Context
    ///summary
    private readonly CrmDbContext dbContext;

    private LoanController(CrmDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    // GET: api/Loan
    /// <summary>
    ///     Gets all loans.
    /// </summary>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
    {
        return await dbContext.Loans.ToListAsync();
    }

    // GET: api/Loans/5
    /// <summary>
    ///     Gets a specific loan by ID.
    /// </summary>
    /// <param name="id">The loan ID</param>
    /// <returns>Returns a loan</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Loan>> GetLoan(int id)
    {
        var loan = await dbContext.Loans.FindAsync(id);

        if (loan == null) return NotFound();

        return loan;
    }

    // POST: api/Loans
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    [HttpPost]
    public async Task<ActionResult<Loan>> PostLoan(Loan loan)
    {
        dbContext.Loans.Add(loan);
        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            // TODO: Handle the Microsoft.EntityFrameworkCore.DbUpdateException
        }

        return CreatedAtAction(nameof(GetLoan), new { id = loan.Id }, loan);
    }

    // PUT: api/Loans/5
    /// <exception cref="DbUpdateConcurrencyException">Condition.</exception>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLoan(int id, Loan Loan)
    {
        if (id != Loan.Id) return BadRequest();

        dbContext.Entry(Loan).State = EntityState.Modified;

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!dbContext.Loans.Any(e => e.Id == id)) return NotFound();

            throw;
        }
        catch (DbUpdateException ex)
        {
            // TODO: Handle the Microsoft.EntityFrameworkCore.DbUpdateException
        }

        return NoContent();
    }

    // DELETE: api/Loans/5
    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLoan(int id)
    {
        var Loan = await dbContext.Loans.FindAsync(id);
        if (Loan == null) return NotFound();

        dbContext.Loans.Remove(Loan);
        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            // TODO: Handle the Microsoft.EntityFrameworkCore.DbUpdateException
        }

        return NoContent();
    }
}