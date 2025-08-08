using SideProject;

var asset = new Asset();

var assetRepository = new AssetRepository();

var menu = new MenuConsole(assetRepository);
menu.ShowMenuConsole();

