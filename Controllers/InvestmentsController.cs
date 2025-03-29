using Ah_webApiBackend.Data;
using Ah_webApiBackend.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ah_webApiBackend.Controllers;

/// <summary>
///     The investments controller.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class InvestmentsController : ControllerBase
{
    private readonly CrmDbContext dbContext;

    public InvestmentsController(CrmDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    // GET: api/Investments
    /// <summary>
    ///     Gets all investments
    /// </summary>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Investment>>> GetInvestments()
    {
        return await dbContext.Investments.ToListAsync();
    }

    // GET: api/Investments/5
    /// <summary>
    ///     Gets a specific investment by ID
    /// </summary>
    /// <param name="id">The investment ID</param>
    /// <returns>returns an investment</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Investment>> GetInvestment(int id)
    {
        var investment = await dbContext.Investments.FindAsync(id);

        if (investment == null) return NotFound();

        return investment;
    }

    // POST: api/Investments
    /// <summary>
    /// </summary>
    /// <param name="investment"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Investment>> PostInvestment(Investment investment)
    {
        dbContext.Investments.Add(investment);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetInvestment), new { id = investment.Id }, investment);
    }

    // PUT: api/Investments/5
    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="investment"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutInvestment(int id, Investment investment)
    {
        if (id != investment.Id) return BadRequest();

        dbContext.Entry(investment).State = EntityState.Modified;

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!dbContext.Investments.Any(e => e.Id == id)) return NotFound();

            throw;
        }

        return NoContent();
    }

    // DELETE: api/Investments/5
    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvestment(int id)
    {
        var investment = await dbContext.Investments.FindAsync(id);
        if (investment == null) return NotFound();

        dbContext.Investments.Remove(investment);
        await dbContext.SaveChangesAsync();

        return NoContent();
    }
}