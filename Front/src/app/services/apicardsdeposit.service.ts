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
export class ApiCardsDepositService {
  url: string = 'http://localhost:49220/api/Deposit/CreditCard';
  constructor(private _http: HttpClient) { }
  
  cardDeposit(deposit: any): Observable<Response> {
    return this._http.post<Response>(this.url, deposit, httpOption);
  }

  validateCard(NumeroDeTarjeta: number): Observable<Response> {
    return this._http.get<Response>(this.url+`?number=${NumeroDeTarjeta}`);
  }
}


