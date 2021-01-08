import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog,MAT_DIALOG_DATA } from '@angular/material/dialog';


@Component({
  selector: 'app-dialog-transaction-status',
  templateUrl: './dialog-transaction-status.component.html',
  styleUrls: ['./dialog-transaction-status.component.scss']
})
export class DialogTransactionStatusComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
  }
  

}
