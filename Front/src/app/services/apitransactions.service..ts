import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Response } from '../models/response';


@Injectable({
  providedIn: 'root'
})
export class ApiTransactionsService {
  url: string = 'http://localhost:49220/api/Transactions/';

  constructor(private _http: HttpClient) { }
  
  getTransactions(): Observable<Response> {
    return this._http.get<Response>(this.url);
  }

}


