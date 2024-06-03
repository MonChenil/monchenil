import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ReservationsService } from '../../services/reservations.service';
import { Reservation } from '../../models/reservation';
import { Pet } from '../../../pets/models/pet';

@Component({
  selector: 'reservations-card',
  templateUrl: './reservations-card.component.html'
})
export class ReservationsCardComponent {
  constructor(private reservationsService: ReservationsService) { }
  
  @Input() declare reservation: Reservation;
  @Output() reservationDeleted = new EventEmitter();

  displayDate(startDateParam: string, endDateParam: string): string {
    const startDate = new Date(startDateParam);
    const endDate = new Date(endDateParam);

    const formatNumber = (num: number) => num.toString().padStart(2, '0');
    const formatDate = (date: Date) => 
      `${formatNumber(date.getDate())}/${formatNumber(date.getMonth() + 1)}/${date.getFullYear()}`;
    const formatTime = (date: Date) => 
      `${formatNumber(date.getHours())}:${formatNumber(date.getMinutes())}`;

    const startFormatted = `Du ${formatDate(startDate)} à ${formatTime(startDate)}`;
    const endFormatted = `au ${formatDate(endDate)} à ${formatTime(endDate)}`;

    return `${startFormatted} ${endFormatted}`;
  }

  displayPets(pets: Pet[]): string {
    return pets
      .map(pet => pet.name.charAt(0).toUpperCase() + pet.name.slice(1))
      .join(', ');
  }

  deleteReservation(reservation: Reservation) {
    this.reservationsService
      .deleteReservation(reservation.id.value)
      .subscribe(() => this.reservationDeleted.emit(reservation));
  }
}
