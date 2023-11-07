using System.Text.Json.Serialization;

namespace OpenAIDallE3Console.Models;

/// <summary>
///     Represents a response from the 
///     DALL-E 3 model for image generation.
/// </summary>
internal class GenerateImageResponse
{
    /// <summary>
    ///     The URL of the generated image.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; } = null;

    /// <summary>
    ///     The revised prompt used for image generation.
    /// </summary>
    [JsonPropertyName("revised_prompt")]
    public string? RevisedPrompt { get; set; } = null;
}
