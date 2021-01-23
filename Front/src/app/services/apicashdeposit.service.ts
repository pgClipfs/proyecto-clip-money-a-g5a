import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApiCashDepositService {
  url: string = 'http://localhost:49220/api/Deposit/Cash';
  

  constructor(private _http: HttpClient) { }
  
  // cardDeposit(deposit: any): Observable<Response> {
  //   return this._http.post<Response>(this.url, deposit, httpOption);
  // }

  getTicket(CVU: string) {
    
    return this._http.get(this.url+`?cvu=${CVU}`, {responseType: 'blob'});
  }
}


