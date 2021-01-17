import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Response } from '../models/response';


const httpOption = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}
@Injectable({
  providedIn: 'root'
})
export class ApiAccountService {
  url: string = 'http://localhost:49220/api/Account';

  constructor(private _http: HttpClient) { }
  
  getAccounts(): Observable<Response> {
    return this._http.get<Response>(this.url);
  }

}


