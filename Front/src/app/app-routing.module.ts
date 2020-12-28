import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthComponent } from './components/auth/auth.component';
import { DepositComponent } from './components/deposit/deposit.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { AuthGuard } from './security/auth.guard';
import {DepositRapiPagoComponent } from './components/deposit-rapi-pago/deposit-rapi-pago.component'

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard]},
  { path: 'registration', component: RegistrationComponent},
  { path: 'deposit', component: DepositComponent, pathMatch: 'full'},
  { path: 'auth', component: AuthComponent},
  { path: 'depositrp', component: DepositRapiPagoComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
