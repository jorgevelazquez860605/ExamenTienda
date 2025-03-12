import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-registro',
  standalone: true,
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.scss'],
  imports: [CommonModule, FormsModule, RouterModule] 
})
export class RegistroComponent {
  cliente = {
    nombre: '',
    apellidos: '',
    direccion: '',
    email: '',
    password: ''
  };

  mensajeExito: string = '';
  mensajeError: string = '';

  constructor(private apiService: ApiService, public router: Router) {} 

  registrar() {
    this.mensajeExito =  '';
    this.mensajeError = '';
    
    this.apiService.registrarCliente(this.cliente).subscribe(
      () => {
        this.mensajeExito = 'Registro exitoso. Redirigiendo...';
        setTimeout(() => this.router.navigate(['/login']), 2000);
      },
      (error) => {
        if (error.status !== 500) {
          this.mensajeError = error.error?.message || 'Error al registrar usuario.';
        } else {
          this.mensajeError = 'Error interno del servidor.';
        }
        console.error('Error en el registro:', error);
      }
    );
  }
}
