import { Pet } from '../../pets/models/pet';

export type Reservation = {
  id: {
    value: string;
  }
  ownerId: string;
  startDate: string;
  endDate: string;
  pets: Pet[];
};