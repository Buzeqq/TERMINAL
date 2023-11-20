import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-svg',
  templateUrl: './svg.component.html',
  styleUrls: ['./svg.component.scss']
})
export class SvgComponent {
  private _height?: number;
  private _width?: number;
  private _path?: string;

  @Input()
  set height(h: number) {
    this._height = h
  }
  get height() {
    return this._height!;
  }

  @Input()
  set width(w: number) {
    this._width = w
  }
  get width() {
    return this._width!;
  }

  @Input()
  set path(p: string) {
    this._path = p
  }
  get path() {
    return this._path!;
  }
}
