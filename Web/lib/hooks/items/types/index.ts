import type { PaginatedResponse, Project, Status } from "@/types/api.types";

export interface QueryData {
  pages: PaginatedResponse<Project>[];
  pageParams: number[];
}

export interface FetchOptions {
  strategy?: "parallel" | "sequential";
}

export interface StatusQueryResult {
  data?: Status;
  isLoading: boolean;
  isError: boolean;
}

export interface SequentialStatusQueryResult {
  data?: Status[];
  isLoading: boolean;
  isError: boolean;
}

export type StatusQueries = StatusQueryResult[] | [SequentialStatusQueryResult]; 