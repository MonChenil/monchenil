import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Pet } from '../models/pet';

@Injectable()
export class PetsService {
  constructor(private http: HttpClient) {}

  createPet(pet: Pet) {
    return this.http.post(environment.backendPets, pet);
  }

  deletePet(id: string) {
    return this.http.delete(`${environment.backendPets}/${id}`);
  }

  getCurrentUserPets() {
    return this.http.get<Pet[]>(environment.backendPets);
  }
}
