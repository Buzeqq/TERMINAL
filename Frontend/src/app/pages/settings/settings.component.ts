import {Component, Input} from '@angular/core';
import {SelectedItem} from "../../core/models/items/selected-item";

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent {
  private _selectedItem?: SelectedItem;

  @Input()
  get selectedItem() {
    return this._selectedItem;
  }
  set selectedItem(item: SelectedItem | undefined) {
    this._selectedItem = item;
  }
}
