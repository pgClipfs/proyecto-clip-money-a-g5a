import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Response } from 'src/app/models/response';
import { Usuario } from 'src/app/models/usuario';
import { ApiUserInfoService } from 'src/app/services/apiusuario.service';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

  UserData: Usuario;

  constructor(private apiUserDataService: ApiUserInfoService) { }

  ngOnInit(): void {
    this.getProfileData();
  }

  getProfileData() {
    this.apiUserDataService.getPerfil().subscribe(
      (response: Response) => {
        if (response.Success === 1) {
          this.UserData = response.Data;
          console.log('this.Userdata', this.UserData);
        }
      },
      (error: HttpErrorResponse) => {
        console.log(error.error.Data);        
      }
    )
  }

}
