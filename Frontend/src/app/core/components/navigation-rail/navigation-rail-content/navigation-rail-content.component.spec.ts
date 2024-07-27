import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavigationRailContentComponent } from './navigation-rail-content.component';

describe('NavigationRailContentComponent', () => {
  let component: NavigationRailContentComponent;
  let fixture: ComponentFixture<NavigationRailContentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NavigationRailContentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NavigationRailContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
