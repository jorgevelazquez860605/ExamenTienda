import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  imports: [CommonModule]
})
export class HeaderComponent implements OnInit {
  isLoggedIn: boolean = false;
  totalCarrito: number = 0;
  constructor(private router: Router,private apiService: ApiService) {}

  ngOnInit(): void {
    this.checkLoginStatus();
  }
  goTo(url: string) {
    this.router.navigateByUrl(url);
  }
  checkLoginStatus() {
    this.isLoggedIn = !!localStorage.getItem('authToken');
    if (this.isLoggedIn) {
      this.actualizarCarrito();
    }
  }

  logout(): void {
    localStorage.removeItem('authToken');
    this.isLoggedIn = false;
    this.router.navigate(['/login']);
  }

  actualizarCarrito(): void {
    const clienteId = localStorage.getItem('clienteId');
    if (!clienteId) return;

    this.apiService.obtenerCarrito().subscribe(
      (data) => {
        this.totalCarrito = data.length;
      }
    );
  }
}
