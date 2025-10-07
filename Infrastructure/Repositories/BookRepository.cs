using DigitalLibrary.Domain.Interfaces;
using DigitalLibrary.Infrastructure.Data;
using DigitalLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Infrastructure.Repositories;

public class BookRepository : IBook<Book>
{
    
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Book>> GetAll()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book> GetById(int id)
    {
        return await _context.Books.FirstOrDefaultAsync(b => b.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task Add(Book item)
    {
        if (item == null || item.Code == 0 || item.Copies == 0) throw new Exception("Campos invalidos");
        var book =  await _context.Books.FirstOrDefaultAsync(b => b.Code == item.Code);
        if (book != null) throw new Exception("A book with that code already exists");
        await _context.Books.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Book item)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == item.Id);
        if (book == null) throw new Exception("Book not found");
        bool codeExists = await _context.Books.AnyAsync(b => b.Code == item.Code && b.Id != item.Id);
        if (codeExists)
            throw new Exception("Ya existe un libro con ese código.");
        book.Name = item.Name;
        book.Author = item.Author;
        book.Copies = item.Copies;
        book.Code = item.Code;
        book.Gender = item.Gender;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book == null) throw new Exception("Book not found");
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }
}