import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DexieTesterComponent } from './dexie-tester.component';

describe('DexieTesterComponent', () => {
  let component: DexieTesterComponent;
  let fixture: ComponentFixture<DexieTesterComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DexieTesterComponent]
    });
    fixture = TestBed.createComponent(DexieTesterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
