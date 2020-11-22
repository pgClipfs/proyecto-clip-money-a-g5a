import {  HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { ApiauthService } from '../services/apiauth.service';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private apiauthService: ApiauthService){

    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(catchError(err => {
            if (err.status === 401) {
                this.apiauthService.logout();
                location.reload();
            }
        
            const error = err.error.message || err.statusText;
            return throwError(error);
        }));
    }
}