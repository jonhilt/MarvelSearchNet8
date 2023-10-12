using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace MarvelSearch.Backend;

public class MarvelApi
{
    private readonly string? _publicKey;
    private readonly string? _privateKey;
    private readonly HttpClient _httpClient;

    public MarvelApi(IConfiguration configuration, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _publicKey = configuration.GetValue<string>("MarvelAPIKey");
        _privateKey = configuration.GetValue<string>("MarvelAPIPrivateKey");
    }

    public async Task<MarvelSearchResult?> Search(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
            return new MarvelSearchResult();

        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
        var hash = GenerateHash(timestamp);

        var url = $"characters?nameStartsWith={searchTerm}&apikey={_publicKey}&hash={hash}&ts={timestamp}";

        return await _httpClient.GetFromJsonAsync<MarvelSearchResult>(url, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }

    private string GenerateHash(string timestamp)
    {
        var toBeHashed = timestamp + _privateKey + _publicKey;
        var hashBytes = MD5.HashData(Encoding.UTF8.GetBytes(toBeHashed));
        var sb = new StringBuilder();

        foreach (var hashByte in hashBytes)
        {
            sb.Append(hashByte.ToString("X2"));
        }

        return sb.ToString().ToLower(); // Marvel API requires lowercase hash
    }
}