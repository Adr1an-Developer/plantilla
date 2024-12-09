export interface IAccess {
  id: string; // Primary key
  menuId: string; // Foreign key: refers to system_menu
  profileId: string; // Foreign key: refers to profiles
  canView: boolean; // Can view the menu
  canAdd: boolean; // Can add new items
  canEdit: boolean; // Can edit items
  canDelete: boolean; // Can delete items
  canExport: boolean; // Can export items
  canAuthorize: boolean; // Can authorize actions
  menuName?: string | null; // Not mapped to database
  profileName?: string | null; // Not mapped to database
  parentMenuId?: string | null; // Not mapped to database
  menuOrder?: number | null; // Not mapped to database
  isChild?: boolean | null; // Logic based on parentMenuId
  isActive: boolean;
  isDeleted: boolean;
  creationDate: string;
  createByUser: string;
  modificationDate: string | null;
  updateByUser: string | null;
}

export interface IAddAccess {
  menuId: string; // Foreign key: refers to system_menu
  profileId: string; // Foreign key: refers to profiles
  canView: boolean; // Can view the menu
  canAdd: boolean; // Can add new items
  canEdit: boolean; // Can edit items
  canDelete: boolean; // Can delete items
  canExport: boolean; // Can export items
  canAuthorize: boolean; // Can authorize actions
}
