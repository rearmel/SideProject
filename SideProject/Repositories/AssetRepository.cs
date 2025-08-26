using SideProject.Repositories.Interface;

namespace SideProject.Repositories;

public class AssetRepository : IAssetRepository
{
    private readonly List<Asset> _asset;

    public AssetRepository()
    {
        _asset = new List<Asset>();
    }

    public void AddAsset(Asset asset)
    {
        _asset.Add(asset);
    }

    public List<Asset> GetAssets()
    {
        return _asset;
    }

    public Asset? GetAssetByCode(string assetCode)
    {
        return _asset.FirstOrDefault(a => a.Code == assetCode);
    }

    public Asset RemoveAssetByCode(string assetCode)
    {
        var assetToRemove = _asset.FirstOrDefault(a => a.Code == assetCode);
        if (assetToRemove != null)
        {
            _asset.Remove(assetToRemove);
            return assetToRemove;
        }
        else
        {
            Console.WriteLine("Ativo não encontrado na carteira.");
            throw new AssetException("Ativo não encontrado na carteira.");
        }
    }
}


