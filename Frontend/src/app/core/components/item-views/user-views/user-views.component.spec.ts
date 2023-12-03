import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserViewsComponent } from './user-views.component';

describe('UserViewsComponent', () => {
  let component: UserViewsComponent;
  let fixture: ComponentFixture<UserViewsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UserViewsComponent]
    });
    fixture = TestBed.createComponent(UserViewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
