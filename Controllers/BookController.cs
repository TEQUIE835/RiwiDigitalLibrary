using DigitalLibrary.Infrastructure.Repositories;
using DigitalLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibrary.Controllers;

public class BookController : Controller
{
    private readonly BookRepository _repository;

    public BookController(BookRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _repository.GetAll();
        return View(books);
    }

    public async Task<IActionResult> GetById(int id)
    {
        var book = await _repository.GetById(id);
        if (book == null) return NotFound();
        return Json(book);
    }

    public async Task<IActionResult> AddBook(Book item)
    {
        try
        {
            await _repository.Add(item);
            TempData["CreateSuccess"] = "Libro creado exitosamente";
        }
        catch (Exception  e)
        {
            TempData["CreateError"] = "Error: " + e.Message;
        }
        
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Update([FromBody]Book item)
    {
        try
        {
            await _repository.Update(item);
            return Ok(new { success = true, message = "Libro actualizado exitosamente" });
        }
        catch (Exception e)
        {
            return BadRequest(new { success = false, message = "Hubo un error: " + e.Message });
        }
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
}