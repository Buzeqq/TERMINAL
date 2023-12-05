import { Injectable } from '@angular/core';
import {db} from "../../../db/terminal-db";
import {Recipe} from "../../../models/recipes/recipe";

@Injectable({
  providedIn: 'root'
})
export class IdbRecipesService {

  constructor() { }

  async addRecipes(recipes: Recipe[]) {
    db.recipes.bulkPut(recipes)
  }

}
