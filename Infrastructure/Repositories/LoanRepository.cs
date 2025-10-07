using DigitalLibrary.Domain.Interfaces;
using DigitalLibrary.Infrastructure.Data;
using DigitalLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Infrastructure.Repositories;

public class LoanRepository : ILoan<Loan>
{
    private readonly AppDbContext _context;

    public LoanRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Loan>> GetAll()
    {
        return await _context.Loans.Include(l => l.Client)
            .Include(l => l.Book)
            .ToListAsync();
    }

    public async Task<Loan> GetById(int id)
    {
        return await _context.Loans.Include(l => l.Client)
            .Include(l => l.Book)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task Add(LoanCreateDto item)
    {
        if (item.ReturnDate <= item.LoanDate) throw new Exception("La fecha de devolucion no es valida");
        var Client = await _context.Clients.FirstOrDefaultAsync(c => c.Document == item.Document);
        if (Client == null) throw new Exception("Client not found");
        var Book = await _context.Books.FirstOrDefaultAsync(b => b.Code == item.Code);
        if (Book == null) throw new Exception("Book not found");

        if (Book.Copies <= 0) throw new Exception($"Not enough copies");

        Book.Copies -= 1;
        await _context.AddAsync(new Loan()
        {
            ClientId = Client.Id,
            BookId = Book.Id,
            LoanDate = item.LoanDate,
            ReturnDate = item.ReturnDate,
            Returned = false
        });
        await _context.SaveChangesAsync();
    }

    public async Task Update(int id)
    {
        var loan = await _context.Loans.FirstOrDefaultAsync(l => l.Id == id);
            var Book = await _context.Books.FirstOrDefaultAsync(b => b.Id == loan.BookId);
            Book.Copies += 1;
            loan.Returned = true;
            await _context.SaveChangesAsync();
        
        
    }

    public async Task Delete(int id)
    {
        var item = await _context.Loans.FirstOrDefaultAsync(l => l.Id == id);
        if (item.Returned == false){
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == item.BookId);
            book.Copies += 1;
        }
        _context.Loans.Remove(item);
        await _context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<Loan>> GetByBookIdAsync(int bookId)
    {
        return await _context.Loans
            .Include(l => l.Client)
            .Include(l => l.Book )
            .Where(l => l.BookId == bookId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Loan>> GetByClientIdAsync(int clientId)
    {
        return await _context.Loans
            .Include(l => l.Client)
            .Include(l => l.Book)
            .Where(l => l.ClientId == clientId)
            .ToListAsync();
    }

}