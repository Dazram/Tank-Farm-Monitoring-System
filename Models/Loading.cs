namespace Capstone.Models;

public class Loading
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? Customer { get; set; }
    public string? Product { get; set; }
    public int Quantity { get; set; }
    public string? DriverName { get; set; }
    public string? TruckNo { get; set; }

    public Loading()
    {
        Date = DateTime.Now;
    }
}