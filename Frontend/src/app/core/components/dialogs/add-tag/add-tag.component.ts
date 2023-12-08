import { Component } from "@angular/core";
import { FormControl, Validators } from "@angular/forms";
import { TagsService } from "src/app/core/services/tags/tags.service";

@Component({
  selector: 'app-add-tag',
  templateUrl: './add-tag.component.html',
  styleUrls: ['./add-tag.component.scss'],
})
export class AddTagComponent {
  tagNameControl = new FormControl('', [
    Validators.required,
  ]);
  tagName = '';

  constructor(private readonly tagsService: TagsService) {
  }

  addTag() {
    this.tagsService.createTag(this.tagName).subscribe();
  }
}
