import { useInfiniteQuery, useQuery } from "@tanstack/react-query";
import { API_CONFIG } from "@/lib/config";
import { fetchDataItems } from "@/lib/services/api.service";
import type { Project, Status } from "@/types/api.types";

// Base hook for fetching any type of data
export function useItems<T>(
  page: number,
  pageSize: number,
  baseUrl: string,
  queryKey: string,
) {
  return useInfiniteQuery({
    queryKey: [queryKey],
    queryFn: ({ pageParam = page }) =>
      fetchDataItems<T>(baseUrl, pageParam, pageSize, { noCache: true }),
    getNextPageParam: (lastPage) => {
      if (lastPage.page < lastPage.totalPages) {
        return lastPage.page + 1;
      }
      return undefined;
    },
    initialPageParam: page,
  });
}

// Specific hook for projects
export function useProjects(page: number, pageSize: number) {
  return useItems<Project>(
    page,
    pageSize,
    `${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.PROJECTS}`,
    "projects",
  );
}

// Specific hook for Status
export function useStatus(page: number, pageSize: number) {
  return useItems<Status>(
    page,
    pageSize,
    `${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.STATUS}`,
    "status",
  );
}

// Hook for fetching a specific status
export function useStatusById(statusId: number) {
  return useQuery({
    queryKey: ["status", statusId],
    queryFn: () =>
      fetchDataItems<Status>(
        `${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.STATUS_BY_ID(statusId)}`,
        1,
        1,
        { noCache: true },
      ),
  });
} 