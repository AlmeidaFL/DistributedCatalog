export class User {
    id: string;
    name: string;
    email: string;
    password: string;
    phone: string;
    cnpj: string;
    role: string;

    constructor(init?: Partial<User>) {
      Object.assign(this, init);
    }

  }
  
export enum Role {
    Vendor = "Vendor",
    Client = "Client",
    Admin = "Admin"
}

function tryParseEnum<T>(enumObj: T, value: string): T[keyof T] | null {
  if (Object.values(enumObj!).includes(value as T[keyof T])) {
    return value as T[keyof T];
  }
  return null;
}