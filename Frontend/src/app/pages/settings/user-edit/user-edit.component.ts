import {Component, Input} from '@angular/core';
import {catchError, EMPTY, Observable, tap} from "rxjs";
import {User} from "../../../core/models/users/user";
import {FormControl, Validators} from "@angular/forms";
import {whitespaceValidator} from "../../../core/components/validators/whitespaceValidator";
import {MatSnackBar} from "@angular/material/snack-bar";
import {UserService} from "../../../core/services/users/user.service";
import {UserDetails} from "../../../core/models/users/user-details";
import {AuthService} from "../../../core/services/auth/auth.service";
import { MatDialog } from '@angular/material/dialog';
import { AddUserComponent } from 'src/app/core/components/dialogs/add-user/add-user.component';
import {NotificationService} from "../../../core/services/notification/notification.service";
import {DeleteDialogComponent} from "../../../core/components/dialogs/delete-dialog/delete-dialog.component";

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent {
  private _userId?: string;
  userDetails$: Observable<User> = new Observable<User>();
  private userDetails?: UserDetails;
  loading: 'determinate' | 'indeterminate' | 'buffer' | 'query' = 'query';

  userRoleFormControl = new FormControl('',[
    Validators.required,
    whitespaceValidator])
  availableRoles = ["Administrator", "Moderator", "User"];

  adminPermissions = this.authService.isAdmin();

  constructor(
    private readonly userService: UserService,
    private readonly notificationService: NotificationService,
    private readonly authService: AuthService,
    private readonly dialog: MatDialog
  ) {  }

  @Input()
  get userId(): string | undefined {
    return this._userId;
  }

  set userId(id: string | undefined) {
    this._userId = id;
    this.userDetails$ = this.userService.getUser(this._userId!)
      .pipe(
        catchError((err, _) => {
          console.log(err);
          this.notificationService.notifyError('Failed to load user');
          return EMPTY;
        }),
        tap(r => {
          r.role == "Registered" ? r.role = "User" : r.role;
          this.userDetails = r;
          this.resetForm();
          this.loading = 'determinate';
        })
      );
  }

  resetForm() {
    this.userRoleFormControl.setValue(this.userDetails!.role);
  }

  readyToSubmit() {
    return this.dirtyForm() && this.userRoleFormControl.valid;
  }

  dirtyForm() {
    return this.userDetails!.role !== this.userRoleFormControl.value
  }

  addUser(){
    this.dialog.open(AddUserComponent);
  }

  editUser() {
    // TODO send a request with new form values
    // don't forget to map User to Registered
    if (this.userRoleFormControl.value == 'User') {
      let role = 'Registered';
    }
  }

  deleteUser() {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: {
        title: `Delete User ${this.userDetails?.email}`,
        message: 'Attention! This action is irreversible.'
      }});
    dialogRef.afterClosed().subscribe(deleteConfirmed => {
      if (deleteConfirmed)
        this.userService.deleteUser(this._userId!).subscribe();
    })
  }
}

