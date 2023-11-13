import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SelectedItem } from "../../../models/items/selected-item";

@Component({
  selector: 'app-item-details',
  templateUrl: './item-details.component.html',
  styleUrls: ['./item-details.component.scss']
})
export class ItemDetailsComponent {
  private _item?: SelectedItem;
  loading: 'determinate' | 'indeterminate' | 'buffer' | 'query' = 'query';

  constructor(
    protected readonly route: ActivatedRoute,
  ) {
    this.route.data.subscribe(data => {
      this._item = {type: data['type'], id: ''};
    });
  }

  get item() {
    return this._item;
  }
  @Input()
  set item(item: SelectedItem | undefined) {
    this._item = item;
  }
}
