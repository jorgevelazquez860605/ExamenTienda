import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { ApiService } from '../../services/api.service';
import { Articulo } from '../../models/articulo.model'; 
import Swal from 'sweetalert2';

@Component({
  selector: 'app-tienda',
  standalone: true, 
  templateUrl: './tienda.component.html',
  styleUrls: ['./tienda.component.scss'],
  imports: [CommonModule] 
})
export class TiendaComponent implements OnInit {
  articulos: Articulo[] = [];
  imagenDefaultBase64: string = '';
  isLoggedIn: boolean = false;
  mensajeExito: string = '';
  mensajeError: string = '';

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {    
    this.cargarImagenDefault();
    this.obtenerArticulos();
  }

  obtenerArticulos(): void {
    this.checkLoginStatus();
    this.apiService.getArticulos().subscribe(
      (data) => {        
         this.articulos = data;
      },
      (error) => {
        console.error('Error al obtener los artículos', error);
      }
    );
  }

  checkLoginStatus() {
    this.isLoggedIn = !!localStorage.getItem('authToken');
  }

  agregarAlCarrito(articuloId: number) {
    this.apiService.agregarAlCarrito(articuloId).subscribe(
      () => {
        Swal.fire({
          icon: 'success',
          title: '¡Artículo agregado!',
          text: 'El artículo ha sido añadido al carrito con éxito.',
          showConfirmButton: false,
          timer: 2000
        });
      },
      (error) => {
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'No se pudo agregar el artículo al carrito.',
          showConfirmButton: false,
          timer: 2000
        });
      }
    );
  }

 cargarImagenDefault(): void {
    this.imagenDefaultBase64 = 'data:image/png;base64,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';
  }  
  onImageError(event: Event) {
    const imgElement = event.target as HTMLImageElement;
    imgElement.src = this.imagenDefaultBase64;
  }
}
