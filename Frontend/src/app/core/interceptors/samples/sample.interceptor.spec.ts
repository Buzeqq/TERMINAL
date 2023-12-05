import { TestBed } from '@angular/core/testing';

import { SampleInterceptor } from './sample.interceptor';

describe('SampleInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      SampleInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: SampleInterceptor = TestBed.inject(SampleInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
