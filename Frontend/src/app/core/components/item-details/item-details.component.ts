import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-item-details',
  templateUrl: './item-details.component.html',
  styleUrls: ['./item-details.component.scss']
})
export class ItemDetailsComponent {
  loading: 'determinate' | 'indeterminate' | 'buffer' | 'query' = 'query';

  constructor(
    protected readonly route: ActivatedRoute,
  ) {
    this.route.data.subscribe(data => {
      this._itemType = data['type'];
    });
  }

  private _itemId?: string;

  @Input()
  get itemId(): string | undefined {
    return this._itemId;
  }

  set itemId(id: string | undefined) {
    this._itemId = id;
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
