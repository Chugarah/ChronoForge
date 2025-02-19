import { useInfiniteQuery, useQueries, useQuery } from "@tanstack/react-query";
import { API_CONFIG } from "@/lib/config";
import { fetchDataItems } from "@/lib/services/api.service";
import type { Project, Status } from "@/types/api.types";
import { itemKeys } from "./keys";
import type { FetchOptions, SequentialStatusQueryResult, StatusQueryResult } from "../types";

// Type guard to check if a value is a Status
function isStatus(value: unknown): value is Status {
  return (
    value !== null &&
    typeof value === "object" &&
    value !== null &&
    "id" in value &&
    "name" in value &&
    typeof (value as Status).id === "number" &&
    typeof (value as Status).name === "string"
  );
}

// Combined hook for both projects and status
export function useProjectsAndStatus(
  page: number,
  pageSize: number,
  options: FetchOptions = { strategy: "parallel" },
) {
  // First fetch projects
  const projectsQuery = useInfiniteQuery({
    queryKey: itemKeys.projects(page, pageSize),
    queryFn: ({ pageParam = page }) =>
      fetchDataItems<Project>(
        `${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.PROJECTS}`,
        pageParam,
        pageSize,
        { noCache: true },
      ),
    getNextPageParam: (lastPage) => {
      if (lastPage.page < lastPage.totalPages) {
        return lastPage.page + 1;
      }
      return undefined;
    },
    initialPageParam: page,
  });

  // Get all unique status IDs from the projects
  const statusIds =
    projectsQuery.data?.pages
      .flatMap((page) => 
        page.items.map((project: Project) => project.statusId)
      )
      .filter((id): id is number => id != null && id !== 0) ?? [];

  // Deduplicate status IDs
  const uniqueStatusIds = Array.from(new Set(statusIds));

  const statusQueries =
    options.strategy === "parallel"
      ? // Parallel: Fetch all statuses at once using useQueries
        useQueries({
          queries: uniqueStatusIds.map((statusId) => ({
            queryKey: itemKeys.status(statusId),
            queryFn: async () => {
              const response = await fetch(
                `${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.STATUS_BY_ID(statusId)}`,
                { headers: { Accept: "application/json" } },
              );
              if (!response.ok) {
                throw new Error("Failed to fetch status");
              }
              return response.json() as Promise<Status>;
            },
            enabled: !!projectsQuery.data && statusId > 0,
            staleTime: 30000,
            gcTime: 5 * 60 * 1000,
            retry: 2,
          })),
        })
      : // Sequential: Fetch one status at a time
        [
          useQuery<Status[]>({
            queryKey: ["status", "sequential", uniqueStatusIds],
            queryFn: async () => {
              const statuses: Status[] = [];
              for (const statusId of uniqueStatusIds) {
                if (statusId > 0) {
                  const response = await fetch(
                    `${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.STATUS_BY_ID(statusId)}`,
                    { headers: { Accept: "application/json" } },
                  );
                  if (!response.ok) {
                    throw new Error("Failed to fetch status");
                  }
                  const status = (await response.json()) as Status;
                  statuses.push(status);
                }
              }
              return statuses;
            },
            enabled: !!projectsQuery.data && uniqueStatusIds.length > 0,
            staleTime: 30000,
            gcTime: 5 * 60 * 1000,
            retry: 2,
          }),
        ] as [SequentialStatusQueryResult];

  // Create a map of statusId to status data
  const statusMap = Object.fromEntries(
    options.strategy === "parallel"
      ? (statusQueries as StatusQueryResult[])
          .map((query, index) => {
            const status = query.data;
            return [uniqueStatusIds[index], isStatus(status) ? status : null];
          })
          .filter(
            (entry): entry is [number, Status | null] =>
              typeof entry[0] === "number",
          )
      : ((statusQueries[0]?.data ?? []) as Status[])
          .map((status, index) => [
            uniqueStatusIds[index],
            isStatus(status) ? status : null,
          ])
          .filter(
            (entry): entry is [number, Status | null] =>
              typeof entry[0] === "number",
          ),
  );

  // Map status data to projects
  const projectsWithStatus = projectsQuery.data?.pages.map((page) => ({
    ...page,
    items: page.items.map((project: Project) => {
      const status = project.statusId
        ? statusMap[project.statusId] ?? null
        : null;
      return {
        ...project,
        status,
      } as Project;
    }),
  }));

  const isLoading =
    projectsQuery.isLoading ||
    (options.strategy === "parallel"
      ? (statusQueries as StatusQueryResult[]).some((query) => query.isLoading)
      : statusQueries[0]?.isLoading ?? false);

  const isError =
    projectsQuery.isError ||
    (options.strategy === "parallel"
      ? (statusQueries as StatusQueryResult[]).some((query) => query.isError)
      : statusQueries[0]?.isError ?? false);

  return {
    projects: {
      ...projectsQuery,
      data:
        projectsWithStatus && projectsQuery.data
          ? {
              pages: projectsWithStatus,
              pageParams: projectsQuery.data.pageParams,
            }
          : projectsQuery.data,
    },
    isLoading,
    isError,
    hasNextPage: projectsQuery.hasNextPage,
    fetchNextPage: projectsQuery.fetchNextPage,
    isFetchingNextPage: projectsQuery.isFetchingNextPage,
    strategy: options.strategy,
  };
} 