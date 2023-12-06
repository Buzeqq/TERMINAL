import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor, HttpResponse
} from '@angular/common/http';
import {Observable, tap} from 'rxjs';
import {IndexedDbService} from "../../services/indexed-db/indexed-db.service";
import {SampleDetails} from "../../models/samples/sampleDetails";

@Injectable()
export class SampleInterceptor implements HttpInterceptor {

  constructor(
    private readonly idbService: IndexedDbService
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const url = /\/samples\/[a-f0-9]{8}(-[a-f0-9]{4}){3}-[a-f0-9]{12}$/i;
    if (request.url.match(url)) {
      return next.handle(request).pipe(
        tap((event) => {
          if (event instanceof HttpResponse)
            this.idbService.addSample(event.body as SampleDetails);
        })
      );
    } else return next.handle(request);
  }
}
