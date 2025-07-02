namespace SideProject
{
    public class MenuConsole
    {
        private AssetRepository _assetRepository;

        public MenuConsole(AssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public static void ShowMenuConsole()
        {
            var assetRepository = new AssetRepository();
            string option;

            do
            {
                Console.WriteLine("Bem-vindo ao menu de ativos! Escolha qual operação deseja fazer:");
                Console.WriteLine("1 - Adicionar ativos:");
                Console.WriteLine("2 - Visualizar ativos da carteira:");
                Console.WriteLine("3 - Opção 3");
                Console.WriteLine("4 - Sair");
                option = Console.ReadLine();
                Console.Clear();

                switch (option)
                {
                    case "1":
                        var option1 = new MenuConsole(assetRepository);
                        option1.InputAssets();
                        break;
                    case "2":
                        var option2 = new MenuConsole(assetRepository);
                        option2.AssetDetails();
                        break;
                    case "3":
                        var option3 = new Option3();
                        option3.Execute();
                        break;
                    case "4":
                        Console.WriteLine("Encerrando programa..");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            } 
            while (option != "4");
        }
        internal void InputAssets()
        {
            var newAsset = new Asset();

            Console.Write("Digite o nome do ativo que você está comprando: ");
            newAsset.AssetCode = Console.ReadLine();

            Console.Write("Digite a quantidade do ativo que você deseja: ");
            newAsset.AssetQuantity = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor do ativo comprado: ");
            newAsset.AssetValue = decimal.Parse(Console.ReadLine());

            newAsset.DateTime = DateTime.Now;

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
                InputAssets();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Opção inválida. Tente novamente!");
                InputAssets();
            }
        }

        internal static string OptionBuyAsset()
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
                decimal totalAllocated = asset.AssetQuantity * asset.AssetValue;
                Console.WriteLine($"Código do ativo: {asset.AssetCode}");
                Console.WriteLine($"Preço por unidade: {asset.AssetValue}");
                Console.WriteLine($"Quantidade: {asset.AssetQuantity}");
                Console.WriteLine($"Valor total alocado: {totalAllocated}");
                Console.WriteLine($"Data de compra: {asset.DateTime:dd/MM/yyyy HH:mm:ss}");
                Console.WriteLine("----------------------------");
            }
            Console.WriteLine("Pressione qualquer tecla para retornar ao menu...");
            Console.ReadKey(); 
            Console.Clear();
        }
    }
}

