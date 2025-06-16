namespace SideProject
{
    public class Menu
    {
        public static void ShowMenu()
        { 

            do
            {
                Console.WriteLine("Bem-vindo ao menu de ativos! Escolha qual operação deseja fazer:");
                Console.WriteLine("1 - Opção 1");
                Console.WriteLine("2 - Opção 2");
                Console.WriteLine("3 - Opção 3");
                Console.WriteLine("4 - Sair");
                var option = Console.ReadLine();
                Console.Clear();
                switch (option)
                {
                    case "1":
                        var option1 = new Option1();
                        option1.Execute();
                        break;
                    case "2":
                        var option2 = new Option2();
                        option2.Execute();
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
            while (true);
        }
    }
}
