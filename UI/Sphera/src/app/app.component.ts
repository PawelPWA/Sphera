import { Component, OnInit } from '@angular/core';
import { OrdersService } from './services/orders.service';
import { SalesOrderYear } from './models/sales-order-year.model';
import { CommonModule, CurrencyPipe, DecimalPipe } from '@angular/common';
import { environment } from '../environments/environment';
import { OrderDetail } from './models/order-detail.model';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CurrencyPipe, DecimalPipe, CommonModule, ScrollingModule, FormsModule, ReactiveFormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  salesOrderYears: SalesOrderYear[] | null = null;
  orderDetails: OrderDetail[] | null = null;
  maxTimeToShipInDays: number = environment.maxTimeToShipInDays;
  isOrdersLoading = false;
  isDetailsLoading = false;

  filterForm = new FormGroup({
    maximumTotal: new FormControl('', [
      Validators.pattern('^[0-9]*[.]?[0-9]*$'),
    ])
  });

  constructor(private ordersService: OrdersService) {
  }


  public getData(): void {
    const maxTotal = this.filterForm.get('maximumTotal')?.value ?? '';

    this.isOrdersLoading = true;
    this.ordersService.getOrders(maxTotal).subscribe(data => {
      this.salesOrderYears = data;
      this.isOrdersLoading = false;
    })
  }

  public getDetails(orderYear: number): void {
    this.isDetailsLoading = true;
    this.ordersService.getOrderDetails(orderYear).subscribe(data => {
      this.orderDetails = data;
      this.isDetailsLoading = false;
    })
  }

  public searchInNewTab(firstName: string, lastName: string) {
    const searchQuery = encodeURIComponent(`${firstName} ${lastName}`);
    const searchUrl = environment.browserUrl.replace('{searchQuery}', searchQuery);
    window.open(searchUrl, '_blank');
  }
}
