using OpenAIDallE3Console.Models;
using Spectre.Console;
using System.Net.Http.Headers;
using System.Net.Http.Json;

var isRunning = true;

while (isRunning)
{
    // Create header
    CreateHeader();

    // Get Token
    string token = GetToken();

    AnsiConsole.Clear();
    CreateHeader();

    // Get prompt
    string prompt = GetPrompt();

    AnsiConsole.Clear();
    CreateHeader();

    // Get quality
    string quality = GetQuality().ToLower();

    // Get size
    string size = GetSize();

    // Get style
    string style = GetStyle().ToLower();

    // LOGIC
    await AnsiConsole.Status()
        .StartAsync("Generating your image...", async ctx =>
        {
            string imageGenerationEndpoint = "https://api.openai.com/v1/images/generations";

            HttpClient client = new();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            GenerateImageRequest imageRequest = new()
            {
                Prompt = prompt,
                Quality = quality,
                Size = size,
                Style = style
            };

            HttpResponseMessage result = await client.PostAsJsonAsync(imageGenerationEndpoint, imageRequest);
            GenerateImageRootResponse? response = await result.Content.ReadFromJsonAsync<GenerateImageRootResponse>();

            // Show link
            var imageResponse = response?.Data?.FirstOrDefault();

            if (!string.IsNullOrEmpty(imageResponse?.RevisedPrompt))
            {
                AnsiConsole.MarkupLine($"The following prompt was used to create your image: [yellow]{response?.Data?.FirstOrDefault()?.RevisedPrompt}[/]");
                AnsiConsole.WriteLine();
            }

            if (!string.IsNullOrEmpty(imageResponse?.Url))
            {
                AnsiConsole.MarkupLine($"Your image is available at [link={imageResponse?.Url}]this link[/].");
            }
        });

    AnsiConsole.WriteLine();
    isRunning = AnsiConsole.Confirm("Do you want to create another image?", false);
}


/// <summary>
///     Creates the header for the console application.
/// </summary>
static void CreateHeader()
{
    // Create a grid for the header text
    Grid grid = new();
    grid.AddColumn();
    grid.AddRow(new FigletText("DALL-E 3").Centered().Color(Color.Red));
    grid.AddRow(Align.Center(new Panel("[red]Sample by Thomas Sebastian Jensen ([link]https://www.tsjdev-apps.de[/])[/]")));

    // Write the grid to the console
    AnsiConsole.Write(grid);
    AnsiConsole.WriteLine();
}

/// <summary>
///     Prompts the user for their OpenAI API key.
/// </summary>
/// <returns>The user's OpenAI API key.</returns>
static string GetToken()
    => AnsiConsole.Prompt(
        new TextPrompt<string>("Please insert your [yellow]OpenAI Key[/]:")
        .PromptStyle("white")
        .ValidationErrorMessage("[red]Invalid prompt[/]")
        .Validate(prompt =>
        {
            if (prompt.Length < 3)
            {
                return ValidationResult.Error("[red]API key too short[/]");
            }

            if (prompt.Length > 200)
            {
                return ValidationResult.Error("[red]API key too long[/]");
            }

            return ValidationResult.Success();
        }));

/// <summary>
///     Prompts the user for the prompt to use for image generation.
/// </summary>
/// <returns>The user's prompt for image generation.</returns>
static string GetPrompt()
    => AnsiConsole.Prompt(
        new TextPrompt<string>("Please insert your [yellow]prompt[/]:")
        .PromptStyle("white")
        .ValidationErrorMessage("[red]Invalid prompt[/]")
        .Validate(prompt =>
        {
            if (prompt.Length < 3)
            {
                return ValidationResult.Error("[red]Prompt too short[/]");
            }

            if (prompt.Length > 4096)
            {
                return ValidationResult.Error("[red]Prompt too long[/]");
            }

            return ValidationResult.Success();
        }));

/// <summary>
///     Prompts the user for the quality of the generated image.
/// </summary>
/// <returns>The user's selected quality for the generated image.</returns>
static string GetQuality()
    => AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title("Please select the [yellow]quality[/] of your image.")
        .PageSize(10)
        .AddChoices(new[] {
            "STANDARD", "HD"
        }));

/// <summary>
///     Prompts the user for the size of the generated image.
/// </summary>
/// <returns>The user's selected size for the generated image.</returns>
static string GetSize()
    => AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title("Please select the [yellow]size[/] of your image.")
        .PageSize(10)
        .AddChoices(new[] {
            "1024x1024", "1792x1024", "1024x1792"
        }));

/// <summary>
///     Prompts the user for the style of the generated image.
/// </summary>
/// <returns>The user's selected style for the generated image.</returns>
static string GetStyle()
    => AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title("Please select the [yellow]style[/] of your image.")
        .PageSize(10)
        .AddChoices(new[] {
            "VIVID", "NATURAL"
        }));