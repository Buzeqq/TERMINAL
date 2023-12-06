import { TestBed } from '@angular/core/testing';

import { IdbProjectsService } from './idb-projects.service';

describe('IdbProjectsService', () => {
  let service: IdbProjectsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IdbProjectsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
