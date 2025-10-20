using System;
using System.Threading.Tasks;
using DeepL;

internal class ApiClient
{
    private readonly DeepLClient _client;

    // Initialize DeepL client with the provided key
    public ApiClient(string authKey)
    {
        _client = new DeepLClient(authKey);
    }

    // Translate text from source to target language
    public async Task<ApiResponse<string>> TranslateAsync(string text, string sourceLang, string targetLang)
    {
        try
        {
            var translation = await _client.TranslateTextAsync(text, sourceLang, targetLang);
            return new ApiResponse<string>("The translation is successful.", translation.Text, System.Net.HttpStatusCode.OK);
        }
        catch (DeepLException ex)
        {
            return new ApiResponse<string>($"DeepL error: {ex.Message}", System.Net.HttpStatusCode.InternalServerError);
        }
        catch (Exception ex)
        {
            return new ApiResponse<string>($"Error: {ex.Message}", System.Net.HttpStatusCode.InternalServerError);
        }
    }

    // Generic API response class
    public class ApiResponse<T>
    {
        public string Message { get; }
        public T? Data { get; }
        public System.Net.HttpStatusCode StatusCode { get; }

        public ApiResponse(string message, T? data, System.Net.HttpStatusCode statusCode)
        {
            Message = message;
            Data = data;
            StatusCode = statusCode;
        }

        public ApiResponse(string message, System.Net.HttpStatusCode statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }
    }
}
