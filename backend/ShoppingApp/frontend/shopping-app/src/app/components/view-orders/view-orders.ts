import { Component, OnInit } from '@angular/core';
import { ProductCard } from "../product-card/product-card";
import { OrderService } from '../../services/order-service';
import { CurrencyPipe, DatePipe } from '@angular/common';

@Component({
  selector: 'app-view-orders',
  imports: [ProductCard, DatePipe, CurrencyPipe],
  templateUrl: './view-orders.html',
  styleUrl: './view-orders.css'
})
export class ViewOrders implements OnInit {
  orders: any[] = [];
  loading = false;
  selectedOrderId: number | null = null;

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.loading = true;
    this.orderService.getAllOrders().subscribe({
      next: (response) => {
        this.orders = response.$values;
        this.loading = false;
      },
      error: (err) => {
        console.error('Failed to fetch orders', err);
        this.loading = false;
      }
    });
  }
  toggleProducts(orderId: number) {
    this.selectedOrderId = this.selectedOrderId === orderId ? null : orderId;
  }
  generatePdf(): void {
    this.orderService.downloadOrderPdf().subscribe({
      next: (pdfBlob) => {
        const fileURL = window.URL.createObjectURL(pdfBlob);
        const a = document.createElement('a');
        a.href = fileURL;
        a.download = 'Orders.pdf';
        a.click();
        window.URL.revokeObjectURL(fileURL);
      },
      error: () => {
        alert('Failed to download PDF.');
      }
    });
  }
}