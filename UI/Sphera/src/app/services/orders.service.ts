import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrderDetail } from '../models/order-detail.model';
import { SalesOrderYear } from '../models/sales-order-year.model';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  constructor(private httpClient: HttpClient) { }

  public getOrders(maximumTotal:string): Observable<SalesOrderYear[]> {
    return this.httpClient.get<SalesOrderYear[]>(`${environment.apiUrl}/Orders/Get/${maximumTotal}`);
  }

  public getOrderDetails(orderYear: number): Observable<OrderDetail[]> {
    return this.httpClient.get<OrderDetail[]>(`${environment.apiUrl}/Orders/GetDetails/${orderYear}`);
  }
}
