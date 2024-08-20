import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TerminalIconComponent } from './terminal-icon.component';

describe('TerminalIconComponent', () => {
  let component: TerminalIconComponent;
  let fixture: ComponentFixture<TerminalIconComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TerminalIconComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(TerminalIconComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
