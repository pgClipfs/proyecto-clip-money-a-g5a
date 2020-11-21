import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiauthService } from '../../services/apiauth.service';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';


@Component({templateUrl: 'login.component.html', styleUrls: ['./login.component.scss']})
export class LoginComponent implements OnInit{
    
    // public loginForm = new FormGroup({
    //     email: new FormControl(''),
    //     password: new FormControl('')
    // });
    public loginForm = this.formBuilder.group({
        cuil: ['', Validators.required],
        password: ['', Validators.required]
    })

    constructor(public apiauthService: ApiauthService,
                private router: Router,
                private formBuilder: FormBuilder
                ){

        if(this.apiauthService.usuarioData){
            this.router.navigate(['/']); 
        }
    }


    ngOnInit() {
        
    }

    login() {
        if (this.loginForm.valid) {
            this.apiauthService.login(this.loginForm.value).subscribe( response => {
                if (response.Exito === 1) {
                    this.router.navigate(['/'])
                }
            })
        }
    }
}