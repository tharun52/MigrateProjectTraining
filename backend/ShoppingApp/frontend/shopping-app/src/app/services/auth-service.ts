import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable()
export class AuthService {
  private baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient, private router: Router) {}

  login(username: string, password: string) {
    return this.http.post<any>(`${this.baseUrl}/Auth/login`, { username, password });
  }

  logout() {
    const refreshToken = localStorage.getItem('refreshToken');
    if (refreshToken) {
      this.http.post(`${this.baseUrl}/Auth/logout`, { refreshToken }).subscribe();
    }
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('role');
    localStorage.removeItem('username');
    this.router.navigate(['/login']);
  }

  signup(username: string, password: string) {
    return this.http.post<any>(`${this.baseUrl}/Auth/register-user`, {
      username,
      password
    });
  }

  signupAsAdmin(username: string, password: string, secretKey: string) {
    return this.http.post<any>(`${this.baseUrl}/Auth/register-admin`, {
      username,
      password,
      secretKey
    });
  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem('token');
    if (!token) return false;

    const decoded = this.decodeToken(token);
    const exp = decoded?.exp;
    const currentTime = Math.floor(Date.now() / 1000);

    return exp && currentTime < exp;
  }


  getRole(): string | null {
    const token = localStorage.getItem('token');
    if (!token) return null;

    const decoded = this.decodeToken(token);
    return decoded?.role || null;
  }
  
  decodeToken(token: string): any {
    try {
      const payload = token.split('.')[1];
      const decodedPayload = atob(payload);
      return JSON.parse(decodedPayload);
    } catch (error) {
      console.error('Failed to decode token', error);
      return null;
    }
  }

  saveLoginData(response: any) {
    localStorage.setItem('token', response.token);
    localStorage.setItem('refreshToken', response.refreshToken);
    localStorage.setItem('role', response.role);
    localStorage.setItem('username', response.username);
  }
}