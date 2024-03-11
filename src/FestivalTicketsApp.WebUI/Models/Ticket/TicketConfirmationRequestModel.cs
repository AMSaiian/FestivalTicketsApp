namespace FestivalTicketsApp.WebUI.Models.Ticket;

public class TicketConfirmationRequestModel
{
    public string CardNumber { get; set; }
    
    public DateOnly ExpirationDate { get; set; }
    
    public List<int> SelectedTicketsId { get; set; }
}