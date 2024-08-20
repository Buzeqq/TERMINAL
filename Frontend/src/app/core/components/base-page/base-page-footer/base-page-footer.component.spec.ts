import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasePageFooterComponent } from './base-page-footer.component';

describe('BasePageFooterComponent', () => {
  let component: BasePageFooterComponent;
  let fixture: ComponentFixture<BasePageFooterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BasePageFooterComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(BasePageFooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
