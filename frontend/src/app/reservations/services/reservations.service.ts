import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable()
export class ReservationsService {
  constructor(private http: HttpClient) { }
  
  getArrivalTimes(startDate: string, endDate?: string) {
    let params = new HttpParams().set('StartDate', startDate);

    if (endDate) {
      params = params.set('EndDate', endDate);
    }
    
    return this.http.get<string[]>(`${environment.backendReservations}/arrival-times`, {
      params,
    });
  }
}