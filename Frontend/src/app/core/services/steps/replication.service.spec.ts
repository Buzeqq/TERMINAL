import { TestBed } from '@angular/core/testing';

import { ReplicationService } from './replication.service';

describe('ReplicationService', () => {
  let service: ReplicationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReplicationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
