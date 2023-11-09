import {Component, Input} from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-item-details',
  templateUrl: './item-details.component.html',
  styleUrls: ['./item-details.component.scss']
})
export class ItemDetailsComponent {
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
  get itemId(): string | undefined {
    return this._itemId;
  }

  set itemId(id: string | undefined) {
    this._itemId = id;
  }

  @Input()
  set itemType(type: string | undefined) {
    this._itemType = type;
  }

  get itemType() {
    return this._itemType;
  }

  private _itemId?: string;
  private _itemType?: string;
}
