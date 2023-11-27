import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SampleViewsComponent } from './sample-views.component';

describe('SampleViewsComponent', () => {
  let component: SampleViewsComponent;
  let fixture: ComponentFixture<SampleViewsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SampleViewsComponent]
    });
    fixture = TestBed.createComponent(SampleViewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
