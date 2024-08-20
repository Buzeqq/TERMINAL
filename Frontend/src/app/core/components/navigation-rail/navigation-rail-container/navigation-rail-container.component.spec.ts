import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavigationRailContainerComponent } from './navigation-rail-container.component';

describe('NavigationRailContainerComponent', () => {
  let component: NavigationRailContainerComponent;
  let fixture: ComponentFixture<NavigationRailContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NavigationRailContainerComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(NavigationRailContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
