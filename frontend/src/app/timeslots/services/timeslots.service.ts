import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { NullablePartial } from '../../shared/shared.types';
import { TimeSlot } from '../models/timeslot';

@Injectable()
export class TimeSlotsService {
  constructor(private http: HttpClient) {}

  create(timeSlot: NullablePartial<TimeSlot>) {
    return this.http.post(environment.backendTimeSlots, timeSlot);
  }

  delete(id: number) {
    return this.http.delete(`${environment.backendTimeSlots}/${id}`);
  }

  getAll() {
    return this.http.get<TimeSlot[]>(environment.backendTimeSlots);
  }
}
