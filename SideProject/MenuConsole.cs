using System.Globalization;

namespace SideProject;

public class MenuConsole
{
    private AssetRepository _assetRepository;

    public MenuConsole(AssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    public void ShowMenuConsole()
    {
        string option;

        do
        {
            Console.WriteLine("Bem-vindo ao menu de ativos! Escolha qual operação deseja fazer:");
            Console.WriteLine("1 - Adicionar ativos:");
            Console.WriteLine("2 - Visualizar ativos da carteira:");
            Console.WriteLine("3 - Vender ativos:");
            Console.WriteLine("4 - Extrato consolidado:");
            Console.WriteLine("5 - Sair");
            option = Console.ReadLine();
            Console.Clear();

            switch (option)
            {
                case "1":
                    BuyAssets();
                    break;
                case "2":
                    AssetDetails();
                    break;
                case "3":
                    SellAssets();
                    break;
                case "4":
                    ShowPortfolio();
                    break;
                case "5":
                    Console.WriteLine("Encerrando programa..");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
        while (option != "5");
    }
    internal void BuyAssets()
    {
        Console.Write("Digite o nome do ativo que você está comprando: ");
        var code = Console.ReadLine()!;

        Console.Write("Digite a quantidade do ativo que você deseja: ");
        var quantity = int.Parse(Console.ReadLine()!);

        Console.Write("Digite o valor do ativo comprado: ");
        string inputValue = Console.ReadLine()!;
        inputValue = inputValue.Replace(".", ",");
        var value = decimal.Parse(inputValue, new CultureInfo("pt-BR"));

        var newAsset = new Asset(code, quantity, value);

        _assetRepository.AddAsset(newAsset);

        Console.Clear();

        string response = OptionBuyAsset();

        if (response == "2")
        {
            Console.Clear();
            Console.WriteLine("Operação finalizada com sucesso! Aguarde enquanto você é redirecionado ao menu..");
            Thread.Sleep(3000);
            Console.Clear();
        }
        else if (response == "1")
        {
            Console.Clear();
            BuyAssets();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Opção inválida. Tente novamente!");
            BuyAssets();
        }
    }

    internal string OptionBuyAsset()
    {
        Console.WriteLine("Deseja comprar mais algum ativo?");
        Console.WriteLine("1 - Sim");
        Console.WriteLine("2 - Não");
        return Console.ReadLine();
    }

    public void AssetDetails()
    {
        var getAssets = _assetRepository.GetAssets();
        Console.WriteLine("Extrato de ativos investidos:");
        foreach (var asset in getAssets)
        {
            decimal totalAllocated = asset.Quantity * asset.Value;
            Console.WriteLine($"Código do ativo: {asset.Code}");
            Console.WriteLine($"Preço por unidade: {asset.Value.ToString("N2", new CultureInfo("pt-BR"))}");
            Console.WriteLine($"Quantidade: {asset.Quantity}");
            Console.WriteLine($"Valor total alocado: {totalAllocated.ToString("N2", new CultureInfo("pt-BR"))}");
            Console.WriteLine($"Data de compra: {asset.CreatedAt:dd/MM/yyyy HH:mm:ss}");
            Console.WriteLine("----------------------------");
        }
        Console.WriteLine("Pressione qualquer tecla para retornar ao menu...");
        Console.ReadKey();
        Console.Clear();
    }

    internal void SellAssets()
    {
        Console.Write("Digite o código do ativo que você está vendendo: ");
        string assetCode = Console.ReadLine();

        Console.Write("Digite a quantidade de ativos que você deseja vender: ");
        int assetQuantity = int.Parse(Console.ReadLine());

        var asset = _assetRepository.GetAssets().FirstOrDefault(a => a.Code == assetCode);
        if (asset == null)
        {
            Console.WriteLine("Ativo não encontrado na carteira.");
            Thread.Sleep(3000);
            return;
        }

        if (IsPossibleSellAsset(assetCode, assetQuantity))
        {
            DecreaseAssetQuantity(assetCode, assetQuantity);
            Console.WriteLine("Venda realizada com sucesso!");
        }
        else
        {
            Console.WriteLine("Quantidade para venda excede a quantidade disponível!");
        }

        Thread.Sleep(3000);
        Console.Clear();

        string response = OptionSellAsset();

        if (response == "2")
        {
            Console.Clear();
            Console.WriteLine("Operação finalizada com sucesso! Aguarde enquanto você é redirecionado ao menu..");
            Thread.Sleep(3000);
            Console.Clear();
        }
        else if (response == "1")
        {
            Console.Clear();
            SellAssets();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Opção inválida. Tente novamente!");
            SellAssets();
        }
    }

    internal static string OptionSellAsset()
    {
        Console.WriteLine("Deseja vender mais algum ativo?");
        Console.WriteLine("1 - Sim");
        Console.WriteLine("2 - Não");
        return Console.ReadLine();
    }

    public bool IsPossibleSellAsset(string assetCode, int assetQuantity)
    {
        var asset = _assetRepository.GetAssets().FirstOrDefault(a => a.Code == assetCode);

        if (asset.Quantity < assetQuantity)
        {
            return false;
        }
        return true;
    }

    public void DecreaseAssetQuantity(string assetCode, int assetQuantity)
    {
        var asset = _assetRepository.GetAssets().FirstOrDefault(a => a.Code == assetCode);
        asset.Decrease(assetQuantity);
    }

    public void ShowPortfolio() 
    { 
        var getAssets = _assetRepository.GetAssets();
        if (getAssets.Count == 0)
        {
            Console.WriteLine("Nenhum ativo encontrado na carteira");
            return;
        }

        decimal totalPortfolioValue = 0;
        Console.WriteLine("Extrato consolidado de ativos investidos:");
        foreach (var asset in getAssets)
        {
            decimal totalAlocated = asset.Quantity * asset.Value;
            totalPortfolioValue += totalAlocated;
            Console.WriteLine($"Código do ativo: {asset.Code} | Quantidade: {asset.Quantity} | Valor de cada ativo: {asset.Value.ToString("N2", new CultureInfo("pt-BR"))} | Total investido: {totalAlocated.ToString("N2", new CultureInfo("pt-BR"))}");
        }
        Console.WriteLine("----------------------------");
        Console.WriteLine($"Valor total investido na carteira: {totalPortfolioValue.ToString("N2", new CultureInfo("pt-BR"))}");
        Console.WriteLine("Pressione qualquer tecla para retornar ao menu...");
        Console.ReadKey();
        Console.Clear();
    }
}

