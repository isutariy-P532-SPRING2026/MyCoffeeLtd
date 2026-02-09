

namespace MyCoffeeLtd.Models;

public class Soy : CondimentDecorator
{
    

    public Soy(Beverage beverage): base(beverage)
    {
        
    }

    public override string GetDescription()
    {
        return beverage.GetDescription() + ", Soy";
    }

    public override double Cost()
    {
        return beverage.Cost() + 0.40;
    }
}
