import type { Status, User } from "@/types/api.types";

/**
 * Type guard to check if a value is a Status
 */
export function isStatus(value: unknown): value is Status {
  return (
    value !== null &&
    typeof value === "object" &&
    "id" in value &&
    "name" in value &&
    typeof (value as Status).id === "number" &&
    typeof (value as Status).name === "string"
  );
}

/**
 * Type guard to check if a value is a User
 */
export function isUser(value: unknown): value is User {
  return (
    value !== null &&
    typeof value === "object" &&
    "id" in value &&
    typeof (value as User).id === "number"
  );
} 