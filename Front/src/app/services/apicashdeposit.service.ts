import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Response } from '../models/response';

let token = JSON.parse(localStorage.getItem('usuario')).Token;

const httpOption = {
  headers: new HttpHeaders({
    'Authorization': 'Bearer ' + token,
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class ApiCashDepositService {
  url: string = 'http://localhost:49220/api/Deposit/Cash';
  

  constructor(private _http: HttpClient) { }
  
  // cardDeposit(deposit: any): Observable<Response> {
  //   return this._http.post<Response>(this.url, deposit, httpOption);
  // }

  getTicket(CVU: string): Observable<Response> {
    console.log('Headers',httpOption);
    
    return this._http.get<Response>(this.url+`?cvu=${CVU}`, httpOption);
  }
}


