using SideProject;

namespace SideProjectTests;

public class AssetTests
{
    [Fact]
    public void Add_New_Asset()
    {
        // Arrange
        var code = "AAA";
        var quantity = 10;
        var value = 123.45m;
        var before = DateTime.Now;

        // Act
        var asset = new Asset(code, quantity, value);

        // Assert
        Assert.Equal(code, asset.Code);
        Assert.Equal(quantity, asset.Quantity);
        Assert.Equal(value, asset.Value);
        Assert.True(asset.CreatedAt >= before && asset.CreatedAt <= DateTime.Now);
    }

    [Fact]
    public void Decrease_Valid_Quantity()
    {
        // Arrange
        var asset = new Asset("AAA", 10, 200m);

        // Act
        asset.Decrease(3);

        // Assert
        Assert.Equal(7, asset.Quantity);
    }

    [Fact]
    public void Decrease_Quantity_reater_Than_Available()
    {
        // Arrange
        var asset = new Asset("AAA", 5, 150.0m);

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => asset.Decrease(10));
        Assert.Equal("Quantidade insuficiente para venda.", ex.Message);
    }

    [Fact]
    public void Decrease_Negative_Quantity()
    {
        // Arrange
        var asset = new Asset("AAA", 8, 80.0m);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => asset.Decrease(-2));
        Assert.Equal("A quantidade não pode ser negativa.", ex.Message);
    }
}