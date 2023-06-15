

public class Revenue
{
    public int Month { get; set; }
    public double RevenueMade { get; set; }

    public Revenue(int month, double revenuemade) 
    {
        Month = month;
        RevenueMade = revenuemade;
    }
}
