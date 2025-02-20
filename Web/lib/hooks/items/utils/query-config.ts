import type { QueryKey, UseQueryOptions } from "@tanstack/react-query";

/**
 * Default configuration for queries
 */
export const defaultQueryConfig: Partial<UseQueryOptions<unknown, Error, unknown, QueryKey>> = {
  staleTime: 30000, // 30 seconds
  gcTime: 5 * 60 * 1000, // 5 minutes
  retry: 2,
};

/**
 * Custom error for API requests
 */
export class APIError extends Error {
  constructor(
    message: string,
    public statusCode?: number,
    public endpoint?: string
  ) {
    super(message);
    this.name = "APIError";
  }
}

/**
 * Handles API response
 */
export async function handleAPIResponse<T>(
  response: Response,
  endpoint: string
): Promise<T> {
  if (!response.ok) {
    throw new APIError(
      `Failed to fetch data: ${response.statusText}`,
      response.status,
      endpoint
    );
  }
  return response.json() as Promise<T>;
} 