using System.Text.Json.Serialization;

namespace OpenAIDallE3Console.Models;

/// <summary>
///     Represents the root object of a 
///     response from the DALL-E 3 model 
///     for image generation.
/// </summary>
internal class GenerateImageRootResponse
{
    /// <summary>
    /// The timestamp of when the 
    /// response was created.
    /// </summary>
    [JsonPropertyName("created")]
    public long Created { get; set; }

    /// <summary>
    /// The array of generated images 
    /// and their metadata.
    /// </summary>
    [JsonPropertyName("data")]
    public GenerateImageResponse?[] Data { get; set; } = Array.Empty<GenerateImageResponse>();
}
