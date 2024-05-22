import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NullablePartial } from '../../shared/shared.types';
import { Pet } from '../models/pet';
import { environment } from '../../../environments/environment';

@Injectable()
export class PetsService {
  constructor(private http: HttpClient) {}

  create(pet: NullablePartial<Pet>) {
    return this.http.post(environment.backendPets, pet);
  }

  delete(id: number) {
    return this.http.delete(`${environment.backendPets}/${id}`);
  }

  getAll() {
    return this.http.get<Pet[]>(environment.backendPets);
  }
}