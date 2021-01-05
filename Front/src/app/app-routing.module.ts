import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthComponent } from './components/auth/auth.component';
import { CardDepositComponent } from './components/card-deposit/card-deposit.component';
import { HomeComponent } from './components/home/home.component';
import { DepositComponent } from './components/deposit/deposit.component';
import { DepositocuentaComponent } from './components/deposit/depositocuenta/depositocuenta.component';
import { DepositRapiPagoComponent } from './components/deposit-rapi-pago/deposit-rapi-pago.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { AuthGuard } from './security/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard]},
  { path: 'registration', component: RegistrationComponent},
  { path: 'auth', component: AuthComponent},
  { path: 'deposit/cards', component: CardDepositComponent, canActivate: [AuthGuard]},
  { path: 'deposit', component: DepositComponent},
  { path: 'depositocuenta', component: DepositocuentaComponent},
  { path: 'depositrp', component: DepositRapiPagoComponent},
  { path: 'auth', component: AuthComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
