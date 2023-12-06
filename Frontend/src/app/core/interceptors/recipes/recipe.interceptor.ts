import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor, HttpResponse
} from '@angular/common/http';
import {Observable, tap} from 'rxjs';
import {IndexedDbService} from "../../services/indexed-db/indexed-db.service";
import {RecipeDetails} from "../../models/recipes/recipeDetails";

@Injectable()
export class RecipeInterceptor implements HttpInterceptor {

  constructor(
    private readonly idbService: IndexedDbService
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const url = /\/recipes\/[a-f0-9]{8}(-[a-f0-9]{4}){3}-[a-f0-9]{12}\/details$/i;
    if (request.url.match(url)) {
      return next.handle(request).pipe(
        tap((event) => {
          if (event instanceof HttpResponse)
            this.idbService.addRecipe(event.body as RecipeDetails);
        })
      );
    } else return next.handle(request);
  }
}
