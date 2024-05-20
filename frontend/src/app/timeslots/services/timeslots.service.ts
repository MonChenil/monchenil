import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NullablePartial } from '../../shared/shared.types';
import { TimeSlot } from '../models/timeslot';
import { environment } from '../../../environments/environment';

@Injectable()
export class TimeSlotsService {
  constructor(private http: HttpClient) {}

  create(timeSlot: NullablePartial<TimeSlot>) {
    return this.http.post(environment.backendTimeSlots, timeSlot);
  }

  getAll() {
    return this.http.get<TimeSlot[]>(environment.backendTimeSlots);
  }
}
