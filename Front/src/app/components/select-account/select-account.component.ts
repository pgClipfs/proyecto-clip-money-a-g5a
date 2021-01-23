import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Response } from 'src/app/models/response';
import { UserAccount } from 'src/app/models/UserAccount';
import { ApiAccountService } from 'src/app/services/apiaccount.service';

@Component({
  selector: 'select-account',
  templateUrl: './select-account.component.html',
  styleUrls: ['./select-account.component.scss']
})
export class SelectAccountComponent implements OnInit {

  Accounts: UserAccount[];
  selectedAccountIndex: number = undefined;
  
  constructor(private apiAccountService: ApiAccountService) { }

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
    //console.log('!isNaN:', !isNaN(this.selectedAccountIndex));
    return !isNaN(this.selectedAccountIndex);
  }

}
