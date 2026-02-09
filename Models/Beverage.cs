using System.Collections.Generic;
using System.Linq;

namespace MyCoffeeLtd.Models;

public abstract class Beverage
{
    // Base info
    public string Description { get; protected set; } = "Beverage";

    // Condiments (simple flags — NOT a pattern)
    public bool SteamedMilk { get; set; }
    public bool Soy { get; set; }
    public bool Mocha { get; set; }
    public bool Chocolate { get; set; }
    public bool WhippedMilk { get; set; }

    // Add-on prices
    protected const decimal PriceSteamedMilk = 0.30m;
    protected const decimal PriceSoy = 0.40m;
    protected const decimal PriceMocha = 0.50m;
    protected const decimal PriceChocolate = 0.45m;
    protected const decimal PriceWhippedMilk = 0.35m;

    // Each beverage defines its base price
    protected abstract decimal BasePrice { get; }

    public virtual decimal Cost()
    {
        decimal total = BasePrice;

        if (SteamedMilk) total += PriceSteamedMilk;
        if (Soy) total += PriceSoy;
        if (Mocha) total += PriceMocha;
        if (Chocolate) total += PriceChocolate;
        if (WhippedMilk) total += PriceWhippedMilk;

        return total;
    }

    public string CondimentsText()
    {
        var list = new List<string>();
        if (SteamedMilk) list.Add("Steamed Milk");
        if (Soy) list.Add("Soy");
        if (Mocha) list.Add("Mocha");
        if (Chocolate) list.Add("Chocolate");
        if (WhippedMilk) list.Add("Whipped Milk");

        return list.Count == 0 ? "" : " + " + string.Join(" + ", list);
    }

    public override string ToString()
        => $"{Description}{CondimentsText()} — ${Cost():0.00}";
}
