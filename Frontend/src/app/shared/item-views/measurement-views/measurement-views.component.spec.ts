import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeasurementViewsComponent } from './measurement-views.component';

describe('MeasurementViewsComponent', () => {
  let component: MeasurementViewsComponent;
  let fixture: ComponentFixture<MeasurementViewsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MeasurementViewsComponent]
    });
    fixture = TestBed.createComponent(MeasurementViewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
