import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthComponent } from './components/auth/auth.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { TransferenciaComponent } from './components/transferencia/transferencia.component'
import { PerfilComponent } from './components/perfil/perfil.component'
import { AuthGuard } from './security/auth.guard';
import { OperacionesComponent } from './components/operaciones/operaciones.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard]},
  { path: 'registration', component: RegistrationComponent},
  { path: 'transferencia', component: TransferenciaComponent},
  { path: 'operaciones', component: OperacionesComponent},
  { path: 'perfil', component: PerfilComponent},
  { path: 'auth', component: AuthComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

