import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CaptchaService {
  private baseUrl = environment.apiBaseUrl;
  
  constructor(private http: HttpClient) {}
  
  validateCaptcha(token: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/Captcha/validate`, { token });
  }
}
