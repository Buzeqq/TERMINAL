import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SampleDetailsDialogComponent } from './sample-details-dialog.component';

describe('SampleDetailsDialogComponent', () => {
  let component: SampleDetailsDialogComponent;
  let fixture: ComponentFixture<SampleDetailsDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SampleDetailsDialogComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(SampleDetailsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
