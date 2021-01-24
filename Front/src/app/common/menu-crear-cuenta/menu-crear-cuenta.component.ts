import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {MatBottomSheet, MatBottomSheetRef} from '@angular/material/bottom-sheet';
import { Response } from 'src/app/models/response';
import { ApiAccountService } from 'src/app/services/apiaccount.service';


@Component({
  selector: 'app-menu-crear-cuenta',
  templateUrl: './menu-crear-cuenta.component.html',
  styleUrls: ['./menu-crear-cuenta.component.scss']
})
export class MenuCrearCuentaComponent implements OnInit {

  constructor(private _bottomSheetRef: MatBottomSheetRef<MenuCrearCuentaComponent>,
              private accountService: ApiAccountService) { }

  ngOnInit(): void {
  }

  createAccount(event, Currency, Type) {
    event.preventDefault()
    console.log(Currency, Type)
    if(Currency === 'pesos'){
      console.log('hola')
      this.accountService.createPesoAccount(Type).subscribe(
        (response: Response) => {
          if (response.Success === 1) {
            console.log(response)
            location.reload();
  
          }
        },
        (error: HttpErrorResponse) => {
          console.log(error.error.Data);        
        }
      )

    }
  }

}
