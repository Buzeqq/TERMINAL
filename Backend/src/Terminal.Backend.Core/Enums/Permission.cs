namespace Terminal.Backend.Core.Enums;

public enum Permission
{
    UserRead = 1, UserWrite, UserUpdate, UserDelete, // User
    ProjectRead, ProjectWrite, ProjectUpdate, ProjectDelete, // Project
    RecipeRead, RecipeWrite, RecipeUpdate, RecipeDelete, // Recipe
    TagRead, TagWrite, TagUpdate, TagDelete, // Tag
    SampleRead, SampleWrite, SampleUpdate, SampleDelete, // Sample
    ParameterRead, ParameterWrite, ParameterUpdate, ParameterDelete, // Parameter
    StepRead, StepWrite, StepUpdate, StepDelete // Step
}