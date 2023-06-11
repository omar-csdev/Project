public class PaidOrder
{
    public string ReservationCode { get; set; }
    public int CustomerId { get; set; }
    public DateTime PaidTime { get; set; }
    public double TotalPrice { get; set; }
}
