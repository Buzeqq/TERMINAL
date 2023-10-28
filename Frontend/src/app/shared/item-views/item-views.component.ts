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

  selectedItemId: string | undefined;

  @Input()
  set itemType(type: string | undefined) {
    this._itemType = type;
  }

  get itemType() {
    return this._itemType;
  }

  private _itemType?: string;
}
