using DigitalLibrary.Infrastructure.Repositories;
using DigitalLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibrary.Controllers;

public class ClientController : Controller
{
    private readonly ClientRepository _repository;

    public ClientController(ClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var clients = await _repository.GetAll();
        return View(clients);
    }

    public async Task<IActionResult> GetById(int id)
    {
        var client = await _repository.GetById(id);
        if (client == null) return NotFound();
        return Json(client);
    }

    public async Task<IActionResult> AddClient(Client item)
    {
        try
        {
            await _repository.Add(item);
            TempData["CreateSuccess"] = "Cliente creado exitosamente";
        }
        catch (Exception  e)
        {
            TempData["CreateError"] = "Error: " + e.Message;
        }
        
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Update([FromBody]Client item)
    {
        try
        {
            await _repository.Update(item);
            return Ok(new { success = true, message = "Cliente creado exitosamente" });
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