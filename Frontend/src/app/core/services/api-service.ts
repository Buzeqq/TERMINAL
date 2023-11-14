import { HttpClient, HttpErrorResponse, HttpParams } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { environment } from "../../../environments/environment";

export abstract class ApiService {
  private apiUrl = environment.apiUrl;

  protected constructor(protected readonly http: HttpClient) {
  }

  handleError(err: HttpErrorResponse) {
    if (err.status == 0) {
      console.error(`There is an error with the client or network: `, err.error)
    } else {
      console.error(`Server-side error: `, err.error)
    }

    return throwError(() => Error('Cannot load data.'))
  }

  protected get<T>(endpoint: string, params?: HttpParams): Observable<T> {
    return this.http.get<T>(this.apiUrl + endpoint, {
      params
    });
  }

  protected post<T>(endpoint: string, body: any): Observable<T> {
    return this.http.post<T>(this.apiUrl + endpoint, body);
  }

  protected put<T>(endpoint: string, body: any): Observable<T> {
    return this.http.put<T>(this.apiUrl + endpoint, body);
  }

  protected delete<T>(endpoint: string): Observable<T> {
    return this.http.delete<T>(this.apiUrl + endpoint);
  }
}
