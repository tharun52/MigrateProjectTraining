import { Component } from '@angular/core';
import { ProductCard } from "../product-card/product-card";
import { ProductService } from '../../services/product-service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-products',
  imports: [ProductCard, RouterLink],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products {
  productIds: number[] = [];
  error?: string;
  isLoading: boolean = false;
  cartCount: number = 0;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.updateCartCount();
    this.isLoading = true;
    this.productService.getAllProducts().subscribe({
      next: (products) => {
        this.productIds = products.map((p: any) => p.productId);
        this.isLoading = false;
      },
      error: (err) => {
        this.error = 'Failed to fetch products.';
        this.isLoading = false;
      }
    });
  }
  updateCartCount(): void {
    const cart = JSON.parse(localStorage.getItem('cart') || '[]');
    this.cartCount = cart.reduce((sum: number, item: any) => sum + item.quantity, 0);
  }
}