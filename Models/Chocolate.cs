
namespace MyCoffeeLtd.Models;

public class Chocolate : CondimentDecorator
{
    

    public Chocolate(Beverage beverage): base(beverage)
    {
        
    }

    public override string GetDescription()
    {
        return beverage.GetDescription() + ", Chocolate";
    }

    public override double Cost()
    {
        return beverage.Cost() + 0.45;
    }
}
