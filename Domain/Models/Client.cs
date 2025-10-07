using System.ComponentModel.DataAnnotations;

namespace DigitalLibrary.Models;

public class Client
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(100)]
    public string Name { get; set; } = String.Empty;
    
    [StringLength(30)]
    public string Document { get; set; } = String.Empty;
    
    [StringLength(200)]
    public string Email { get; set; } = String.Empty;

    [StringLength(20)] 
    public string Phone { get; set; } = String.Empty;

    public List<Loan> Loans { get; set; }
}