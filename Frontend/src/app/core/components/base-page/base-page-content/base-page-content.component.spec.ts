import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasePageContentComponent } from './base-page-content.component';

describe('BasePageContentComponent', () => {
  let component: BasePageContentComponent;
  let fixture: ComponentFixture<BasePageContentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BasePageContentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BasePageContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
