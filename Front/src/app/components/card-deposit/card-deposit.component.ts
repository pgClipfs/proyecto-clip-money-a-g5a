import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-card-deposit',
  templateUrl: './card-deposit.component.html',
  styleUrls: ['./card-deposit.component.scss']
})
export class CardDepositComponent implements OnInit {

  public cardForm = this.formBuilder.group({
    FullName: ['', Validators.required],
    DocumentNumber: ['', Validators.required],
    CreditCardNumber: ['', Validators.required],
    SecurityNumber: ['', Validators.required],
    ExpirationDate: ['', Validators.required],
    Amount: ['', Validators.required],
  })

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
  }

  caca() {
    console.log('asdjkfasfhjfzskjfgh');
    
  }
  // signUp() {

  //   const formClone = {...this.registrationForm.value}
  //   const hashedPassword = sha256(formClone.Password)
  //   formClone.Password = hashedPassword;
    
  //   if(this.registrationForm.valid) {
  //     this.apiauthService
  //     .singUp(toFormData(formClone)).subscribe(
  //       (response: Response) => {                
  //         if(response.Success === 1) {
  //           console.log('hola')
  //           this.router.navigate(['./auth'])
  
  //         }
  //       },
  //       (error: HttpErrorResponse) => {                     
  //         this.error= error.error.Data;
  //       }
  //     )
  //   }

  // }

}
