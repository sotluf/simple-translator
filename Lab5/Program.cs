using System.Text;

Console.InputEncoding = Console.OutputEncoding = Encoding.Unicode;

using (var httpClient = new HttpClient())
{
    var apiClient = new ApiClient(httpClient);

    var languagesResponse = await apiClient.GetAsync();
    if (languagesResponse.Data is { } languages)
    {
        foreach (var language in languages)
        {
            Console.WriteLine($"{language.LanguageName} ({language.CountryCode})");
        }
    }
    else
    {
        Console.WriteLine(
            $"""
             Помилка при отриманні списку мов, код: {languagesResponse.StatusCode}.
             {languagesResponse.Message}
             """
        );
        return;
    }

    Console.Write("Введіть код вхідної мови, наприклад, 'en': ");
    var sourceCountryCode = Console.ReadLine() ?? "";
    Console.Write("Введіть код вихідної мови, наприклад, 'ua': ");
    var targetCountryCode = Console.ReadLine() ?? "";
    Console.Write("Введіть текст: ");
    var sourceText = Console.ReadLine() ?? "";
    var translationResponse = await apiClient.PostAsync(sourceText, sourceCountryCode, targetCountryCode);
    if (translationResponse.Data is { } targetTexts)
    {
        Console.WriteLine("Переклад:");
        foreach (var targetText in targetTexts)
        {
            Console.WriteLine(targetText);
        }
    }
    else
    {
        Console.WriteLine(
            $"""
             Помилка при перекладі: {translationResponse.StatusCode}.
             {translationResponse.Message}
             """
        );
    }
}
