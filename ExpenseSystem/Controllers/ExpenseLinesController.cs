using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseSystem.Models;

namespace ExpenseSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseLinesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExpenseLinesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ExpenseLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseLine>>> GetExpensesLines()
        {
          if (_context.ExpensesLines == null)
          {
              return NotFound();
          }
            return await _context.ExpensesLines.ToListAsync();
        }

        // GET: api/ExpenseLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseLine>> GetExpenseLine(int id)
        {
          if (_context.ExpensesLines == null)
          {
              return NotFound();
          }
            var expenseLine = await _context.ExpensesLines.FindAsync(id);

            if (expenseLine == null)
            {
                return NotFound();
            }

            return expenseLine;
        }

        // PUT: api/ExpenseLines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpenseLine(int id, ExpenseLine expenseLine)
        {
            if (id != expenseLine.Id)
            {
                return BadRequest();
            }

            _context.Entry(expenseLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseLineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        [HttpPut("RecalculateExpenses")]
        private async Task<IActionResult> RecalculateExpenses(int expenseId) {
            var expense = await _context.Expenses.FindAsync(expenseId);
            if (expense == null) {
                throw new Exception($"Could not find {expenseId}");
            }
            expense.Total = (from el in _context.ExpensesLines
                             join i in _context.Items
                             on el.ItemId equals i.Id
                             select new {
                                 ExpenseTotal = el.Quantity * i.Price
                             }).Sum(x => x.ExpenseTotal);
            await _context.SaveChangesAsync();
            return Ok();
        }


        // POST: api/ExpenseLines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExpenseLine>> PostExpenseLine(ExpenseLine expenseLine)
        {
          if (_context.ExpensesLines == null)
          {
              return Problem("Entity set 'AppDbContext.ExpensesLines'  is null.");
          }
            _context.ExpensesLines.Add(expenseLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpenseLine", new { id = expenseLine.Id }, expenseLine);
        }

        // DELETE: api/ExpenseLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenseLine(int id)
        {
            if (_context.ExpensesLines == null)
            {
                return NotFound();
            }
            var expenseLine = await _context.ExpensesLines.FindAsync(id);
            if (expenseLine == null)
            {
                return NotFound();
            }

            _context.ExpensesLines.Remove(expenseLine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenseLineExists(int id)
        {
            return (_context.ExpensesLines?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
