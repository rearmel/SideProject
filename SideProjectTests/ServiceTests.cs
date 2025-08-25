using Moq;
using SideProject;
using SideProject.Repositories.Interface;
using SideProject.Service;

namespace SideProjectTests;

public class ServiceTests
{
    private readonly Mock<IAssetRepository> _assetRepositoryMock;
    private readonly AssetService _service;

    public ServiceTests()
    {
        _assetRepositoryMock = new Mock<IAssetRepository>();
        _service = new AssetService(_assetRepositoryMock.Object);
    }

    [Fact]
    public void BuyAsset_When_Code_Is_Invalid()
    {
        // Arrange
        var asset = new AssetBuilder()
        .WithCode("123@")
        .WithQuantity(10)
        .WithValue(100)
        .Build();

        var assets = new List<Asset> { asset };
        _assetRepositoryMock.Setup(r => r.GetAssets()).Returns(assets);

        // Act
        var result = _service.BuyAsset("123@", 10, 100);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Código do ativo deve conter apenas letras e números!", result.Message);
    }

    [Fact]
    public void BuyAsset_Quantity_Is_Negative_Or_Equals_Zero()
    {
        // Arrange
        var asset = new AssetBuilder()
        .WithCode("ABC")
        .WithQuantity(0)
        .WithValue(100)
        .Build();

        var assets = new List<Asset> { asset };
        _assetRepositoryMock.Setup(r => r.GetAssets()).Returns(assets);

        // Act
        var result = _service.BuyAsset("ABC", 0, 100);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Quantidade deve ser maior que zero!", result.Message);
    }

    [Fact]
    public void BuyAsset_Value_Is_Negative_Or_Equals_Zero()
    {
        // Arrange
        var asset = new AssetBuilder()
        .WithCode("ABC")
        .WithQuantity(10)
        .WithValue(-2)
        .Build();

        var assets = new List<Asset> { asset };
        _assetRepositoryMock.Setup(r => r.GetAssets()).Returns(assets);

        // Act
        var result = _service.BuyAsset("ABC", 10, -2);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Valor deve ser maior que zero!", result.Message);
    }

    [Fact]
    public void BuyAsset_When_Exist_Asset_And_Increase_Quantity()
    {
       // Arrange
        var asset = new AssetBuilder()
        .WithCode("ABC")
        .WithQuantity(10)
        .WithValue(100)
        .Build();

        var assets = new List<Asset> { asset };
        _assetRepositoryMock.Setup(r => r.GetAssets()).Returns(assets);

        // Act
        var result = _service.BuyAsset("ABC", 5, 100);

        // Assert
        Assert.Equal(15, asset.Quantity); 
        Assert.True(result.IsSuccess);
        Assert.Equal("Ativo já existente na carteira, quantidade atualizada com sucesso!", result.Message);
    }

    [Fact]
    public void Buy_And_Add_Asset_Whith_Success()
    {
        // Arrange
        _assetRepositoryMock.Setup(r => r.GetAssets()).Returns(new List<Asset>());

        // Act
        var result = _service.BuyAsset("DEF", 5, 100);

        // Assert
        _assetRepositoryMock.Verify(r => r.AddAsset(It.IsAny<Asset>()), Times.Once);
        Assert.True(result.IsSuccess);
        Assert.Equal("Ativo adicionado com sucesso!", result.Message);
    }

    [Fact]
    public void Sell_Asset_And_Asset_Not_Exists()
    {
        // Arrange
        _assetRepositoryMock.Setup(r => r.GetAssets()).Returns(new List<Asset>());

        // Act
        var result = _service.SellAsset("XYZ", 10);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Ativo não encontrado na carteira!", result.Message);
    }

    [Fact]
    public void Sell_Asset_When_Quantity_Exceeds_Available()
    {
        // Arrange  
        var asset = new Asset("ABC", 5, 100);
        
        _assetRepositoryMock.Setup(r => r.GetAssets())
        .Returns(new List<Asset> { asset });

        // Act  
        var result = _service.SellAsset("ABC", 10);

        // Assert  
        Assert.False(result.IsSuccess);
        Assert.Equal("Quantidade para venda excede a quantidade disponível na carteira!", result.Message);
    }
}
