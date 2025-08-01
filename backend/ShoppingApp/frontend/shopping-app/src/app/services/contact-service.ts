import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ContactRequest } from '../models/ContactRequest';

@Injectable({ providedIn: 'root' })
export class ContactService {
  private apiUrl = `${environment.apiBaseUrl}/ContactUs/add`;

  constructor(private http: HttpClient) {}

  submitContact(form: ContactRequest): Observable<any> {
    return this.http.post(this.apiUrl, form);
  }
}
