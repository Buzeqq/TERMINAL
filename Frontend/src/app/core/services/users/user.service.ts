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
        tap(_ => this.notificationService.notifySuccess('Deleted user. Reload page to see changes.')),
        catchError(_ => {
          this.notificationService.notifyError('Failed deleting user. Check your network connection.')
          return EMPTY;
        })
      )
  }

  updateEmail(id: string | undefined, data: {email: string | null}) {
    return this.patch(`users/${id}/email`, data)
      .pipe(
        tap(_ => this.notificationService.notifyUpdated('User')),
        catchError(_ => {
          this.notificationService.notifyUpdatingFailed('User');
          return EMPTY;
        })
      )
  }

  updateRole(id: string | undefined, data: {role: string | null}) {
    return this.patch(`users/${id}/role`, data)
      .pipe(
        tap(_ => this.notificationService.notifyUpdated('User')),
        catchError(_ => {
          this.notificationService.notifyUpdatingFailed('User');
          return EMPTY;
        })
      )
  }

  updatePassword(id: string, data: { newPassword: string }) {
    return this.patch(`users/${id}/password`, data)
      .pipe(
        tap(_ => this.notificationService.notifyUpdated('User')),
        catchError(_ => {
          this.notificationService.notifyUpdatingFailed('User');
          return EMPTY;
        })
      )
  }
}
