import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipeViewsComponent } from './recipe-views.component';

describe('RecipeViewsComponent', () => {
  let component: RecipeViewsComponent;
  let fixture: ComponentFixture<RecipeViewsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RecipeViewsComponent]
    });
    fixture = TestBed.createComponent(RecipeViewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
