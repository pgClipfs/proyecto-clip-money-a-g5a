import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Response } from '../models/response';
import { Usuario } from '../models/usuario';
import { map } from 'rxjs/operators';
import { Login } from '../models/login';

const httpOption = {
    headers: new HttpHeaders({
      'Contentd-Type': 'application/json'
    })
}

@Injectable({
    providedIn: 'root'
})
export class ApiauthService {
    url: string = 'http://localhost:49220/api/Authentication';

    private usuarioSubject: BehaviorSubject<Usuario>;
    public usuario: Observable<Usuario>;

    public get usuarioData(): Usuario {
        return this.usuarioSubject.value;
    }

    constructor(private _http: HttpClient){
        this.usuarioSubject = new BehaviorSubject<Usuario>(JSON.parse(localStorage.getItem('usuario')));
        this.usuario = this.usuarioSubject.asObservable();
    }

    login(login: Login): Observable<Response>{
        return this._http.post<Response>(this.url, login, httpOption).pipe(
            map(res => {
                console.log(res.Data);
                if(res.Exito === 1){
                    const usuario: Usuario = res.Data;
                    localStorage.setItem('usuario', JSON.stringify(usuario));
                    this.usuarioSubject.next(usuario);
                }
                return res;
            })
        );
    }

    logout(){
        localStorage.removeItem('usuario');
        this.usuarioSubject.next(null);
    }
}