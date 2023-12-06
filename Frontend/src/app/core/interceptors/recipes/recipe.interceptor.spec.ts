import { TestBed } from '@angular/core/testing';

import { RecipeInterceptor } from './recipe.interceptor';

describe('RecipeInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      RecipeInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: RecipeInterceptor = TestBed.inject(RecipeInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
