using SideProject.Helpers;
using SideProject.Repositories;
using SideProject.Repositories.Interface;
using System.Text.RegularExpressions;

namespace SideProject.Service;

public class AssetService
{
    private readonly IAssetRepository _assetRepository;
    public AssetService(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    public Result BuyAsset(string code, int quantity, decimal value)
    {
        if (!AssetHelper.IsValidCode(code))
            return Result.Failure("Código do ativo deve conter apenas letras e números!");

        if (quantity <= 0)
            return Result.Failure("Quantidade deve ser maior que zero!");

        if (value <= 0)
            return Result.Failure("Valor deve ser maior que zero!");

        var asset = _assetRepository.GetAssets().FirstOrDefault(a => a.Code == code);

        if (asset != null)
        {
            asset.Increase(quantity);
            return Result.Success("Ativo já existente na carteira, quantidade atualizada com sucesso!");
        }
        else
        {
            var newAsset = new Asset(code, quantity, value);
            _assetRepository.AddAsset(newAsset);
        }

        return Result.Success("Ativo adicionado com sucesso!");
    }

    public Result SellAsset(string code, int quantity)
    {
        var asset = _assetRepository.GetAssetByCode(code);

        if (asset == null)
        {
            return Result.Failure("Ativo não encontrado na carteira!");
        }

        if (asset.Quantity < quantity)
        {
            return Result.Failure("Quantidade para venda excede a quantidade disponível na carteira!");
        }

        asset.Decrease(quantity);

        if (asset.IsQuantityZero())
        {
            _assetRepository.RemoveAssetByCode(code);
        }

        return Result.Success("Ativo vendido com sucesso!");
    }
}
