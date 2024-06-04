import { formatDate } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Reservation } from '../models/reservation';

@Injectable()
export class ReservationsService {
  constructor(private http: HttpClient) {}

  getArrivalTimes(startDate: Date, endDate: Date, petIdsAsString: string) {
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
          ...(petIdsAsString && { PetIds: petIdsAsString }),
        },
      },
    );
  }

  getDepartureTimes(startDate: Date, endDate: Date, petIdsAsString: string) {
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
      `${environment.backendReservations}/departure-times`,
      {
        params: {
          StartDate: formattedStartDate,
          ...(endDate && { EndDate: formattedEndDate }),
          ...(petIdsAsString && { PetIds: petIdsAsString }),
        },
      },
    );
  }

  createReservation(reservation: any) {
    return this.http.post(environment.backendReservations, reservation);
  }

  getReservations() {
    return this.http.get<Reservation[]>(environment.backendReservations);
  }

  deleteReservation(id: string) {
    return this.http.delete(`${environment.backendReservations}/${id}`);
  }
}
