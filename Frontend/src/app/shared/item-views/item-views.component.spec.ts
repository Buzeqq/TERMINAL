import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemViewsComponent } from './item-views.component';

describe('ItemViewsComponent', () => {
  let component: ItemViewsComponent;
  let fixture: ComponentFixture<ItemViewsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ItemViewsComponent]
    });
    fixture = TestBed.createComponent(ItemViewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
