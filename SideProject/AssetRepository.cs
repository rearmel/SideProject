namespace SideProject
{
    public class AssetRepository
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
    }
}
