import {Component, Input} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SelectedItem } from "../../core/models/items/selected-item";

@Component({
  selector: 'app-item-views',
  templateUrl: './item-views.component.html',
  styleUrls: ['./item-views.component.scss']
})
export class ItemViewsComponent {
  private _selectedItem: SelectedItem | undefined;

  constructor(
    protected readonly route: ActivatedRoute,
  ) {
    this.route.data.subscribe(data => {
      this._selectedItem = {type: data['type'], id: ''};
    });
  }

  @Input()
  get selectedItem() {
    return this._selectedItem;
  }
  set selectedItem(item: SelectedItem | undefined) {
    this._selectedItem = item;
  }
}
