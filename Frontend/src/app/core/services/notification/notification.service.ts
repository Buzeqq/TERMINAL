import { Injectable } from '@angular/core';
import { MatSnackBar } from "@angular/material/snack-bar";

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private snackBar: MatSnackBar) {
  }

  notifySuccess(message: string) {
    this.snackBar.open(message, 'Close', {
      duration: 5000
    });
  }

  notifyError(message: string) {
    this.snackBar.open(message, 'Close', {
      duration: 5000
    });
  }

  notifySessionExpiration(time: number) {
    /* time provided in milliseconds */
    let ref = this.snackBar.open(
      "Session expires in " + time/60_000 + ' minutes', 'Extend', {
       duration: 30000
      })
    return ref.onAction();
  }

  notifyNoPermission(message: string) {
    this.snackBar.open(message, 'Close', {
      duration: 5000
    });
  }
}
