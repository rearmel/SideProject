using SideProject;

namespace SideProjectTests;

public class AssetBuilder
{
    private string _code = "ABC";
    private int _quantity = 1;
    private decimal _value = 1m;

    public AssetBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    public AssetBuilder WithQuantity(int quantity)
    {
        _quantity = quantity;
        return this;
    }

    public AssetBuilder WithValue(decimal value)
    {
        _value = value;
        return this;
    }

    public Asset Build()
    {
        return new Asset(_code, _quantity, _value);
    }
}
