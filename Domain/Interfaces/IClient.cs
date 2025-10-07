namespace DigitalLibrary.Domain.Interfaces;

public interface IClient<T>
{
    Task<List<T>> GetAll();
    
    Task<T> GetById(int id);
    Task Add(T item);
    Task Update(T item);
    Task Delete(int id);
}