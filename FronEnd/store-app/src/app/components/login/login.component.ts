import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  imports: [CommonModule, FormsModule] 
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  mensajeError: string = '';
  mensajeExito: string = '';

  constructor(private apiService: ApiService, private router: Router) {}

  login() {
    this.mensajeError = '';
    this.mensajeExito = '';

    if (!this.email || !this.password) {
      this.mensajeError = 'Todos los campos son obligatorios';
      return;
    }

    this.apiService.login(this.email, this.password).subscribe(
      (response) => {
        if (response && response.token) {
          localStorage.setItem('authToken', response.token);
          this.mensajeExito = 'Inicio de sesión exitoso. Redirigiendo...';

          setTimeout(() => {
            this.router.navigateByUrl('/tiendas').then(() => {
              window.location.reload(); // ✅ Actualiza el header sin problemas
            });
          }, 1000);
        }
      },
      (error) => {
        this.mensajeError = error.status === 401 
          ? 'Credenciales incorrectas' 
          : 'Error al iniciar sesión';
      }
    );
  }
}
