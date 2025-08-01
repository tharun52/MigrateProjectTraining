import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { News } from '../models/News';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NewsService {
  private baseUrl = `${environment.apiBaseUrl}/News`;

  constructor(private http: HttpClient) {}

  getAllNews(): Observable<News[]> {
    return this.http.get<any>(this.baseUrl).pipe(
      map(response => response.$values || [])
    );  
  }


  exportNews(): Observable<Blob> {
    return this.http.get(`${this.baseUrl}/export`, {
      responseType: 'blob'
    });
  }
}
