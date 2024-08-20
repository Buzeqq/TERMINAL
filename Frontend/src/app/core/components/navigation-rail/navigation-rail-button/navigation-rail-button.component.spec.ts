import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavigationRailButtonComponent } from './navigation-rail-button.component';

describe('NavigationRailButtonComponent', () => {
  let component: NavigationRailButtonComponent;
  let fixture: ComponentFixture<NavigationRailButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NavigationRailButtonComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(NavigationRailButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
