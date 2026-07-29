import { HttpEvent, HttpHandler, HttpRequest } from "@angular/common/http";
import { HttpInterceptor } from "@angular/common/http/src/interceptor";
import { Observable } from "rxjs/Observable";

export class WriteOutJsonInterceptor implements HttpInterceptor {
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next
      .handle(request)
      .do((data) => console.log(JSON.stringify(data, null, "\t")));
  }
}
