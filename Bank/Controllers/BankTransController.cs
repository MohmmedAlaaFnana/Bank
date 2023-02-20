using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bank.Data;
using Bank.Models;

namespace Bank.Controllers
{
    public class BankTransController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankTransController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BankTrans
        public async Task<IActionResult> Index()
        {
            return View(await _context.BankTrans.ToListAsync());
        }

        // GET: BankTrans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankTrans = await _context.BankTrans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankTrans == null)
            {
                return NotFound();
            }

            return View(bankTrans);
        }

        // GET: BankTrans/AddOrEdit
        public IActionResult AddOrEdit()
        {
            return View(new BankTrans());
        }

        // POST: BankTrans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] BankTrans bankTrans)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bankTrans);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bankTrans);
        }

        // GET: BankTrans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankTrans = await _context.BankTrans.FindAsync(id);
            if (bankTrans == null)
            {
                return NotFound();
            }
            return View(bankTrans);
        }

        // POST: BankTrans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] BankTrans bankTrans)
        {
            if (id != bankTrans.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankTrans);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankTransExists(bankTrans.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bankTrans);
        }

        // GET: BankTrans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankTrans = await _context.BankTrans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankTrans == null)
            {
                return NotFound();
            }

            return View(bankTrans);
        }

        // POST: BankTrans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bankTrans = await _context.BankTrans.FindAsync(id);
            _context.BankTrans.Remove(bankTrans);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankTransExists(int id)
        {
            return _context.BankTrans.Any(e => e.Id == id);
        }
    }
}
