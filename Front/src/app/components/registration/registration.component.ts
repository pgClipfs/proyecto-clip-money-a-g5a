import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { ApiauthService } from 'src/app/services/apiauth.service';
import { HttpErrorResponse } from '@angular/common/http';
import { DialogTermsComponent } from './dialog-terms/dialog-terms.component';
import { MatDialog } from '@angular/material/dialog';
import { Response } from 'src/app/models/response';
import { sha256 } from 'js-sha256';
import { toFormData } from '../../../utils/toFormData';

@Component({
  selector: 'user-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  public registrationForm = this.formBuilder.group({
    Name: ['', Validators.required],
    Surname: ['', Validators.required],
    Cuil: ['', Validators.required],
    Email: ['', Validators.required],
    Password: ['', Validators.required],
    PhoneNumber: ['', Validators.required],
    DniFront: [null, Validators.required],
    DniBack: [null, Validators.required]
  })

  public error: string = "";
  public firstForm: boolean = true;
  public dniFront = null;

  ngOnInit(): void {
  }

  public constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private apiauthService: ApiauthService,
    public dialog: MatDialog,
    private router: Router,
    private cd: ChangeDetectorRef
    ) {
    this.route.queryParams.subscribe(params => {
        this.registrationForm.controls['Name'].setValue(params["firstname"]);
        this.registrationForm.controls['Surname'].setValue(params["lastname"]);
    });
}

  openDialog(e) {
    e.preventDefault();
    const dialogRef = this.dialog.open(DialogTermsComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  onFileSelect(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      console.log(event.target.name)
      this.registrationForm.get(event.target.name).setValue(file);
      this.dniFront = file;
    }
  }

  signUp() {

    const formClone = {...this.registrationForm.value}
    const hashedPassword = sha256(formClone.Password)
    formClone.Password = hashedPassword;
    
    if(this.registrationForm.valid) {
      this.apiauthService
      .singUp(toFormData(formClone)).subscribe(
        (response: Response) => {                
          if(response.Success === 1) {
            console.log('hola')
            this.router.navigate(['./auth'])
  
          }
        },
        (error: HttpErrorResponse) => {                     
          this.error= error.error.Data;
        }
      )
    }

  }
}
