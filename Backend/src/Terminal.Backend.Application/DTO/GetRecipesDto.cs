namespace Terminal.Backend.Application.DTO;

public class GetRecipesDto
{
    public IEnumerable<RecipeDto> Recipes { get; set; }
}