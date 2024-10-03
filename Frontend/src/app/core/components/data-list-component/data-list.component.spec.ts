import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DataListComponent } from './data-list.component';

describe('DataListComponentComponent', () => {
  let component: DataListComponent<any>;
  let fixture: ComponentFixture<DataListComponent<any>>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DataListComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(DataListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
