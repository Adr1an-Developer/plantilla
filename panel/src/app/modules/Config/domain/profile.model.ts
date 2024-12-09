export interface IProfile {
  id: string;
  name: string;
  abbreviation: string;
  users?: any;
  isActive: boolean;
  isDeleted: boolean;
  creationDate: string;
  modificationDate: string | null;
  updateByUser: string | null;
}

export interface IAddProfile {
  name: string;
  abbreviation: string;
}
