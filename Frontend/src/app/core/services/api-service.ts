import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {Observable, throwError} from "rxjs";
import { environment } from "../../../environments/environment";

export abstract class ApiService {
  private apiUrl = environment.apiUrl;
  protected constructor(protected readonly http: HttpClient) {}

  get<T>(endpoint: string): Observable<T> {
    return this.http.get<T>(this.apiUrl + endpoint);
  }

  post<T>(endpoint: string, body: any): Observable<T> {
    return this.http.post<T>(this.apiUrl + endpoint, body);
  }

  put<T>(endpoint: string, body: any): Observable<T> {
    return this.http.put<T>(this.apiUrl + endpoint, body);
  }

  delete<T>(endpoint: string): Observable<T> {
    return this.http.delete<T>(this.apiUrl + endpoint);
  }

  handleError(err: HttpErrorResponse) {
    if (err.status == 0) {
      console.error(`There is an error with the client or network: `, err.error)
    } else {
      console.error(`Server-side error: `, err.error)
    }

    return throwError(() => Error('Cannot load data.'))
  }
}
