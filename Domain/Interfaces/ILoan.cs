using DigitalLibrary.Models;

namespace DigitalLibrary.Domain.Interfaces;

public interface ILoan<T>
{
    Task<List<T>> GetAll();
    
    Task<T> GetById(int id);
    Task Add(LoanCreateDto item);
    Task Update(int id);
    Task Delete(int id);
}