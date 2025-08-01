import { Component, Input } from '@angular/core';
import { ProductService } from '../../services/product-service';
import { Product } from '../../models/Product';
import { DatePipe } from '@angular/common';
import { environment } from '../../../environments/environment';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-card',
  imports: [DatePipe, FormsModule, ReactiveFormsModule],
  templateUrl: './product-card.html',
  styleUrl: './product-card.css'
})
export class ProductCard {
  @Input() productId!: number;
  @Input() isCart: boolean = false;
  product?: Product;
  error?: string;
  quantity: number = 1; 
  ImageUrl = `${environment.apiBaseUrl}/Image/`;

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    if (this.productId) {
      this.productService.getProductById(this.productId).subscribe({
        next: (data) => (this.product = data),
        error: (err) => (this.error = 'Product not found.'),
      });
    }
  }

  addToCart(): void {
    if (!this.productId || this.quantity < 1) {
      alert("Invalid quantity or product.");
      return;
    }

    const cart = JSON.parse(localStorage.getItem('cart') || '[]');

    const index = cart.findIndex((item: any) => item.productId === this.productId);
    if (index !== -1) {
      cart[index].quantity += this.quantity;
    } else {
      cart.push({
        productId: this.productId,
        quantity: this.quantity
      });
    }

    localStorage.setItem('cart', JSON.stringify(cart));
    window.location.reload();
  }
}