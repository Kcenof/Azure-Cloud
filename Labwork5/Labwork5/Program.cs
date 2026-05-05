using System;
using Azure;
using System.Text;
using Azure.AI.TextAnalytics;

namespace EntityRecognitionConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            string endpoint = "https://labwork4.cognitiveservices.azure.com/";
            string apiKey = "ADCcdDRiaBuGRNvi26qo3kH671HqzG1UoWiQ0WL2Ot12WYdAVRZoJQQJ99CEACi5YpzXJ3w3AAAaACOGquUR";

            var client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(apiKey));

            Console.WriteLine("Введіть текст для аналізу:");
            string userInput = Console.ReadLine();

            Console.WriteLine("\nОбробка...\n");

            try
            {
                LinkedEntityCollection linkedEntities = client.RecognizeLinkedEntities(userInput);

                Console.WriteLine(new string('-', 100));
                Console.WriteLine($"| {"Сутність",-25} | {"URL",-68} |");
                Console.WriteLine(new string('-', 100));

                foreach (LinkedEntity entity in linkedEntities)
                {
                    Console.WriteLine($"| {entity.Name,-25} | {entity.Url,-68} |");
                }
                Console.WriteLine(new string('-', 100));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Сталася помилка: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}