import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Response } from 'src/app/models/response';
import { UserAccount } from 'src/app/models/UserAccount';
import { ApiAccountService } from 'src/app/services/apiaccount.service';

@Component({
  selector: 'app-transferencia',
  templateUrl: './transferencia.component.html',
  styleUrls: ['./transferencia.component.scss']
})
export class TransferenciaComponent implements OnInit {

  Accounts: UserAccount[];
  selectedAccountIndex: number = undefined;

  public transferForm = this.formBuilder.group({
    DebitAccountId: ['', Validators.required],
    Amount: ['', Validators.required],
    CreditAccountId: ['', Validators.required],
    Concept: ['', Validators.required],
    DestinationReference: ['', Validators.required],
    EmailNotificacion: ['', Validators.required],
  })

  constructor(private apiAccountService: ApiAccountService, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.getAccounts();
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
    return !isNaN(this.selectedAccountIndex);
  }

  transfer(){

  }
}
