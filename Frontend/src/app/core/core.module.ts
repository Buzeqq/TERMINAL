import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TimeAgoPipe } from './pipes/time-ago.pipe';



@NgModule({
  declarations: [
    TimeAgoPipe
  ],
  exports: [
    TimeAgoPipe
  ],
  imports: [
    CommonModule
  ]
})
export class CoreModule { }
