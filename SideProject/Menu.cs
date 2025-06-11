namespace SideProject
{
    public class Menu
    {
        public static void ShowMenu()
        {
            Console.WriteLine("Bem-vindo ao menu de ativos! Escolha qual operação deseja fazer:");
            Console.WriteLine("1 - Opção 1");
            Console.WriteLine("2 - Opção 2");
            Console.WriteLine("3 - Opção 3");
            Console.WriteLine("4 - Sair");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    var option1 = new option1();
                    option1.Execute();
                    ShowMenu();
                    break;
                case "2":
                    var option2 = new option2();
                    option2.Execute();
                    ShowMenu();
                    break;
                case "3":
                    var option3 = new option3();
                    option3.Execute();
                    ShowMenu();
                    break;
                case "4":
                    return; 
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    ShowMenu();
                    break;
            }
        }
    }
}
