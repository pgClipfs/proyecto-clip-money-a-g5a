import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';


const httpOption = {
  headers: new HttpHeaders({
    'Contentd-Type': 'application/json'
  })
}
@Injectable({
  providedIn: 'root'
})
export class ApiaccountService {
  url: string = 'http://localhost:49220/api/Account'
  constructor(private _http: HttpClient
    ) { }
  
  /*getCuentas(user:number):Object {
    return this._http.get<Response>(this.url,user);
  }*/

}


