import { Pet } from '../../pets/models/pet';

export type Reservation = {
  id: string;
  ownerId: string;
  startDate: string;
  endDate: string;
  pets: Pet[];
};