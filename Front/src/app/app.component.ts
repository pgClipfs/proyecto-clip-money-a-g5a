import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from './models/usuario';
import { ApiauthService } from './services/apiauth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'app';
  usuario: Usuario;

  constructor(public apiauthService: ApiauthService,
              private router: Router
    ) {
      this.apiauthService.usuario.subscribe(res => {

        this.usuario = res;
        // console.log('cambio en el objeto: ' + res);
      });
      // this.usuario = this.apiauthService.usuarioData; // no se usa este porque al no ser un observable el menu lateral no se va a actualizar

  }

  logout() {
    this.apiauthService.logout();
    this.router.navigate(['/login']);
  }
}
