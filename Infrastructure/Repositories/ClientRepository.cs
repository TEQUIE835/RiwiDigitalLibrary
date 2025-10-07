using DigitalLibrary.Domain.Interfaces;
using DigitalLibrary.Infrastructure.Data;
using DigitalLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Infrastructure.Repositories;

public class ClientRepository : IClient<Client>
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Client>> GetAll()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client> GetById(int id)
    {
        return await _context.Clients.FirstOrDefaultAsync(b => b.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task Add(Client item)
    {
        if (item == null || item.Document == null || item.Document == "" || item.Email == null || item.Email == "") throw new Exception("Campos invalidos");
        var client =  await _context.Clients.FirstOrDefaultAsync(c => c.Document == item.Document);
        if (client != null) throw new Exception("A book with that code already exists");
        await _context.Clients.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Client item)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == item.Id);
        if (client == null) throw new Exception("Book not found");
        bool codeExists = await _context.Clients.AnyAsync(c => c.Document == item.Document && c.Id != item.Id);
        if (codeExists)
            throw new Exception("Ya existe un libro con ese código.");
        client.Name = item.Name;
        client.Document = item.Document;
        client.Email = item.Email;
        client.Phone = item.Phone;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        if (client == null) throw new Exception("Book not found");
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }
}