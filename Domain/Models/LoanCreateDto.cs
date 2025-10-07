namespace DigitalLibrary.Models;

public class LoanCreateDto
{
    public string Document { get; set; }
    public int Code { get; set; }
    public DateOnly LoanDate { get; set; }
    public DateOnly ReturnDate { get; set; }
}