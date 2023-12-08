import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTagComponent } from './add-tag.component';

describe('AddTagComponent', () => {
  let component: AddTagComponent;
  let fixture: ComponentFixture<AddTagComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddTagComponent]
    });
    fixture = TestBed.createComponent(AddTagComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
