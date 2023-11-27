import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SampleEditComponent } from './sample-edit.component';

describe('SampleEditComponent', () => {
  let component: SampleEditComponent;
  let fixture: ComponentFixture<SampleEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SampleEditComponent]
    });
    fixture = TestBed.createComponent(SampleEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
