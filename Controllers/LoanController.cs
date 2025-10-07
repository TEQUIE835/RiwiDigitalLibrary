using DigitalLibrary.Infrastructure.Repositories;
using DigitalLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibrary.Controllers;

public class LoanController : Controller
{
    private readonly LoanRepository _repository;

    public LoanController(LoanRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var Loans = await _repository.GetAll();
        return View(Loans);
    }

    public async Task<IActionResult> GetById(int id)
    {
        var Loan = await _repository.GetById(id);
        if (Loan == null) return NotFound();
        return Json(Loan);
    }

    public async Task<IActionResult> AddLoan(LoanCreateDto item)
    {
        try
        {
            await _repository.Add(item);
            TempData["CreateSuccess"] = "Prestamo creado exitosamente";
        }
        catch (Exception  e)
        {
            TempData["CreateError"] = "Error: " + e.Message;
        }
        
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Update( int id)
    {
        try
        {
            await _repository.Update(id);
            TempData["ReturnSuccess"] = "Se ha devuelto el libro exitosamente";
        }
        catch (Exception e)
        {
            TempData["ReturnError"] = "Error: " + e.Message;
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _repository.Delete(id);
            TempData["DeleteSuccess"] = "Se elimino correctamente";
        }
        catch (Exception e)
        {
            TempData["DeleteError"] = "Error al eliminar: " + e.Message;
        }
        
        return RedirectToAction("Index");

    }
    public async Task<IActionResult> ByBook(int id)
    {
        var loans = await _repository.GetByBookIdAsync(id);
        return View(loans);
    }

    public async Task<IActionResult> ByClient(int id)
    {
        var loans = await _repository.GetByClientIdAsync(id);
        return View(loans);
    }
}