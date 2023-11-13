import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatusbarComponent } from './statusbar.component';

describe('ToolbarComponent', () => {
  let component: StatusbarComponent;
  let fixture: ComponentFixture<StatusbarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [StatusbarComponent]
    });
    fixture = TestBed.createComponent(StatusbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
