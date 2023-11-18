import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TagEditComponent } from './tag-edit.component';

describe('TagEditComponent', () => {
  let component: TagEditComponent;
  let fixture: ComponentFixture<TagEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TagEditComponent]
    });
    fixture = TestBed.createComponent(TagEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
