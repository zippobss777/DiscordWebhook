using static System.Net.WebRequestMethods;
using System.Text.Json;
using System.Text;
using System.Net.Http;

namespace DiscordWebhook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("┌------------------------┐");
            Console.Write("|      ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("ZippoWebhook");
            Console.ForegroundColor= ConsoleColor.White;
            Console.WriteLine("      |");
            Console.WriteLine("└------------------------┘");
            Console.Write("\nWebhook link: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            string webhook = Console.ReadLine();

            if (!webhook.StartsWith("https://discord.com/api/webhooks/"))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("┌------------------------┐");
                Console.Write("|      ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("ZippoWebhook");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("      |");
                Console.WriteLine("└------------------------┘");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nBłąd: Podany link nie jest poprawnym linkiem do webhooka Discorda!");
                Console.ForegroundColor= ConsoleColor.DarkGray;
                return;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nTreść wiadomości: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            string wiadomosc = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nOpcjonalna nazwa webhooka (domyślnie: 'ZippoWebhook'): ");
            Console.ForegroundColor = ConsoleColor.Gray;
            string nick = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nick))
            {
                nick = "ZippoWebhook";
            }

            WebhookMessage wiadomoscDiscord = new WebhookMessage();
            wiadomoscDiscord.content = wiadomosc;
            wiadomoscDiscord.username = nick;

            string jsonPayload = JsonSerializer.Serialize(wiadomoscDiscord);

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.PostAsync(webhook, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("┌------------------------┐");
                    Console.Write("|      ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("ZippoWebhook");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("      |");
                    Console.WriteLine("└------------------------┘");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nWiadomość została pomyślnie wysłana na serwer Discord!");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("┌------------------------┐");
                    Console.Write("|      ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("ZippoWebhook");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("      |");
                    Console.WriteLine("└------------------------┘");
                    Console.ForegroundColor= ConsoleColor.DarkRed;
                    Console.WriteLine($"\nWystąpił błąd podczas wysyłania: {response.StatusCode}");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
            }
        }
    }

    public class WebhookMessage
    {
        public string content { get; set; }
        public string username { get; set; }
        public string avatar_url { get; set; }
    }
}