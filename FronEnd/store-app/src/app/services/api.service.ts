import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders  } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Articulo } from '../models/articulo.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
   private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken');
    return new HttpHeaders({ 
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  getTiendas(): Observable<any> {
    return this.http.get(`${this.apiUrl}/tienda`);
  }

  getArticulos(): Observable<Articulo[]> {
   
    return this.http.get<Articulo[]>(`${this.apiUrl}/Articulo`);
  }

  login(email: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/auth/login`, { email, password }, {
      headers: { 'Content-Type': 'application/json' }
    });
  }

  registrarCliente(cliente: any): Observable<any> {    
    return this.http.post(`${this.apiUrl}/Cliente`, cliente, {
      headers: { 'Content-Type': 'application/json' }
    });
  }

  obtenerCarrito(): Observable<any> {
    return this.http.get(`${this.apiUrl}/Carrito`, { headers: this.getAuthHeaders() });
  }

  agregarAlCarrito(articuloId: number): Observable<any> {
    return this.http.post(
      `${this.apiUrl}/Carrito/agregar-al-carrito`, 
      { articuloId }, 
      { headers: this.getAuthHeaders() }
    );
  }

  quitarUnidad(articuloId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Carrito/quitar-unidad/${articuloId}`, { headers: this.getAuthHeaders() });
  }

  eliminarArticulo(articuloId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Carrito/eliminar/${articuloId}`, { headers: this.getAuthHeaders() });
  }
  eliminarTodosArticulo(articuloId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Carrito/eliminar/todos/${articuloId}`, { headers: this.getAuthHeaders() });
  }
  
}
