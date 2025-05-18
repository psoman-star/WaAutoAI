// See https://aka.ms/new-console-template for more information
using System.Text;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WhatsAppConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Please enter the WhatsApp phone number (in international format, without '+', e.g., 8613812345678):");
            string phone = Console.ReadLine()?.Trim();

            Console.WriteLine("Please enter the message to send:");
            string message = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine("Phone number and message cannot be empty!");
                return;
            }

            try
            {
                await SendWhatsAppMessage(phone, message);
                Console.WriteLine("Message sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send message: " + ex.Message);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static async Task SendWhatsAppMessage(string phone, string message)
        {
            using (var client = new HttpClient())
            {
                var requestBody = new
                {
                    phone = phone,
                    message = message
                };

                string json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:3000/send-message", content);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
