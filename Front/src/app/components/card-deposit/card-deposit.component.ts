import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Response } from 'src/app/models/response';
import { ApiCardsDepositService } from 'src/app/services/apicardsdeposit.service';
import { toFormData } from '../../../utils/toFormData';
import { HttpErrorResponse } from '@angular/common/http';



@Component({
  selector: 'app-card-deposit',
  templateUrl: './card-deposit.component.html',
  styleUrls: ['./card-deposit.component.scss']
})
export class CardDepositComponent implements OnInit {

  public error: string = "";

  public cardForm = this.formBuilder.group({
    FullName: ['', Validators.required],
    ExpirationDate: ['', Validators.required],
    CreditCardNumber: ['', Validators.required],
    SecurityNumber: ['', Validators.required],
    DocumentNumber: ['', Validators.required],
    Amount: ['', Validators.required],
  })

  constructor(private formBuilder: FormBuilder, private apiDepositService: ApiCardsDepositService,) { }

  ngOnInit(): void {
  }

  deposit() {

    const formClone = {...this.cardForm.value};
    formClone.DebitAccountId = 3; //para que tenga exito debe tener un id de cuenta q sea de ese usuario
    formClone.ExpirationDate = "06/22"; //no llegue a hacer formateo de la fecha de 06-12 a 06/12 entonces la hardcodie

    console.log(formClone);

    if(this.cardForm.valid) {
      this.apiDepositService.cardDeposit(toFormData(formClone)).subscribe(
        (response: Response) => {                
          if(response.Success === 1) {
            console.log(response)
            // this.router.navigate(['./'])
          }
        },
        (error: HttpErrorResponse) => {                     
          this.error= error.error.Data;
        }
      )
    }
    
  }

}
