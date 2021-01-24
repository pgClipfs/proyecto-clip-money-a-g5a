import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Response } from 'src/app/models/response';
import { ApiAccountService } from 'src/app/services/apiaccount.service';
import { UserAccount } from 'src/app/models/UserAccount';
import { MatDialog } from '@angular/material/dialog';
import { AliasFormComponent } from './alias-form/alias-form.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  Accounts: UserAccount[];
  gettingAccounts: boolean = true;

  constructor(private apiAccountService: ApiAccountService, public dialog: MatDialog) { }
    
  ngOnInit(): void {
    this.getAccounts();
  }

  openDialog(oldAlias, accountId): void {
    const dialogRef = this.dialog.open(AliasFormComponent, {
      width: '250px',
      data: {alias: oldAlias}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(result)
      console.log(oldAlias, accountId)
      console.log('The dialog was closed');

      this.apiAccountService.setNewAlias(accountId, result).subscribe(
        (response: Response) => {
          if (response.Success === 1) {
            this.getAccounts()
          }
        },
        (error: HttpErrorResponse) => {
          console.log(error.error.Data);        
        }
      )

    });
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

