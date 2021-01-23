import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators} from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Response } from 'src/app/models/response';
import { UserAccount } from 'src/app/models/UserAccount';
import { ApiAccountService } from 'src/app/services/apiaccount.service';
import { ApiCashDepositService } from 'src/app/services/apicashdeposit.service';


@Component({
  selector: 'app-deposit-rapi-pago',
  templateUrl: './deposit-rapi-pago.component.html',
  styleUrls: ['./deposit-rapi-pago.component.scss']
})
export class DepositRapiPagoComponent implements OnInit {

  public depositrpform = this.formBuilder.group({
    CVU: ['', Validators.required],
    monto: ['', Validators.required]
  })

  public boletaGenerada: boolean = false;
  // Accounts: UserAccount[];
  Accounts: UserAccount[] = [
    {
      AccountId: 6,
      Alias: "PRUEBA1.PRUEBA2",
      Balance: 0,
      CVU: "0784673993803028515583",
      Currency: {
        "CurrencyId": 1,
        "CurrencyName": "PESO ARGENTINO",
        "Fee": 1.0,
        "SalePrice": 70.0000,
        "PurchasePrice": 60.0000
      },
      OpeningDate: "2021-01-03T00:00:00",
      TypeAccount:{
        "AccountTypeId": 1,
        "AccountTypeName": "CORRIENTE"
      },
      User: null
    }
  ];

  public error: string = "";
  
  constructor(
    private formBuilder: FormBuilder, 
    private apiAccountService: ApiAccountService,
    private apiCashDepositService: ApiCashDepositService
  ) { }

  ngOnInit(): void {
    this.getAccounts();

  }

  generarBoleta(){
    if (this.depositrpform.valid) {
      this.boletaGenerada = true;
    }
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
        this.error = error.error.Data;
      }
    )
  }

  getTicket() {
    this.apiCashDepositService.getTicket(this.Accounts[0].CVU).subscribe(
      (response) => {

          // this.Accounts = response.Data;
          console.log('PDF Response OK', response);
          var file = window.URL.createObjectURL(response);
          window.open(file);

      },
      (error: HttpErrorResponse) => {
        this.error = error.error.Data;
        console.log('PDF Response ERROR',error);
        console.log(error);
        
      }
    )
  }

}
