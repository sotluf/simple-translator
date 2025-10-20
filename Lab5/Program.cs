using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        var apiKey = Environment.GetEnvironmentVariable("DEEPL_API_KEY");

        // Check if API key is provided
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
            // Ask for languages if it's the first run or user chose to change languages
            if (firstRun || string.IsNullOrWhiteSpace(sourceLang) || string.IsNullOrWhiteSpace(targetLang))
            {
                Console.Write("Enter source language code (e.g., EN): ");
                sourceLang = Console.ReadLine()?.Trim() ?? "";

                Console.Write("Enter target language code (e.g., NL): ");
                targetLang = Console.ReadLine()?.Trim() ?? "";

                firstRun = false;
            }

            // Ask for text to translate
            Console.Write("Enter your text: ");
            var text = Console.ReadLine()?.Trim() ?? "";

            // Perform translation
            var response = await apiClient.TranslateAsync(text, sourceLang, targetLang);

            // Display result or error
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

            // Menu options
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1 - Translate another text with the same languages");
            Console.WriteLine("2 - Choose different languages");
            Console.WriteLine("0 - Exit");

            Console.Write("Select an option: ");
            var choice = Console.ReadLine()?.Trim() ?? "";

            switch (choice)
            {
                case "1": // Continue with same languages
                    break;
                case "2": // Change languages
                    sourceLang = "";
                    targetLang = "";
                    break;
                case "0": // Exit
                    Console.WriteLine("Exiting program...");
                    return;
                default: // Invalid option
                    Console.WriteLine("Invalid option. Exiting program...");
                    return;
            }

            Console.WriteLine();
        }
    }
}
