import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasePageHeaderComponent } from './base-page-header.component';

describe('BasePageHeaderComponent', () => {
  let component: BasePageHeaderComponent;
  let fixture: ComponentFixture<BasePageHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BasePageHeaderComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BasePageHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
