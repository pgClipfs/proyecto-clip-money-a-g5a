import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiauthService } from '../../services/apiauth.service';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { sha256 } from 'js-sha256';


@Component({selector: 'app-login', templateUrl: 'login.component.html', styleUrls: ['./login.component.scss']})
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

            const formClone = {...this.loginForm.value}
            const hashedPassword = sha256(formClone.password)
            formClone.password = hashedPassword;

            this.apiauthService.login(formClone).subscribe( response => {
                if (response.Success === 1) {
                    this.router.navigate(['/'])
                }
            })
        }
    }
}