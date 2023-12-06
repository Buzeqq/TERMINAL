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

  notifyConnectionError() {
    this.snackBar.open("Feature accessible only online. Please check your network connection.", 'Close', {
      duration: 5000
    });
  }

  notifyNoPermission() {
    this.snackBar.open("Access denied. Contact administration for assistance.", 'Close', {
      duration: 5000
    });
  }
}
