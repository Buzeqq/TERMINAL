namespace Terminal.Backend.Application.DTO.Recipes;

public class GetRecipesDto
{
    public IEnumerable<GetRecipeDto> Recipes { get; set; }
}