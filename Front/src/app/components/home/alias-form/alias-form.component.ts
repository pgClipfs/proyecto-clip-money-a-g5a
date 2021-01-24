import { Component, Inject, OnInit } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-alias-form',
  templateUrl: './alias-form.component.html',
  styleUrls: ['./alias-form.component.scss']
})
export class AliasFormComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<AliasFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data) {}

  ngOnInit(): void {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
