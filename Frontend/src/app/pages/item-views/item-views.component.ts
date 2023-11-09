import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-item-views',
  templateUrl: './item-views.component.html',
  styleUrls: ['./item-views.component.scss']
})
export class ItemViewsComponent {
  loading: 'determinate' | 'indeterminate' | 'buffer' | 'query' = 'query';

  constructor(
    protected readonly route: ActivatedRoute,
  ) {
    this.route.data.subscribe(data => {
      this._itemType = data['type'];
    });
  }

  private _selectedItemId: string | undefined;

  @Input()
  get selectedItemId(): string | undefined {
    return this._selectedItemId;
  }

  set selectedItemId(id: string | undefined) {
    this._selectedItemId = id;
  }

  private _itemType?: string;

  get itemType() {
    return this._itemType;
  }

  @Input()
  set itemType(type: string | undefined) {
    this._itemType = type;
  }

  loaded(): void {
    this.loading = 'determinate';
  }
}
