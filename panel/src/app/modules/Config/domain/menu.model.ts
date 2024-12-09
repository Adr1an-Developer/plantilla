export interface IMenu {
  id: string; // Primary key
  name: string; // Menu name
  url: string; // URL
  title: string; // Menu title
  icon: string; // Menu icon
  parentMenuId?: string | null; // Parent menu id (null if root menu)
  order: number; // Menu display order
  group: boolean;
  parentMenu?: string;
  isActive: boolean;
  isDeleted: boolean;
  creationDate: string;
  createByUser: string;
  modificationDate: string | null;
  updateByUser: string | null;
}

export interface IAddMenu {
  name: string; // Menu name
  url: string; // URL
  title: string; // Menu title
  icon: string; // Menu icon
  parentMenuId?: string | null; // Parent menu id (null if root menu)
  order: number;
  group: boolean;
}
