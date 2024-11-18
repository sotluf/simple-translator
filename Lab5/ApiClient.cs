using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Lab5;
using static Statics;

internal class ApiClient(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;


    public async Task<ApiResponse<Language>> GetAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var request = CreateRequest(HttpMethod.Get, "languages");
            var response = await _httpClient.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<Language>(response.ReasonPhrase ?? "An error occured.", response.StatusCode);
            }

            var json = (await response.Content.ReadFromJsonAsync<GetMethodContent>(
                Statics.JsonSerializerOptions, cancellationToken
            ))!;
            return new ApiResponse<Language>("Request successful.", json.Languages, response.StatusCode);
        }
        catch (Exception exception)
        {
            return new ApiResponse<Language>(exception.Message, HttpStatusCode.InternalServerError);
        }
    }


    public async Task<ApiResponse<string>> PostAsync(
        string text,
        string sourceCountryCode,
        string targetCountryCode,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var request = CreateRequest(
                HttpMethod.Post,
                $"translate?source={sourceCountryCode}&target={targetCountryCode}"
            );
            request.Content = new StringContent(text);
            var response = await _httpClient.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<string>(response.ReasonPhrase ?? "An error occured.", response.StatusCode);
            }

            var json = (await response.Content.ReadFromJsonAsync<PostMethodContent>(
                Statics.JsonSerializerOptions, cancellationToken
            ))!;
            return new ApiResponse<string>(
                "Request successful.",
                [..json.Translations.Select(ti => ti.Translation)],
                response.StatusCode
            );
        }
        catch (Exception exception)
        {
            return new ApiResponse<string>(exception.Message, HttpStatusCode.InternalServerError);
        }
    }
}

file static class Statics
{
    private const string ApiKey = "CEQRdhEP01NDjs8DDelT1F2gyIyEoHtz";
    private static readonly Uri BaseUri = new("https://api.apilayer.com/language_translation/");


    public static JsonSerializerOptions JsonSerializerOptions { get; } = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };


    public static HttpRequestMessage CreateRequest(HttpMethod method, string relativePath)
    {
        var message = new HttpRequestMessage(method, new Uri(BaseUri, relativePath));
        message.Headers.Add("apikey", ApiKey);
        return message;
    }
}
