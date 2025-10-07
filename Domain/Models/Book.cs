using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DigitalLibrary.Models;

public class Book
{
    [Key] 
    public int Id { get; set; }
    
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(30)]
    public string Author { get; set; } = string.Empty;

    [StringLength(50)] 
    public string Gender { get; set; } = string.Empty;
    
    public int Code { get; set; }
    
    public int Copies { get; set; }

    public List<Loan> Loans { get; set; }
    
}