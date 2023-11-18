import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeasurementEditComponent } from './measurement-edit.component';

describe('MeasurementEditComponent', () => {
  let component: MeasurementEditComponent;
  let fixture: ComponentFixture<MeasurementEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MeasurementEditComponent]
    });
    fixture = TestBed.createComponent(MeasurementEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
