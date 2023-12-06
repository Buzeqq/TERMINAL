import {Injectable} from '@angular/core';
import {db} from "../../../db/terminal-db";
import {Recipe} from "../../../models/recipes/recipe";
import {NotFoundError} from "rxjs";
import {RecipeDetails} from "../../../models/recipes/recipeDetails";
import {RecipeEntity} from "../../../db/tables/recipe-entity";

@Injectable({
  providedIn: 'root'
})
export class IdbRecipesService {

  constructor() { }

  async getRecipes(pageIndex: number, pageSize: number, desc = true) {
    let query = db.recipes.orderBy('name');
    if (desc) query = query.reverse();
    return query
      .offset(pageIndex)
      .limit(pageSize)
      .toArray();
  }

  async getRecipe(id: string) : Promise<RecipeEntity> {
    const recipe = await db.recipes.where('id').equals(id).first();
    if (recipe == undefined) throw NotFoundError;
    return recipe;
  }

  async searchRecipes(searchPhrase: string, pageIndex: number, pageSize: number) {
    const offset = pageIndex * pageSize;
    return db.recipes
      .filter((recipe) => (
        recipe.name.toLowerCase().includes(searchPhrase.toLowerCase()))
      )
      .offset(offset)
      .limit(pageSize)
      .toArray();
  }

  async addRecipes(recipes: Recipe[]) {
    /* Table.bulkPut() would overwrite steps with an empty list */
    for (const newRecipe of recipes) {
      const recipe = await db.recipes.get(newRecipe.id);
      if (recipe) db.recipes.put({id: newRecipe.id, name: newRecipe.name, steps: recipe.steps})
      else db.recipes.add({...newRecipe, steps: []})
    }
  }

  addRecipe(recipe: RecipeDetails) {
    db.recipes.put(recipe);
  }

  async getRecipesAmount() {
    return db.recipes.count();
  }
}
