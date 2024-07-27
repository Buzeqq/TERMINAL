import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError } from "rxjs";
import { NotAuthorizedError } from "../errors/errors";

export const globalErrorHandlerInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((errorResponse: HttpErrorResponse) => {
      if (errorResponse.status === 401) {
        throw new NotAuthorizedError(errorResponse.error);
      }
      throw errorResponse;
    })
  );
};
