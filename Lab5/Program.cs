using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        Console.Write("Enter your DeepL API Key: ");
        var apiKey = Console.ReadLine()?.Trim() ?? "";

        var apiClient = new ApiClient(apiKey);

        Console.Write("Enter source language code (e.g., EN): ");
        var sourceLang = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Enter target language code (e.g., UK): ");
        var targetLang = Console.ReadLine()?.Trim() ?? "";

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
            Console.WriteLine($"\nTranslation error:: {response.StatusCode}");
            Console.WriteLine(response.Message);
        }

    }
}
