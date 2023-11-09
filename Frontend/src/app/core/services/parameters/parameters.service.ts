import { Injectable } from '@angular/core';
import { ApiService } from "../api-service";
import { HttpClient } from "@angular/common/http";
import { map, Observable } from "rxjs";
import { Parameter } from "../../models/parameters/parameter";

@Injectable({
  providedIn: 'root'
})
export class ParametersService extends ApiService {

  constructor(httpClient: HttpClient) {
    super(httpClient);
  }

  getParameters(): Observable<Parameter[]> {
    return this.get<{ parameters: Parameter[] }>('parameters')
      .pipe(map(p => p.parameters));
  }
}
