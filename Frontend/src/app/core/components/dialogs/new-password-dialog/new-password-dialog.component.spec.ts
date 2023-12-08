import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewPasswordDialogComponent } from './new-password-dialog.component';

describe('NewPasswordDialogComponent', () => {
  let component: NewPasswordDialogComponent;
  let fixture: ComponentFixture<NewPasswordDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NewPasswordDialogComponent]
    });
    fixture = TestBed.createComponent(NewPasswordDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
