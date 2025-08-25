namespace SideProject.Repositories.Interface;

public interface IAssetRepository
{
    void AddAsset(Asset asset);
    List<Asset> GetAssets();
    Asset? GetAssetByCode(string assetCode);
    Asset RemoveAssetByCode(string assetCode);
}
