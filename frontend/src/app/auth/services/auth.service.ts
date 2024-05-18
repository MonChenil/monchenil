import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable()
export class AuthService {
  constructor(private http: HttpClient) {}

  isAuthenticated(): Observable<any> {
    return this.http.get(environment.backendIsAuthenticated);
  }

  login(data: any): Observable<any> {
    return this.http.post(environment.backendLogin, data);
  }

  register(data: any): Observable<any> {
    return this.http.post(environment.backendRegister, data);
  }
}
