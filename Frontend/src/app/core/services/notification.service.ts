import { inject, Injectable } from '@angular/core';
import { MatSnackBar } from "@angular/material/snack-bar";

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private readonly snackBarRef = inject(MatSnackBar);

  notifyError(error: string) {
    this.snackBarRef.open(error, 'Close', {
      duration: 5000,
      panelClass: 'error-notification'});
  }
}
