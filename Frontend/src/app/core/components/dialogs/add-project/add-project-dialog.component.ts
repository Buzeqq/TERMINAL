import { Component } from "@angular/core";
import { ProjectsService } from "../../../services/projects/projects.service";
import { FormControl, Validators } from "@angular/forms";
import { FormMatcher } from "../../matchers/formMatcher";
import { whitespaceValidator } from "../../validators/whitespaceValidator";

@Component({
  selector: 'app-add-project-dialog',
  templateUrl: './add-project-dialog.component.html',
  styleUrls: ['./add-project-dialog.component.scss'],
})
export class AddProjectDialogComponent {
  min = 3;
  max = 50;
  newProjectNameControl = new FormControl('', [
    Validators.required,
    Validators.minLength(this.min),
    Validators.maxLength(this.max),
    whitespaceValidator
  ]);
  matcher = new FormMatcher();
  newProjectName = '';

  constructor(private readonly projectService: ProjectsService) {
  }

  addProject() {
    this.projectService.addProject(this.newProjectName).subscribe();
  }
}
