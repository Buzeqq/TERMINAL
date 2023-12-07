namespace Terminal.Backend.Application.DTO.Recipes;

public class GetRecipesDto
{
    public IEnumerable<RecipeDto> Recipes { get; set; }

    public record RecipeDto(Guid Id, string Name);
}