export interface Person {
  id: string;
  givenName: string;
  surname: string;
  fullName: string;
  gender: Gender;
  birthDate: Date;
  birthLocation: string;
  deathDate: Date;
  deathLocation: string;
}

enum Gender {
  Male,
  Female,
  Other,
}
