import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSidenavModule } from '@angular/material/sidenav';
import { HomeComponent } from './components/home/home.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatTabsModule } from '@angular/material/tabs';
import { MatButtonModule } from '@angular/material/button';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DialogDeleteComponent } from './common/delete/dialogdelete.component';
import { LoginComponent } from './components/login/login.component';
import { JwtInterceptor } from './security/jwt.interceptor';
import { FlexLayoutModule } from '@angular/flex-layout';
import { RegistrationComponent } from './components/registration/registration.component';
import { ErrorInterceptor } from './security/error.interceptor';
import { AuthComponent } from './components/auth/auth.component';
import { PreregistroComponent } from './components/auth/preregistro/preregistro.component';
import { DialogTermsComponent } from './components/registration/dialog-terms/dialog-terms.component';
import {MatDividerModule} from '@angular/material/divider';
import {MatSelectModule} from '@angular/material/select';
import {MatRippleModule} from '@angular/material/core';
import {MatIconModule} from '@angular/material/icon';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatListModule} from '@angular/material/list';
import { TransferenciaComponent } from './components/transferencia/transferencia.component';
import { PerfilComponent } from './components/perfil/perfil.component';
import { OperacionesComponent } from './components/operaciones/operaciones.component';
import { CardDepositComponent } from './components/card-deposit/card-deposit.component';
import { DepositComponent } from './components/deposit/deposit.component';
import { DepositocuentaComponent } from './components/deposit/depositocuenta/depositocuenta.component';
import { DepositRapiPagoComponent } from './components/deposit-rapi-pago/deposit-rapi-pago.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    DialogDeleteComponent,
    LoginComponent,
    RegistrationComponent,
    AuthComponent,
    PreregistroComponent,
    DialogTermsComponent,
    TransferenciaComponent,
    PerfilComponent,
    OperacionesComponent,
    CardDepositComponent,
    DepositComponent,
    DepositocuentaComponent,
    DepositRapiPagoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatSidenavModule,
    HttpClientModule,
    MatTableModule,
    MatDialogModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatSnackBarModule,
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    MatTabsModule,
    MatDividerModule,
    MatSelectModule,
    MatRippleModule,
    MatIconModule,
    MatToolbarModule,
    MatListModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
