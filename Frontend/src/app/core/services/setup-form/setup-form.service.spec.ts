import { TestBed } from '@angular/core/testing';

import { SetupFormService } from './setup-form.service';

describe('SetupFormService', () => {
  let service: SetupFormService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SetupFormService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
