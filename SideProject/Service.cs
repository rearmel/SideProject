namespace SideProject;

public class Service
{
    private readonly AssetRepository _assetRepository;
    public Service(AssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    public Result BuyAsset(string code, int quantity, decimal value)
    {
        if (string.IsNullOrWhiteSpace(code))
            return Result.Failure("Código do ativo inválido!");

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
        var asset = _assetRepository.GetAssets().FirstOrDefault(a => a.Code == code);
        
        if (asset == null)
        {
            return Result.Failure("Ativo não encontrado na carteira!");
        }

        if(asset.Quantity <= quantity)
        {
            return Result.Failure("Quantidade para venda excede a quantidade disponível na carteira!");
        }
        asset.Decrease(quantity);

        if(asset.Quantity == 0)
        {
            _assetRepository.RemoveAssetByCode(code);
        }

        return Result.Success("Ativo vendido com sucesso!");
    }
}
