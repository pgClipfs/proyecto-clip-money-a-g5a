import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Response } from 'src/app/models/response';
import { ApiCardsDepositService } from 'src/app/services/apicardsdeposit.service';
import { toFormData } from '../../../utils/toFormData';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiAccountService } from 'src/app/services/apiaccount.service';

@Component({
  selector: 'app-card-deposit',
  templateUrl: './card-deposit.component.html',
  styleUrls: ['./card-deposit.component.scss']
})
export class CardDepositComponent implements OnInit {

  Accounts: Account[];
  // Accounts: any[] = [
  //   {
  //     AccountId: 6,
  //     Alias: null,
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
  //   }
  // ];

  minExpirationDate: string = new Date().toISOString().substring(0, 7);
  cardInput: HTMLElement;
  cardIcon: string = "fa fa-credit-card";

  public error: string = "";

  public cardForm = this.formBuilder.group({
    FullName: ['', Validators.required],
    ExpirationDate: ['', Validators.required],
    CreditCardNumber: ['', Validators.required],
    SecurityNumber: ['', Validators.required],
    DocumentNumber: ['', Validators.required],
    Amount: ['', Validators.required],
    DebitAccountId: ['', Validators.required],
  })

  constructor(
    private formBuilder: FormBuilder,
    private apiDepositService: ApiCardsDepositService,
    private apiAccountService: ApiAccountService
  ) { }

  ngOnInit(): void {
    this.getAccounts();
    this.cardInput = document.getElementById('CardNumber');
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

  validateCard() {
    
    setTimeout(() => {
      if (this.cardInput.classList.contains('ng-valid')) {
        
        //@ts-ignore
        let CreditCardNumber = parseInt(this.cardInput.value, 10);
        console.log(CreditCardNumber);
  
        this.apiDepositService.validateCard(CreditCardNumber).subscribe(
          (response: Response) => {
            if (response.Success === 1) {
              //this.Accounts = response.Data;
              console.log('this.CreditC Responseard', response);
              this.cardIcon = this.setCardIcon(response.Data.Brand);
            }
          },
          (error: HttpErrorResponse) => {
            this.error = error.error.Data;
          }
        )
  
      }
      
    }, 100);

  }

  setCardIcon(brand: string) {
    switch (brand) {
      case 'Visa':
        return "fa fa-cc-visa";
      case 'MasterCard':
        return "fa fa-cc-mastercard";
      case 'American Express':
        return "fa fa-cc-amex";
      case 'Discover':
        return "fa fa-cc-discover";
      case 'Diners Club':
        return "fa fa-cc-diners-club";
      case 'JCB':
        return "fa fa-cc-jcb";
    
      default:
        return "fa fa-credit-card";
    }
  }

  deposit() {

    const formClone = { ...this.cardForm.value };

    //cambia el formato de la fecha de YYYY-MM a MM/YY
    formClone.ExpirationDate = formClone.ExpirationDate.substring(5) + '/' + formClone.ExpirationDate.substring(2, 4);
    //console.log(formClone.ExpirationDate);

    formClone.SecurityNumber = parseInt(formClone.SecurityNumber, 10);
    //console.log('Parsed ', formClone.SecurityNumber);

    console.log('DataForm', formClone);
    
    if (this.cardForm.valid) {
      this.apiDepositService.cardDeposit(formClone).subscribe(
        (response: Response) => {
          if (response.Success === 1) {
            console.log('Success POST', response)
            //MOSTRAR ESTADO
          }
        },
        (error: HttpErrorResponse) => {
          this.error = error.error.Data;
          console.error('Fail POST', error.error.Data);

        }
      )
    }

  }

  onlyNumbers(event) {
    //console.log(event);
    //let key = event.keyCode || event.which;

    //ignorar el error que tira al compilar
    //@ts-ignore
    event.returnValue = (isNaN(String.fromCharCode(event.keyCode)) || event.keyCode == 32) ? false : true;

  }


}

