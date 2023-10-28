import {Component, Input} from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-item-views',
  templateUrl: './item-views.component.html',
  styleUrls: ['./item-views.component.scss']
})
export class ItemViewsComponent {
  constructor(
    protected readonly route: ActivatedRoute,
  ) {
    this.route.data.subscribe(data => {
      this._itemType = data['type'];
    });
  }

  loading: 'determinate' | 'indeterminate' | 'buffer' | 'query' = 'query';
  loaded(): void {
    this.loading = 'determinate';
  }

  
  @Input()
  get selectedItemId(): string | undefined{
    return this._selectedItemId;
  }

  set selectedItemId(id: string | undefined) {
    this._selectedItemId = id;
  }

  @Input()
  set itemType(type: string | undefined) {
    this._itemType = type;
  }

  get itemType() {
    return this._itemType;
  }

  private _selectedItemId: string | undefined;
  private _itemType?: string;
}
