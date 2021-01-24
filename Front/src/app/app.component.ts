import { Component } from '@angular/core';
import { MatBottomSheet, MatBottomSheetRef} from '@angular/material/bottom-sheet';  
import { Router } from '@angular/router';
import { MenuCrearCuentaComponent } from './common/menu-crear-cuenta/menu-crear-cuenta.component';
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
  opened = false;
  

  onNgInit(){

  }
  

  constructor(
      private _bottomSheet: MatBottomSheet,
      public apiauthService: ApiauthService,
      private router: Router
    ){
      this.apiauthService.usuario.subscribe(res => {

        this.usuario = res;
        // console.log('cambio en el objeto: ' + res);
      });
      // this.usuario = this.apiauthService.usuarioData; // no se usa este porque al no ser un observable el menu lateral no se va a actualizar

  }


  openCreateAccount() {
    this._bottomSheet.open(MenuCrearCuentaComponent);
  }

  logout() {
    this.apiauthService.logout();
    this.router.navigate(['/auth']);
  }
}
