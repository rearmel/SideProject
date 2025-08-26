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
            throw new AssetException("Quantidade insuficiente para venda.");


        if (quantity < 0)
            throw new AssetException("A quantidade não pode ser negativa.");

        Quantity -= quantity;
    }

    public void Increase(int quantity)
    {
        Quantity += quantity;
    }

    public decimal CalculateTotalAllocated()
    {
        return Quantity * Value;
    }

    public bool IsQuantityZero()
    {
        return Quantity == 0;
    }
}
