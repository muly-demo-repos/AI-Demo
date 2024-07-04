import { JsonValue } from "type-fest";

export type User = {
  createdAt: Date;
  email: string | null;
  firstName: string | null;
  id: string;
  lastName: string | null;
  newField: boolean | null;
  roles: JsonValue;
  updatedAt: Date;
  username: string;
};
