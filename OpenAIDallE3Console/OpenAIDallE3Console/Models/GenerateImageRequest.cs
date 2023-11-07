using System.Text.Json.Serialization;

namespace OpenAIDallE3Console.Models;


/// <summary>
///     Represents a request to generate an image 
///     using DALL-E 3 model.
/// </summary>
internal class GenerateImageRequest
{
    /// <summary>
    ///     The prompt for the image generation.
    /// </summary>
    [JsonPropertyName("prompt")]
    public required string Prompt { get; set; }

    /// <summary>
    ///     The name of the model to use for 
    ///     image generation.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; } = "dall-e-3";

    /// <summary>
    ///     The number of images to generate.
    /// </summary>
    [JsonPropertyName("n")]
    public int Amount { get; } = 1;

    /// <summary>
    ///     The quality of the generated image.
    /// </summary>
    [JsonPropertyName("quality")]
    public string Quality { get; set; } = "standard";

    /// <summary>
    ///     The format of the response.
    /// </summary>
    [JsonPropertyName("response_format")]
    public string ResponseFormat { get; } = "url";

    /// <summary>
    ///     The size of the generated image.
    /// </summary>
    [JsonPropertyName("size")]
    public string Size { get; set; } = "1024x1024";

    /// <summary>
    ///     The style of the generated image.
    /// </summary>
    [JsonPropertyName("style")]
    public string Style { get; set; } = "vivid";

    /// <summary>
    ///     The user requesting the image generation.
    /// </summary>
    [JsonPropertyName("user")]
    public string User { get; set; } = string.Empty;
}
