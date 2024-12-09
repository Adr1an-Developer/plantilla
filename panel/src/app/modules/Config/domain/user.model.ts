export interface IUser {
  id: string;
  userName: string;
  profileId: string;
  firstName: string;
  lastName: string;
  email: string;
  isFirstLogin: boolean;
  externalCode: string | null;
  profileName: string;
  isActive: boolean;
  isDeleted: boolean;
  creationDate: string;
  createByUser: string;
  modificationDate: string | null;
  updateByUser: string | null;
}

export interface IAddUser {
  userName: string;
  profileId: string;
  firstName: string;
  lastName: string;
  email: string;
  externalCode: string | null;
}

export interface IUserLogged {
  UserId: string;
  FullName: string;
  Profile: string;
  Email: string;
  Username: string;
  Name: string;
  isFirstLogin: boolean;
}

export interface IChangePassword {
  oldPassword: string;
  UserId: string;
  newPassword: string;
  confirmNewPassword: string;
}
