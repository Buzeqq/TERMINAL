import {Component, Input} from '@angular/core';
import {SelectedItem} from "../../core/models/items/selected-item";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent {
  private _selectedItem?: SelectedItem;
  tabIndex = 0;

  constructor (
    private readonly route: ActivatedRoute
  ) {
    this.route.queryParamMap.subscribe(params => {
      const focusOn = params.get('tab');
      if (focusOn == 'Samples')
        this.tabIndex = 1;
    })
  }

  @Input()
  get selectedItem() {
    return this._selectedItem;
  }
  set selectedItem(item: SelectedItem | undefined) {
    this._selectedItem = item;
  }
}
