import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Reservation } from '../models/reservation';

@Injectable()
export class ReservationsService {
  constructor(private http: HttpClient) {}
}