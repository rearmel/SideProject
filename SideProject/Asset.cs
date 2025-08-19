namespace SideProject;

public class Asset
{
    public Asset(string assetCode, int assetQuantity, decimal assetValue)
    {
        Code = assetCode;
        Quantity = assetQuantity;
        Value = assetValue;
        CreatedAt = DateTime.Now;
    }

    public string Code { get; private set; }

    public int Quantity { get; private set; }

    public decimal Value { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public void Decrease(int quantity)
    {
        if (quantity > Quantity)
            throw new InvalidOperationException("Quantidade insuficiente para venda.");


        if (quantity < 0)
            throw new ArgumentException("A quantidade não pode ser negativa.");

        Quantity -= quantity;
    }
}
