import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { Response } from '../models/response';
import { Usuario } from '../models/usuario';
import { catchError, map } from 'rxjs/operators';
import { Login } from '../models/login';
import { SingUp } from '../models/singUp';

const httpOption = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
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
                if(res.Success === 1){
                    const usuario: Usuario = res.Data;
                    localStorage.setItem('usuario', JSON.stringify(usuario));
                    this.usuarioSubject.next(usuario);
                }
                return res;
            })
        );
    }

    singUp(singUp: any): any {
        return this._http.post<Response>(this.url + "/Registration", singUp)
    }

    logout(){
        localStorage.removeItem('usuario');
        this.usuarioSubject.next(null);
    }

}