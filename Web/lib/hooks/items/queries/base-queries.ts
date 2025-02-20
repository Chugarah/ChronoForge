import { 
  useInfiniteQuery,
  useQuery
} from "@tanstack/react-query";
import { API_CONFIG } from "@/lib/config";
import { fetchDataItems } from "@/lib/services/api.service";
import type { Project, Status } from "@/types/api.types";
import { itemKeys } from "./keys";
import { handleAPIResponse } from "../utils/query-config";

interface PaginatedResponse<T> {
  items: T[];
  page: number;
  totalPages: number;
  totalItems: number;
}

/**
 * Base hook for fetching paginated data
 */
export function useItems<T>(
  page: number,
  pageSize: number,
  baseUrl: string,
  queryKey: string,
) {
  return useInfiniteQuery<PaginatedResponse<T>>({
    queryKey: [queryKey, { page, pageSize }] as const,
    queryFn: async ({ pageParam }) => {
      const currentPage = (pageParam ?? page) as number;
      return fetchDataItems<T>(baseUrl, currentPage, pageSize, { noCache: true });
    },
    getNextPageParam: (lastPage: PaginatedResponse<T>) => {
      if (lastPage.page < lastPage.totalPages) {
        return lastPage.page + 1;
      }
      return undefined;
    },
    initialPageParam: page,
    staleTime: 30000, // 30 seconds
    gcTime: 5 * 60 * 1000, // 5 minutes
    retry: 2,
  });
}

/**
 * Hook for fetching projects with pagination
 */
export function useProjects(page: number, pageSize: number) {
  return useItems<Project>(
    page,
    pageSize,
    `${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.PROJECTS}`,
    "projects",
  );
}

/**
 * Hook for fetching status with pagination
 */
export function useStatus(page: number, pageSize: number) {
  return useItems<Status>(
    page,
    pageSize,
    `${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.STATUS}`,
    "status",
  );
}

/**
 * Hook for fetching a specific status by ID
 */
export function useStatusById(statusId: number) {
  const endpoint = `${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.STATUS_BY_ID(statusId)}`;

  return useQuery<Status>({
    queryKey: itemKeys.status(statusId),
    queryFn: async () => {
      const response = await fetch(
        endpoint,
        { headers: { Accept: "application/json" } }
      );
      return handleAPIResponse<Status>(response, endpoint);
    },
    enabled: statusId > 0,
    staleTime: 30000, // 30 seconds
    gcTime: 5 * 60 * 1000, // 5 minutes
    retry: 2,
  });
} 