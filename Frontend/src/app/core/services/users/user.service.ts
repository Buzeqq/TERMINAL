import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {ApiService} from "../api-service";
import {catchError, EMPTY, map, Observable, tap} from "rxjs";
import {User} from "../../models/users/user";
import {UserDetails} from "../../models/users/user-details";
import {NotificationService} from "../notification/notification.service";

@Injectable({
  providedIn: 'root'
})
export class UserService extends ApiService {

  constructor(
    http: HttpClient,
    private readonly notificationService: NotificationService,
  ){ super(http); }

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

  deleteUser(id: string) {
    return this.delete(`users/${id}`)
      .pipe(
        tap(_ => this.notificationService.notifySuccess('Deleted user')),
        catchError(_ => {
          this.notificationService.notifyError('Failed deletion of user')
          return EMPTY;
        })
      )
  }
}
