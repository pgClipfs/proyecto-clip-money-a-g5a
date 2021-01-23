import { HttpErrorResponse } from '@angular/common/http';
import { HtmlTagDefinition } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { Response } from 'src/app/models/response';
import { UserAccount } from 'src/app/models/UserAccount';
import { ApiAccountService } from 'src/app/services/apiaccount.service';

@Component({
  selector: 'app-depositocuenta',
  templateUrl: './depositocuenta.component.html',
  styleUrls: ['./depositocuenta.component.scss']
})
export class DepositocuentaComponent implements OnInit {

  // Accounts: UserAccount[] = [
  //   {
  //     AccountId: 6,
  //     Alias: "PRUEBA1.PRUEBA2",
  //     Balance: 0,
  //     CVU: "0784673993803028515583",
  //     Currency: {
  //       "CurrencyId": 1,
  //       "CurrencyName": "PESO ARGENTINO",
  //       "Fee": 1.0,
  //       "SalePrice": 70.0000,
  //       "PurchasePrice": 60.0000
  //     },
  //     OpeningDate: "2021-01-03T00:00:00",
  //     TypeAccount:{
  //       "AccountTypeId": 1,
  //       "AccountTypeName": "CORRIENTE"
  //     },
  //     User: null
  //   },
  //   {
  //     AccountId: 5,
  //     Alias: "PRUEBA3.PRUEBA4",
  //     Balance: 0,
  //     CVU: "9984674568803028516627",
  //     Currency: {
  //       "CurrencyId": 1,
  //       "CurrencyName": "PESO ARGENTINO",
  //       "Fee": 1.0,
  //       "SalePrice": 70.0000,
  //       "PurchasePrice": 60.0000
  //     },
  //     OpeningDate: "2021-01-03T00:00:00",
  //     TypeAccount:{
  //       "AccountTypeId": 1,
  //       "AccountTypeName": "CORRIENTE"
  //     },
  //     User: null
  //   },
  // ];
  
  Accounts: UserAccount[];
  selectedAccountIndex: number = undefined;

  constructor(private apiAccountService: ApiAccountService) { }

  ngOnInit(): void {
    this.getAccounts();

    // setInterval(() => {
    //   console.log(this.selectedAccountIndex);      
    // }, 3000);
  }

  getAccounts() {
    this.apiAccountService.getAccounts().subscribe(
      (response: Response) => {
        if (response.Success === 1) {
          this.Accounts = response.Data;
          console.log('this.Accounts', this.Accounts);

        }
      },
      (error: HttpErrorResponse) => {
        console.log(error.error.Data);
      }
    )
  }

  isNumber(): boolean {
    //console.log('!isNaN:', !isNaN(this.selectedAccountIndex));
    return !isNaN(this.selectedAccountIndex);
  }

}
