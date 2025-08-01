import { Component } from '@angular/core';
import { ProductService } from '../../services/product-service';
import { CartItem } from '../../models/Cart';
import { environment } from '../../../environments/environment';
import { Router, RouterLink } from '@angular/router';
import { ProductCard } from "../product-card/product-card";

@Component({
  selector: 'app-cart',
  imports: [ProductCard, RouterLink],
  templateUrl: './cart.html',
  styleUrl: './cart.css'
})
export class Cart {
  cartItems: CartItem[] = [];
  isLoading = true;
  error?: string;
  ImageUrl = `${environment.apiBaseUrl}/Image/`;

  constructor(private productService: ProductService, private router:Router) {}

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart(): void {
    const cart = JSON.parse(localStorage.getItem('cart') || '[]');

    this.cartItems = [];
    this.isLoading = true;
    if (!cart.length) {
      this.isLoading = false;
      return;
    }

    let loaded = 0;
    cart.forEach((item: { productId: number; quantity: number }) => {
      this.productService.getProductById(item.productId).subscribe({
        next: (product) => {
          this.cartItems.push({ product, productId: item.productId, quantity: item.quantity });
          loaded++;
          if (loaded === cart.length) this.isLoading = false;
        },
        error: () => {
          this.error = 'Failed to load some products.';
          loaded++;
          if (loaded === cart.length) this.isLoading = false;
        }
      });
    });
  }

  clearCart(): void {
    localStorage.removeItem('cart');
    this.cartItems = [];
    this.router.navigateByUrl('/home');
  }
}