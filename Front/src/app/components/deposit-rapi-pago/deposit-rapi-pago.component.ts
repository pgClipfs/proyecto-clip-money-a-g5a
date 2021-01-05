import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators} from '@angular/forms';


@Component({
  selector: 'app-deposit-rapi-pago',
  templateUrl: './deposit-rapi-pago.component.html',
  styleUrls: ['./deposit-rapi-pago.component.scss']
})
export class DepositRapiPagoComponent implements OnInit {

  public depositrpform = this.formBuilder.group({
    cvu: ['', Validators.required],
    monto: ['', Validators.required]
  })

  public boletaGenerada: boolean = false;
  public error: string = "";
  
  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
  }

  generarBoleta(){
    this.boletaGenerada = true;
  }

}
