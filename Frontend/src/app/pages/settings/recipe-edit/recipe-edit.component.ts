import {Component, Input} from '@angular/core';
import {catchError, EMPTY, Observable, tap} from "rxjs";
import {FormControl} from "@angular/forms";
import {whitespaceValidator} from "../../../core/components/validators/whitespaceValidator";
import {RecipesService} from "../../../core/services/recipes/recipes.service";
import {RecipeDetails} from "../../../core/models/recipes/recipeDetails";
import {NotificationService} from "../../../core/services/notification/notification.service";
import {MatDialog} from "@angular/material/dialog";
import {DeleteDialogComponent} from "../../../core/components/dialogs/delete-dialog/delete-dialog.component";

@Component({
  selector: 'app-recipe-edit',
  templateUrl: './recipe-edit.component.html',
  styleUrls: ['./recipe-edit.component.scss']
})
export class RecipeEditComponent {
  private _recipeId?: string;
  recipeDetails$: Observable<RecipeDetails> = new Observable<RecipeDetails>();
  private recipeDetails?: RecipeDetails;
  loading: 'determinate' | 'indeterminate' | 'buffer' | 'query' = 'query';

  recipeNameFormControl = new FormControl('',[whitespaceValidator])

  constructor(
    private readonly recipeService: RecipesService,
    private readonly notificationService: NotificationService,
    private readonly dialog: MatDialog,
  ) {  }

  @Input()
  get recipeId(): string | undefined {
    return this._recipeId;
  }

  set recipeId(id: string | undefined) {
    this._recipeId = id;
    this.recipeDetails$ = this.recipeService.getRecipe(this._recipeId!)
      .pipe(
        catchError((err, _) => {
          console.log(err);
          this.notificationService.notifyError('Failed to load recipe');
          return EMPTY;
        }),
        tap(r => {
          this.recipeDetails = r;
          this.resetForm();
          this.loading = 'determinate';
        })
      );
  }

  resetForm() {
    this.recipeNameFormControl.setValue(this.recipeDetails!.name);
  }

  readyToSubmit() {
    return this.dirtyForm() && this.recipeNameFormControl.valid;
  }

  dirtyForm() {
    return this.recipeDetails!.name !== this.recipeNameFormControl.value
  }

  editRecipe() {
    if (this.dirtyForm() && this.recipeDetails && this.recipeNameFormControl.value) {
      this.recipeService.editRecipe(this.recipeDetails.id, {
        id: this.recipeDetails.id,
        name: this.recipeNameFormControl.value,
        steps: this.recipeDetails.steps
      }).subscribe();
    }
    else
      this.notificationService.notifyError('Updating failed, check if provided value is valid.')
  }

  deleteRecipe() {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: {
        title: `Delete Recipe ${this.recipeDetails?.name}`,
        message: 'Attention! This action is irreversible.'
      }});
    dialogRef.afterClosed().subscribe(deleteConfirmed => {
      if (deleteConfirmed)
        this.recipeService.deleteRecipe(this._recipeId!, this.recipeDetails!.name).subscribe();
    })
  }
}
