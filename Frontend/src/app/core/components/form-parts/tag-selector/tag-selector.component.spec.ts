import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TagSelectorComponent } from './tag-selector.component';

describe('TagSelectorComponent', () => {
  let component: TagSelectorComponent;
  let fixture: ComponentFixture<TagSelectorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TagSelectorComponent]
    });
    fixture = TestBed.createComponent(TagSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
