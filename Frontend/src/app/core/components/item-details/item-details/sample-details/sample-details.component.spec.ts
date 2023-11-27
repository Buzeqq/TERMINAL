import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SampleDetailsComponent } from './sample-details.component';

describe('SampleDetailsComponent', () => {
  let component: SampleDetailsComponent;
  let fixture: ComponentFixture<SampleDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SampleDetailsComponent]
    });
    fixture = TestBed.createComponent(SampleDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
