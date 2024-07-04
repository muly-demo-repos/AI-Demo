import { InputJsonValue } from "../../types";

export type UserUpdateInput = {
  email?: string | null;
  firstName?: string | null;
  lastName?: string | null;
  newField?: boolean | null;
  password?: string;
  roles?: InputJsonValue;
  username?: string;
};
