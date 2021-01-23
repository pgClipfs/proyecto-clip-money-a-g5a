import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Response } from 'src/app/models/response';
import { ApiAccountService } from 'src/app/services/apiaccount.service';
import { UserAccount } from 'src/app/models/UserAccount';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  Accounts: UserAccount[];
  gettingAccounts: boolean = true;

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

    setTimeout(() => {
      this.gettingAccounts = false;      
    }, 1000);

  }

}

