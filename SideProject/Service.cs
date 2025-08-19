namespace SideProject;

public class Service
{
    private readonly AssetRepository _assetRepository;
    public Service(AssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    public Result IsPossibleBuyAsset(string code, int quantity, decimal value)
    {
        if (string.IsNullOrWhiteSpace(code))
            return Result.Failure("Código do ativo inválido.");

        if (quantity <= 0)
            return Result.Failure("Quantidade deve ser maior que zero.");

        if (value <= 0)
            return Result.Failure("Valor deve ser maior que zero.");

        var newAsset = new Asset(code, quantity, value);
        _assetRepository.AddAsset(newAsset);

        return Result.Success("Ativo adicionado com sucesso.");
    }

    public Result SellAsset(string code, int quantity)
    {
        var asset = _assetRepository.GetAssets().FirstOrDefault(a => a.Code == code);
        
        if (asset == null)
        {
            return Result.Failure("Ativo não encontrado na carteira.");
        }

        if(asset.Quantity <= quantity)
        {
            return Result.Failure("Quantidade para venda excede a quantidade disponível na carteira.");
        }
        asset.Decrease(quantity);
        return Result.Success("Ativo vendido com sucesso.");
    }
}
