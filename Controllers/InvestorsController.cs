using Ah_webApiBackend.Data;
using Ah_webApiBackend.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ah_webApiBackend.Controllers;

/// <summary>
///     The investors controller.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class InvestorsController : ControllerBase
{
    /// <summary>
    ///     The dbContext.
    /// </summary>
    private readonly CrmDbContext dbContext;

    /// <summary>
    ///     Initializes a new instance of the <see cref="InvestorsController" /> class.
    /// </summary>
    /// <param name="dbContext">
    ///     The dbContext.
    /// </param>
    public InvestorsController(CrmDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    // GET: api/Investors

    /// <summary>
    ///     The get investors.
    /// </summary>
    /// <exception cref="OperationCanceledException">
    ///     If the <see cref="CancellationToken" /> is canceled.
    /// </exception>
    /// <returns>
    ///     The <see cref="Task" />.
    /// </returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Investor>>> GetInvestors()
    {
        return await dbContext.Investors.ToListAsync();
    }

    // GET: api/Investors/5

    /// <summary>
    ///     The get investor.
    /// </summary>
    /// <param name="id">
    ///     The id.
    /// </param>
    /// <returns>
    ///     The<see cref="Task" />.
    /// </returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Investor>> GetInvestor(int id)
    {
        var investor = await dbContext.Investors.FindAsync(id);

        if (investor == null) return NotFound();

        return investor;
    }

    // POST: api/Investors

    /// <summary>
    ///     The post investor.
    /// </summary>
    /// <param name="investor">
    ///     The investor.
    /// </param>
    /// <returns>
    ///     The <see cref="Task" />.
    /// </returns>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    /// <exception cref="DbUpdateException">An error is encountered while saving to the database.</exception>
    /// <exception cref="DbUpdateConcurrencyException">
    ///     A concurrency violation is encountered while saving to the database.
    ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
    ///     This is usually because the data in the database has been modified since it was loaded into memory.
    /// </exception>
    [HttpPost]
    public async Task<ActionResult<Investor>> PostInvestor(Investor investor)
    {
        dbContext.Investors.Add(investor);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetInvestor), new { id = investor.Id }, investor);
    }

    // PUT: api/Investors/5

    /// <summary>
    ///     The put investor.
    /// </summary>
    /// <param name="id">
    ///     The id.
    /// </param>
    /// <param name="investor">
    ///     The investor.
    /// </param>
    /// <returns>
    ///     The <see cref="Task" />.
    /// </returns>
    /// <exception cref="DbUpdateConcurrencyException">Condition.</exception>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutInvestor(int id, Investor investor)
    {
        if (id != investor.Id) return BadRequest();

        dbContext.Entry(investor).State = EntityState.Modified;

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!dbContext.Investors.Any(e => e.Id == id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // DELETE: api/Investors/5

    /// <summary>
    ///     The delete investor.
    /// </summary>
    /// <param name="id">
    ///     The id.
    /// </param>
    /// <exception cref="OperationCanceledException">
    ///     If the <see cref="CancellationToken" /> is canceled.
    /// </exception>
    /// <exception cref="DbUpdateException">
    ///     An error is encountered while saving to the database.
    /// </exception>
    /// <returns>
    ///     The <see cref="Task" />.
    /// </returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvestor(int id)
    {
        var investor = await dbContext.Investors.FindAsync(id);
        if (investor == null) return NotFound();

        dbContext.Investors.Remove(investor);
        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            // TODO: Handle the Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException
        }

        return NoContent();
    }
}