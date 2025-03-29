using Ah_webApiBackend.Data;
using Ah_webApiBackend.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ah_webApiBackend.Controllers;

/// <summary>
///     The borrowers controller.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BorrowersController : ControllerBase
{
    /// <summary>
    ///     The dbContext.
    /// </summary>
    private readonly CrmDbContext dbContext;

    /// <summary>
    ///     default constructor for borrowers controller.
    /// </summary>
    /// <summary>
    ///     The borrowers controller.
    /// </summary>
    public BorrowersController(CrmDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    // GET: api/Borrowers
    /// <summary>
    ///     Gets all borrowers.
    /// </summary>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Borrower>>> GetBorrowers()
    {
        return await dbContext.Borrowers.ToListAsync();
    }

    // GET: api/Borrowers/5
    /// <summary>
    ///     Gets a specific borrower by ID.
    /// </summary>
    /// <param name="id">The borrower ID</param>
    /// <returns>Returns a borrower</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Borrower>> GetBorrower(int id)
    {
        var borrower = await dbContext.Borrowers.FindAsync(id);

        if (borrower == null) return NotFound();

        return borrower;
    }

    // POST: api/Borrowers
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    [HttpPost]
    public async Task<ActionResult<Borrower>> PostBorrower(Borrower borrower)
    {
        dbContext.Borrowers.Add(borrower);
        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            // TODO: Handle the Microsoft.EntityFrameworkCore.DbUpdateException
        }

        return CreatedAtAction(nameof(GetBorrower), new { id = borrower.Id }, borrower);
    }

    // PUT: api/Borrowers/5
    /// <exception cref="DbUpdateConcurrencyException">Condition.</exception>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBorrower(int id, Borrower borrower)
    {
        if (id != borrower.Id) return BadRequest();

        dbContext.Entry(borrower).State = EntityState.Modified;

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!dbContext.Borrowers.Any(e => e.Id == id)) return NotFound();

            throw;
        }
        catch (DbUpdateException ex)
        {
            // TODO: Handle the Microsoft.EntityFrameworkCore.DbUpdateException
        }

        return NoContent();
    }

    // DELETE: api/Borrowers/5
    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBorrower(int id)
    {
        var borrower = await dbContext.Borrowers.FindAsync(id);
        if (borrower == null) return NotFound();

        dbContext.Borrowers.Remove(borrower);
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