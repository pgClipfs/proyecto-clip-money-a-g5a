import { Component, OnInit } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-preregistro',
  templateUrl: './preregistro.component.html',
  styleUrls: ['./preregistro.component.scss']
})
export class PreregistroComponent implements OnInit {


  constructor(private formBuilder: FormBuilder, private router: Router) { }


  public registrationForm = this.formBuilder.group({
    nombreCompleto: ['', Validators.required],
    apellido: ['', Validators.required],
})

  ngOnInit(): void {
  }

  handleSubmit(){
    let navigationExtras: NavigationExtras = {
      queryParams: {
          "firstname": this.registrationForm.value.nombreCompleto,
          "lastname": this.registrationForm.value.apellido
      }
  };
    this.router.navigate(["registration"], navigationExtras);
  }
  
}
