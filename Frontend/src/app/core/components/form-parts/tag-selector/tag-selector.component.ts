import { Component, ElementRef, forwardRef, Input, OnInit, ViewChild } from '@angular/core';
import { TagsFormControl } from "../../../../pages/add-sample/types/addSampleTypes";
import { TagsService } from "../../../services/tags/tags.service";
import { SearchService } from "../../../services/search/search.service";
import { BehaviorSubject, combineLatestWith, debounceTime, filter, map, Observable, startWith, switchMap } from "rxjs";
import { Tag } from "../../../models/tags/tag";
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR } from "@angular/forms";
import { MatAutocompleteSelectedEvent } from "@angular/material/autocomplete";

@Component({
  selector: 'app-tag-selector',
  templateUrl: './tag-selector.component.html',
  styleUrls: ['./tag-selector.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TagSelectorComponent),
      multi: true
    }
  ]
})
export class TagSelectorComponent implements OnInit, ControlValueAccessor {
  @Input({ required: true })
  formControl?: TagsFormControl;

  constructor(private readonly tagService: TagsService,
              private readonly searchService: SearchService) {

  }

  private chosenTags = new BehaviorSubject<Tag[]>([]);

  public chosenTags$ = this.chosenTags.asObservable();
  public recentTags$ = this.tagService.getTags(0, 10).pipe(
    combineLatestWith(this.chosenTags$),
    map(([recentTags, chosenTags]) =>
      recentTags.filter(t1 => !chosenTags.find(t2 => t1.id === t2.id))),
  );
  public filteredTags$: Observable<Tag[]> = new Observable<Tag[]>();

  @ViewChild('tagInput') tagInput?: ElementRef<HTMLInputElement>;
  public tagFormControl = new FormControl<string>('');

  ngOnInit(): void {
    this.filteredTags$ = this.tagFormControl.valueChanges.pipe(
      startWith(''),
      debounceTime(500),
      filter(phrase => !!phrase),
      switchMap(phrase => this.searchService.searchTags(phrase!, 0, 10)),
      combineLatestWith(this.chosenTags$),
      map(([filteredTags, chosenTags]) =>
        filteredTags.filter(t1 => !chosenTags.find(t2 => t1.id === t2.id)))
    );
  }

  selectedTag(event: MatAutocompleteSelectedEvent) {
    const newTag = event.option.value;
    if (this.chosenTags.value.find(t => t.id === newTag)) {
      return;
    }

    this.chosenTags.next([newTag, ...this.chosenTags.value]);
    this.formControl?.setValue(this.chosenTags.value.map(t => t.id));
    this.tagInput!.nativeElement.value = '';
    this.tagFormControl.setValue('');
  }

  removeTag(tagId: string) {
    const tag = this.chosenTags.value.find(t => t.id == tagId);
    if (!tag) return;
    const index = this.chosenTags.value.indexOf(tag);
    if (index >= 0) {
      this.chosenTags.value.splice(index, 1)
      this.chosenTags.next(this.chosenTags.value);
      this.formControl!.setValue(this.chosenTags.value.map(t => t.id));
    }
  }

  onChange: any = () => {}
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  onTouch: any = () => {}
  registerOnTouched(fn: any): void {
    this.onTouch = fn;
  }

  val = "";
  set value(val: any) {
    if(val !== undefined && this.val !== val) {
      this.val = val;
      this.onChange(val);
      this.onTouch(val);
    }
  }
  writeValue(obj: any): void {

  }
}
