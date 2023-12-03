import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SelectedItem } from "../../models/items/selected-item";
import { BreakpointObserver } from '@angular/cdk/layout';

@Component({
  selector: 'app-item-views',
  templateUrl: './item-views.component.html',
  styleUrls: ['./item-views.component.scss']
})
export class ItemViewsComponent {
  private _selectedItem: SelectedItem | undefined;

  constructor(
    protected readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly breakpointObserver: BreakpointObserver
  ) {
    this.route.data.subscribe(data => {
      this._selectedItem = { type: data['type'], id: '' };
    });
  }

  @Input()
  get selectedItem() {
    return this._selectedItem;
  }
  set selectedItem(item: SelectedItem | undefined) {
    // TODO: details should be expandable on mobile but
    // https://stackblitz.com/angular/ygdrrokyvkv?file=app%2Ftable-expandable-rows-example.html
    // doesn't work :// so we just navigate to the sample page
    // no time to waste on this
    this._selectedItem = item;
    if (!item?.config?.init){ // init is used to prevent navigation on view load
      this.routeToItemDetails(item!);
    } 
  }

  routeToItemDetails(item: SelectedItem) {
    this.breakpointObserver.observe('(max-width: 768px)').subscribe(result => {
      if (result.matches) {
        let path = item.type.toLowerCase() + 's';
        this.router.navigate([path, item.id]);
      }
    });
  }

}
