import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiauthService } from '../../services/apiauth.service';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'user-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  public registrationForm = this.formBuilder.group({
    name: ['', Validators.required],
    surname: ['', Validators.required],
    cuil: ['', Validators.required],
    email: ['', Validators.required],
    password: ['', Validators.required],
    tel: ['', Validators.required]
  })

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
  }

  signUp() {
    if (this.registrationForm.valid) {
      //
    }


  }
}
