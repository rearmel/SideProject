using SideProject;
using SideProject.Repositories;

var assetRepository = new AssetRepository();

var menu = new MenuConsole(assetRepository);
menu.ShowMenuConsole();

