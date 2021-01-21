import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef ,MAT_DIALOG_DATA } from '@angular/material/dialog';


@Component({
  selector: 'app-dialog-transaction-status',
  templateUrl: './dialog-transaction-status.component.html',
  styleUrls: ['./dialog-transaction-status.component.scss']
})
export class DialogTransactionStatusComponent implements OnInit {

  code: number = this.getRandom(99999999999, 10000000000000);
  date: string = new Date().toLocaleDateString() + ' ' + new Date().toLocaleTimeString();

  constructor(
    public dialogRef: MatDialogRef<DialogTransactionStatusComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit(): void {
    console.log('Status transacción:', this.data.Status);
  }
  
  closeDialog(): void {
    this.dialogRef.close();
  }
  
  getRandom(min:number, max:number) :number {
    return Math.floor(Math.random() * (max - min)) + min;    
  }
  

}
