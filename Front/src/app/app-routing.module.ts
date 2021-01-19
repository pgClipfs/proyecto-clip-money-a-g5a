import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './security/auth.guard';
import { AuthComponent } from './components/auth/auth.component';
import { CardDepositComponent } from './components/deposit/card-deposit/card-deposit.component';
import { HomeComponent } from './components/home/home.component';
import { DepositComponent } from './components/deposit/deposit.component';
import { DepositocuentaComponent } from './components/deposit/depositocuenta/depositocuenta.component';
import { DepositRapiPagoComponent } from './components/deposit/deposit-rapi-pago/deposit-rapi-pago.component';
// import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { TransferenciaComponent } from './components/transferencia/transferencia.component'
import { PerfilComponent } from './components/perfil/perfil.component'
import { OperacionesComponent } from './components/operaciones/operaciones.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard]},
  { path: 'registration', component: RegistrationComponent},
  { path: 'transferencia', component: TransferenciaComponent, canActivate: [AuthGuard]},
  { path: 'operaciones', component: OperacionesComponent, canActivate: [AuthGuard]},
  { path: 'perfil', component: PerfilComponent, canActivate: [AuthGuard]},
  { path: 'deposit', component: DepositComponent, canActivate: [AuthGuard]},
  { path: 'deposit/bankAccount', component: DepositocuentaComponent, canActivate: [AuthGuard]},
  { path: 'deposit/cards', component: CardDepositComponent, canActivate: [AuthGuard]},
  { path: 'deposit/cash', component: DepositRapiPagoComponent, canActivate: [AuthGuard]},
  { path: 'auth', component: AuthComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

