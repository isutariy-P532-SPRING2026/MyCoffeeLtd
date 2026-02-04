namespace MyCoffeeLtd.Models;

public abstract class Beverage
{
    public string Description { get; protected set; } = "Unknown Beverage";
    public abstract decimal Cost();
    public override string ToString() => $"{Description} - ${Cost():0.00}";
}
