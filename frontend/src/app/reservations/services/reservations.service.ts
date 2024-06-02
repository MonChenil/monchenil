import { formatDate } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable()
export class ReservationsService {
  constructor(private http: HttpClient) {}

  getArrivalTimes(startDate: Date, endDate: Date) {
    const now = new Date();
    if (startDate < now) {
      startDate = now;
    }

    let formattedStartDate;
    let formattedEndDate;

    try {
      formattedStartDate = formatDate(startDate, 'yyyy-MM-ddTHH:mm:ss', 'en');
      formattedEndDate = formatDate(endDate, 'yyyy-MM-ddTHH:mm:ss', 'en');
    } catch (error) {
      console.error(error);
      return of([]);
    }

    return this.http.get<string[]>(
      `${environment.backendReservations}/arrival-times`,
      {
        params: {
          StartDate: formattedStartDate,
          ...(endDate && { EndDate: formattedEndDate }),
        },
      },
    );
  }
}
