import { Injectable } from '@angular/core';
import { MatSnackBar } from "@angular/material/snack-bar";

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private snackBar: MatSnackBar) { }

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
}
