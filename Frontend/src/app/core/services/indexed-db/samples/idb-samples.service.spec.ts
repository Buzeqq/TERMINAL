import { TestBed } from '@angular/core/testing';

import { IdbSamplesService } from './idb-samples.service';

describe('IdbSamplesService', () => {
  let service: IdbSamplesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IdbSamplesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
