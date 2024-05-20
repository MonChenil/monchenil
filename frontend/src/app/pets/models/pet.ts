enum PetType {
  Cat,
  Dog,
}

export type Pet = {
  id: number;
  name: string;
  type: PetType;
  incompatibleTypes: PetType[];
};
