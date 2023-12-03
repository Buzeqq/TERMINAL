import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {ApiService} from "../api-service";
import {map, Observable} from "rxjs";
import {User} from "../../models/users/user";
import {UserDetails} from "../../models/users/user-details";

@Injectable({
  providedIn: 'root'
})
export class UserService extends ApiService {

  constructor(
    http: HttpClient
  ) { super(http) }

  getUsers(pageNumber: number, pageSize: number, orderBy = "Role", desc = true): Observable<User[]> {
    return this.get<{ users: User[] }>('users', new HttpParams({
      fromObject: {
        pageNumber,
        pageSize,
        orderBy,
        desc
      }
    }))
      .pipe(
        map(u => u.users),
      );
  }

  getUser(id: string): Observable<UserDetails> {
    return this.get<UserDetails>(`users/${id}`);
  }

  getUsersAmount(): Observable<number> {
    return this.get<number>('users/amount');
  }
}
