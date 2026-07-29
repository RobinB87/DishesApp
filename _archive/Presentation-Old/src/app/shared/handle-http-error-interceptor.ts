import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of'
import { HttpErrorResponse } from '@angular/common/http/src/response';
import { GlobalErrorHandler } from './global-error-handler';

@Injectable()
export class HandleHttpErrorInterceptor implements HttpInterceptor {

    constructor(private globalErrorHandler: GlobalErrorHandler) {
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        return next.handle(request)
        .catch((error: HttpErrorResponse) => {
            if (error.error instanceof Error) {
                // client-side or network error
                const errorToLog = `Http error (client/network). ${error.message}`;
                this.globalErrorHandler.handleError(errorToLog);
            }
            else {
                // unsuccesful response code
                const errorToLog = `Http error (unsuccesful response). Message: ${error.message}, status code: ${(error).status}, body: ${JSON.stringify(error.error)} `;
                this.globalErrorHandler.handleError(errorToLog);
            }

            if (error.status === 422) {
                // throw the error body
                return Observable.throw(error.error);
            }
            else {
                // Before Angular 6, this was Observable.of(..)
                return of(new HttpResponse());
            }
        }
        )
    }
}
