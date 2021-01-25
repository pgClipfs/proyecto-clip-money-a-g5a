import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Response } from 'src/app/models/response';
import { ApiTransactionsService } from 'src/app/services/apitransactions.service.';

@Component({
  selector: 'app-operaciones',
  templateUrl: './operaciones.component.html',
  styleUrls: ['./operaciones.component.scss']
})
export class OperacionesComponent implements OnInit {

  constructor(private _transactionsService: ApiTransactionsService) {
    this.refreshTransactions();
   }
  
  displayedColumns: string[] = ['VoucherNumber', 'Amount', 'Account', 'TransactionType', 'DateTime', 'Concept'];
  dataSource = ELEMENT_DATA;
  
  ngOnInit(): void {
    
  }

  refreshTransactions() {
    this._transactionsService.getTransactions().subscribe(
      (response: Response) => {
        console.log(response)
        if (response.Success === 1) {
          ELEMENT_DATA.push(...response.Data.map(elem => {elem.DateTime = elem.DateTime.substring(0,10); return elem}))
          let prueba = [...ELEMENT_DATA]
          this.dataSource = prueba;
        }
      },
      (error: HttpErrorResponse) => {
        console.log(error.error.Data);        
      }
    )
  }

}
export interface PeriodicElement {
  VoucherNumber: number;
  TransactionType: string;
  Amount: Number;
  Account: string;
  DateTime: Date;
  Concept: string;
}


const ELEMENT_DATA: PeriodicElement[] = [
];