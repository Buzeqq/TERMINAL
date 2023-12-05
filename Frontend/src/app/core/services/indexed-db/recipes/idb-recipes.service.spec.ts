import { TestBed } from '@angular/core/testing';

import { IdbRecipesService } from './idb-recipes.service';

describe('IdbRecipesService', () => {
  let service: IdbRecipesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IdbRecipesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
