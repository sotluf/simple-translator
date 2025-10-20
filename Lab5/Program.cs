using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        var apiKey = Environment.GetEnvironmentVariable("DEEPL_API_KEY");

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            Console.WriteLine("API Key not found! Please set the DEEPL_API_KEY environment variable.");
            return;
        }

        var apiClient = new ApiClient(apiKey);

        string sourceLang = "";
        string targetLang = "";
        bool firstRun = true;

        while (true)
        {
            if (firstRun || string.IsNullOrWhiteSpace(sourceLang) || string.IsNullOrWhiteSpace(targetLang))
            {
                Console.Write("Enter source language code (e.g., EN): ");
                sourceLang = Console.ReadLine()?.Trim() ?? "";

                Console.Write("Enter target language code (e.g., NL): ");
                targetLang = Console.ReadLine()?.Trim() ?? "";

                firstRun = false;
            }

            Console.Write("Enter your text: ");
            var text = Console.ReadLine()?.Trim() ?? "";

            var response = await apiClient.TranslateAsync(text, sourceLang, targetLang);

            if (response.Data is { } translatedText)
            {
                Console.WriteLine("\nResult:");
                Console.WriteLine(translatedText);
            }
            else
            {
                Console.WriteLine($"\nTranslation error: {response.StatusCode}");
                Console.WriteLine(response.Message);
            }

            Console.WriteLine("\nMenu:");
            Console.WriteLine("1 - Translate another text with the same languages");
            Console.WriteLine("2 - Choose different languages");
            Console.WriteLine("0 - Exit");

            Console.Write("Select an option: ");
            var choice = Console.ReadLine()?.Trim() ?? "";

            switch (choice)
            {
                case "1":
                    break;
                case "2":
                    sourceLang = "";
                    targetLang = "";
                    break;
                case "0":
                    Console.WriteLine("Exiting program...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Exiting program...");
                    return;
            }

            Console.WriteLine();
        }
    }
}
