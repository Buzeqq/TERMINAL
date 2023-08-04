import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";

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
}
