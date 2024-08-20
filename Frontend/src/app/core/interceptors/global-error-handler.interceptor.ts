import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, EMPTY } from 'rxjs';
import { ForbiddenError, NotAuthorizedError } from '../errors/errors.model';
import { inject } from '@angular/core';
import { NotificationService } from '../services/notification.service';

export const globalErrorHandlerInterceptor: HttpInterceptorFn = (req, next) => {
  const notificationService = inject(NotificationService);
  return next(req).pipe(
    catchError((errorResponse: HttpErrorResponse) => {
      if (errorResponse.status === 401) {
        throw new NotAuthorizedError(errorResponse.error);
      }

      if (errorResponse.status === 403) {
        throw new ForbiddenError(errorResponse.error);
      }

      if (errorResponse.status === 500) {
        notificationService.notifyError(
          'An error occurred. Please try again later.',
        );
        return EMPTY;
      }

      throw errorResponse;
    }),
  );
};
