import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StepsCreatorComponent } from './steps-creator.component';

describe('StepsCreatorComponent', () => {
  let component: StepsCreatorComponent;
  let fixture: ComponentFixture<StepsCreatorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [StepsCreatorComponent]
    });
    fixture = TestBed.createComponent(StepsCreatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
