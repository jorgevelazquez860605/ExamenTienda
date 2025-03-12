import { Routes } from '@angular/router';
import { TiendaComponent } from './components/tienda/tienda.component';
import { LoginComponent } from './components/login/login.component';
import { RegistroComponent } from './components/registro/registro.component';
import { CarritoComponent } from './components/carrito/carrito.component';

export const appRoutes: Routes = [
  { path: '', redirectTo: 'tiendas', pathMatch: 'full' },
  { path: 'tiendas', component: TiendaComponent },
  { path: 'login', component: LoginComponent },
  { path: 'registro', component: RegistroComponent },
  { path: 'carrito', component: CarritoComponent }
];
