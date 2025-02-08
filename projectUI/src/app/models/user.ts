export interface User {
  id?: number;
  username: string;
  password?: string;
}

export interface Item {
  id?: number;
  name: string;
  description: string;
  createdAt?: Date;
}