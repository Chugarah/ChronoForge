import type { PaginatedResponse, Project, Status, User } from "@/types/api.types";

// Query Data interface for paginated project data
// Used to store the results of infinite queries
export interface QueryData {
  pages: PaginatedResponse<Project>[];
  pageParams: number[];
}

// Options for fetching data
// You can extend this with additional options like:
// - retryCount?: number
// - cacheTime?: number
// - refetchInterval?: number
export interface FetchOptions {
  strategy?: "parallel" | "sequential";
}

// Single Status query result
// Used when fetching individual status items in parallel
export interface StatusQueryResult {
  data?: Status;
  isLoading: boolean;
  isError: boolean;
}

// Sequential Status query result
// Used when fetching multiple status items sequentially
export interface SequentialStatusQueryResult {
  data?: Status[];
  isLoading: boolean;
  isError: boolean;
}

// Combined type for both parallel and sequential status queries
export type StatusQueries = StatusQueryResult[] | [SequentialStatusQueryResult];

// Single User query result
// Used when fetching individual user items in parallel
// You can extend this with additional query metadata like:
// - lastUpdated?: Date
// - isFetching?: boolean
// - isStale?: boolean
export interface UserQueryResult {
  data?: User;
  isLoading: boolean;
  isError: boolean;
}

// Sequential User query result
// Used when fetching multiple user items sequentially
// You can extend this with additional batch metadata like:
// - batchSize?: number
// - currentBatch?: number
export interface SequentialUserQueryResult {
  data?: User[];
  isLoading: boolean;
  isError: boolean;
}

// Combined type for both parallel and sequential user queries
export type UserQueries = UserQueryResult[] | [SequentialUserQueryResult]; 