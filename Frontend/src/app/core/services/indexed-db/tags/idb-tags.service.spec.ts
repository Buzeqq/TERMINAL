import { TestBed } from '@angular/core/testing';

import { IdbTagsService } from './idb-tags.service';

describe('IdbTagsService', () => {
  let service: IdbTagsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IdbTagsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
