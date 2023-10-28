import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl} from "@angular/forms";
import {ActivatedRoute, ParamMap, Router} from "@angular/router";

@Component({
  selector: 'app-quick-actions',
  templateUrl: './quick-actions.component.html',
  styleUrls: ['./quick-actions.component.scss']
})
export class QuickActionsComponent implements OnInit {
  private defaultFilterValue = true;

  searchControl= new FormControl<string>('');
  searchFilters = this.formBuilder.group({
    projects: this.defaultFilterValue,
    measurements: this.defaultFilterValue,
    recipes: this.defaultFilterValue
  });

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute
  ) {  }

  /*
  After navigating to /search, a new quick-actions component is created,
  so in order to preserve entered search expression and filters we need to initialize them in ngOnInit.
  (I have no better idea for now)
  */
  ngOnInit(): void {
    this.route.queryParamMap.subscribe((params: ParamMap) => {
      let searchPhrase = params.get('q');
      this.searchControl.setValue(searchPhrase ? searchPhrase : '');

      let projectsFilter = params.get('projects');
      let measurementsFilter = params.get('measurements');
      let recipeFilter = params.get('recipes');

      this.searchFilters.setValue({
        projects: JSON.parse(projectsFilter ? projectsFilter : String(this.defaultFilterValue)),
        measurements: JSON.parse(measurementsFilter ? measurementsFilter : String(this.defaultFilterValue)),
        recipes: JSON.parse(recipeFilter ? recipeFilter : String(this.defaultFilterValue))
      })
    })
  }

  goToSearch() {
    let phrase = this.searchControl.getRawValue();
    let queryParams = {
      queryParams: {
        q: phrase ? phrase : '',
        projects: this.searchFilters.value.projects,
        measurements: this.searchFilters.value.measurements,
        recipes: this.searchFilters.value.recipes,
      }
    }

    this.router.navigate(['search'], queryParams);
  }

  clearSearchControl() {
    this.searchControl.reset();
  }
}
