import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Usuario } from '../models/usuario'
import { Response } from '../models/response';

const httpOption = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class ApiUserInfoService {
  url: string = 'http://localhost:49220/api/User'

  constructor(private _http: HttpClient) { }

  getPerfil(): Observable<Response> {
    return this._http.get<Response>(this.url);
  }

  edit(usuario: Usuario): Observable<Response> {
    return this._http.put<Response>(this.url, usuario, httpOption)
  }
}


