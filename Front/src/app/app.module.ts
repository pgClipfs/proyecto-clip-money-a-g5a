import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtInterceptor } from './security/jwt.interceptor';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatTabsModule } from '@angular/material/tabs';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDividerModule} from '@angular/material/divider';
import { MatSelectModule} from '@angular/material/select';
import { MatRippleModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ErrorInterceptor } from './security/error.interceptor';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { AuthComponent } from './components/auth/auth.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { PreregistroComponent } from './components/auth/preregistro/preregistro.component';
import { HomeComponent } from './components/home/home.component';
import { DialogTermsComponent } from './components/registration/dialog-terms/dialog-terms.component';
import { DialogDeleteComponent } from './common/delete/dialogdelete.component';
import { TransferenciaComponent } from './components/transferencia/transferencia.component';
import { PerfilComponent } from './components/perfil/perfil.component';
import { OperacionesComponent } from './components/operaciones/operaciones.component';
import { CardDepositComponent } from './components/deposit/card-deposit/card-deposit.component';
import { DepositComponent } from './components/deposit/deposit.component';
import { DepositocuentaComponent } from './components/deposit/depositocuenta/depositocuenta.component';
import { DepositRapiPagoComponent } from './components/deposit/deposit-rapi-pago/deposit-rapi-pago.component';
import { DialogTransactionStatusComponent } from './components/deposit/card-deposit/dialog-transaction-status/dialog-transaction-status.component';
import { SelectAccountComponent } from './components/select-account/select-account.component';
import { MenuCrearCuentaComponent } from './common/menu-crear-cuenta/menu-crear-cuenta.component';
import {MatBottomSheetModule, MatBottomSheetRef, MAT_BOTTOM_SHEET_DATA, MAT_BOTTOM_SHEET_DEFAULT_OPTIONS} from '@angular/material/bottom-sheet';
import { AliasFormComponent } from './components/home/alias-form/alias-form.component';


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
    DepositRapiPagoComponent,
    DialogTransactionStatusComponent,
    SelectAccountComponent,
    MenuCrearCuentaComponent,
    AliasFormComponent
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
    MatListModule,
    MatProgressSpinnerModule,
    MatProgressBarModule,
    MatBottomSheetModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
     {provide: MAT_BOTTOM_SHEET_DEFAULT_OPTIONS, useValue: {hasBackdrop: false}}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
