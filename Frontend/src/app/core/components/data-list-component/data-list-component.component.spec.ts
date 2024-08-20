import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DataListComponentComponent } from './data-list-component.component';

describe('DataListComponentComponent', () => {
  let component: DataListComponentComponent;
  let fixture: ComponentFixture<DataListComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DataListComponentComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(DataListComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
