import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';

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


  ngOnInit(): void {
  }

  public constructor(private formBuilder: FormBuilder, private route: ActivatedRoute) {
    this.route.queryParams.subscribe(params => {
        this.registrationForm.controls['name'].setValue(params["firstname"]);
        this.registrationForm.controls['surname'].setValue(params["lastname"]);
    });
}

  signUp() {
    if (this.registrationForm.valid) {
      //
    }


  }
}
