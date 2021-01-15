import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-operaciones',
  templateUrl: './operaciones.component.html',
  styleUrls: ['./operaciones.component.scss']
})
export class OperacionesComponent implements OnInit {

  constructor() { }
  
  displayedColumns: string[] = ['position', 'name', 'account', 'monto', 'type', 'date', 'concepto'];
  dataSource = ELEMENT_DATA;
  
  ngOnInit(): void {
  }

}
export interface PeriodicElement {
  name: string;
  position: number;
  account: number;
  monto: number;
  type: string;
  date: string;
  concepto: string;
}


const ELEMENT_DATA: PeriodicElement[] = [
  {position: 1, name: 'Usuario', monto: 200, account:123456, type: 'Deposito', date: "30/12/2020", concepto:'Venta'},
  {position: 2, name: 'Usuario', monto: 200, account:123456, type: 'Transferencia', date: "30/12/2020", concepto:'Venta'},
  {position: 3, name: 'Usuario', monto: 200, account:123456, type: 'Deposito', date: "1/1/2021", concepto:'Venta'},
  {position: 4, name: 'Usuario', monto: 200, account:123456, type: 'Transferencia', date: "1/1/2021", concepto:'Venta'},
  {position: 5, name: 'Usuario', monto: 200, account:123456, type: 'Transferencia', date: "1/1/2021", concepto:'Venta'},
  {position: 6, name: 'Usuario', monto: 200, account:123456, type: 'Transferencia', date: "1/1/2021", concepto:'Venta'},
  {position: 7, name: 'Usuario', monto: 200, account:123456, type: 'Deposito', date: "2/1/2021", concepto:'Venta'},
  {position: 8, name: 'Usuario', monto: 200, account:123456, type: 'Transferencia', date: "2/1/2021", concepto:'Venta'},
  {position: 9, name: 'Usuario', monto: 200, account:123456, type: 'Deposito', date: "2/1/2021", concepto:'Venta'},
  {position: 10, name: 'Usuario', monto: 200, account:123456, type: 'Deposito', date: "4/1/2021", concepto:'Venta'},
];