using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        Console.Write("Введіть свій DeepL API Key: ");
        var apiKey = Console.ReadLine()?.Trim() ?? "";

        var apiClient = new ApiClient(apiKey);

        Console.Write("Введіть код мови джерела (EN, DE, FR, UK…): ");
        var sourceLang = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Введіть код мови перекладу (UK, EN, DE…): ");
        var targetLang = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Введіть текст для перекладу: ");
        var text = Console.ReadLine()?.Trim() ?? "";

        var response = await apiClient.TranslateAsync(text, sourceLang, targetLang);

        if (response.Data is { } translatedText)
        {
            Console.WriteLine("\nПереклад:");
            Console.WriteLine(translatedText);
        }
        else
        {
            Console.WriteLine($"\nПомилка перекладу: {response.StatusCode}");
            Console.WriteLine(response.Message);
        }

        Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
        Console.ReadKey();
    }
}
