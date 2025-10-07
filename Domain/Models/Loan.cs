using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalLibrary.Models;

public class Loan
{
    [Key]
    public int Id { set; get; }
    
    [ForeignKey("Client")]
    public int ClientId { set; get; }
    
    [ForeignKey("Book")]
    public int BookId { set; get; }
    
    public DateOnly LoanDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly? ReturnDate { get; set; }

    public bool Returned { get; set; } = false;
    
    public Client Client { get; set; }
    
    public Book Book { get; set; }
    
    
    
    
}