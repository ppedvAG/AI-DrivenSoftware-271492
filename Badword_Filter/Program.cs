namespace Badword_Filter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var filter = new NicknameFilter("blocked-terms.json");

            Console.WriteLine("Nickname eingeben");
            var nickname = Console.ReadLine();
            bool allowed = filter.IsNicknameAllowed(nickname ?? "");

            if (!allowed)
            {
                Console.WriteLine("Nickname nicht erlaubt.");
            } 
            else
            {
                Console.WriteLine($"Hallo {nickname}!");
            }
        }
    }
}
