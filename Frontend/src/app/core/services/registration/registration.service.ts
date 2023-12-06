import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api-service';
import { Observable, catchError, map } from 'rxjs';
import { InvitationCreation } from '../../models/users/invitations/invitationCreation';
import { InvitationDetails } from '../../models/users/invitations/invitationDetails';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService extends ApiService {

  constructor(
    http: HttpClient
  ) { super(http) }

  createInvitation(email: string, role: string): Observable<InvitationCreation> {
    return this.post<InvitationCreation>('users', { email, role }).pipe(
      map(r => r),
      catchError(err => this.handleError(err))
    )
  }

  getInvitation(id: string): Observable<InvitationDetails> {
    return this.get<InvitationDetails>(`users/invitations/${id}`)
  }

  confirmInvitation(id: string, password: string): Observable<any> {
    return this.post(`users/invitations/accept/${id}`, { id, password });
  }
}
