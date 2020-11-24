import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { ApiauthService } from 'src/app/services/apiauth.service';
import { HttpErrorResponse } from '@angular/common/http';
import { DialogTermsComponent } from './dialog-terms/dialog-terms.component';
import { MatDialog } from '@angular/material/dialog';
import { Response } from 'src/app/models/response';
import { sha256 } from 'js-sha256';

@Component({
  selector: 'user-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  public registrationForm = this.formBuilder.group({
    nombre: ['', Validators.required],
    apellido: ['', Validators.required],
    cuil: ['', Validators.required],
    email: ['', Validators.required],
    contraseña: ['', Validators.required],
    telefono: ['', Validators.required]
  })

  public error: string = "";

  ngOnInit(): void {
  }

  public constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private apiauthService: ApiauthService,
    public dialog: MatDialog,
    private router: Router
    ) {
    this.route.queryParams.subscribe(params => {
        this.registrationForm.controls['nombre'].setValue(params["firstname"]);
        this.registrationForm.controls['apellido'].setValue(params["lastname"]);
    });
}

  openDialog(e) {
    e.preventDefault();
    const dialogRef = this.dialog.open(DialogTermsComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  signUp() {

    const formClone = {...this.registrationForm.value}
    const hashedPassword = sha256(formClone.contraseña)
    formClone.contraseña = hashedPassword;
    
    this.apiauthService
    .singUp(formClone).subscribe(
      (response: Response) => {                          
        if(response.Exito === 1) {
          this.router.navigate(['/auth'])

        }
      },
      (error: HttpErrorResponse) => {                     
        this.error= error.error.Data;
      }
    )

  }
}
