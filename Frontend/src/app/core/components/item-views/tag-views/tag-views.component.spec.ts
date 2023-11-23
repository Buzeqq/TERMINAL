import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TagViewsComponent } from './tag-views.component';

describe('TagViewsComponent', () => {
  let component: TagViewsComponent;
  let fixture: ComponentFixture<TagViewsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TagViewsComponent]
    });
    fixture = TestBed.createComponent(TagViewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
