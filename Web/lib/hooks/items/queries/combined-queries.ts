import { useInfiniteQuery, useQueries, useQuery } from "@tanstack/react-query";
import { API_CONFIG } from "@/lib/config";
import { fetchDataItems } from "@/lib/services/api.service";
import type { Project, Status, User } from "@/types/api.types";
import { itemKeys } from "./keys";
import type { FetchOptions, SequentialStatusQueryResult, SequentialUserQueryResult, StatusQueryResult, UserQueryResult } from "../types";

// Type guard to check if a value is a Status
// You can extend this with additional type checks if Status interface changes
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

// Type guard to check if a value is a User
// You can extend this with additional type checks if User interface changes
// Example: Add checks for new required fields like role or department
function isUser(value: unknown): value is User {
  return (
    value !== null &&
    typeof value === "object" &&
    value !== null &&
    "id" in value &&
    typeof (value as User).id === "number"
  );
}

// Combined hook for projects, status, and project managers
// This hook fetches:
// 1. Projects with pagination
// 2. Status data for each project
// 3. Project manager data for each project
// You can extend this hook to:
// - Add filtering options
// - Add sorting options
// - Add search functionality
// - Add data transformation
export function useProjectsAndStatus(
  page: number,
  pageSize: number,
  options: FetchOptions = { strategy: "parallel" },
) {
  // First fetch projects with pagination
  // You can extend this query with:
  // - Additional query parameters
  // - Custom error handling
  // - Custom data transformation
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

  // Extract unique status IDs from projects
  // You can extend this to:
  // - Filter out specific status IDs
  // - Add validation for status IDs
  // - Add transformation of IDs
  const statusIds =
    projectsQuery.data?.pages
      .flatMap((page) => 
        page.items.map((project: Project) => project.statusId)
      )
      .filter((id): id is number => id != null && id !== 0) ?? [];

  // Extract unique project manager IDs from projects
  // You can extend this to:
  // - Filter out specific user IDs
  // - Add validation for user IDs
  // - Add transformation of IDs
  const projectManagerIds =
    projectsQuery.data?.pages
      .flatMap((page) => 
        page.items.map((project: Project) => project.projectManager)
      )
      .filter((id): id is number => id != null && id !== 0) ?? [];

  // Deduplicate IDs to avoid redundant fetches
  const uniqueStatusIds = Array.from(new Set(statusIds));
  const uniqueProjectManagerIds = Array.from(new Set(projectManagerIds));

  // Fetch status data either in parallel or sequentially
  // You can extend this to:
  // - Add custom error handling per status
  // - Add retry logic
  // - Add data transformation
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

  // Fetch project manager data either in parallel or sequentially
  // You can extend this to:
  // - Add custom error handling per user
  // - Add retry logic
  // - Add data transformation
  const projectManagerQueries =
    options.strategy === "parallel"
      ? // Parallel: Fetch all project managers at once using useQueries
        useQueries({
          queries: uniqueProjectManagerIds.map((userId) => ({
            queryKey: itemKeys.projectManager(userId),
            queryFn: async () => {
              const response = await fetch(
                `${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.PROJECTMANAGER_BY_ID(userId)}`,
                { headers: { Accept: "application/json" } },
              );
              if (!response.ok) {
                throw new Error("Failed to fetch project manager");
              }
              return response.json() as Promise<User>;
            },
            enabled: !!projectsQuery.data && userId > 0,
            staleTime: 30000,
            gcTime: 5 * 60 * 1000,
            retry: 2,
          })),
        })
      : // Sequential: Fetch one project manager at a time
        [
          useQuery<User[]>({
            queryKey: ["projectManager", "sequential", uniqueProjectManagerIds],
            queryFn: async () => {
              const users: User[] = [];
              for (const userId of uniqueProjectManagerIds) {
                if (userId > 0) {
                  const response = await fetch(
                    `${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.PROJECTMANAGER_BY_ID(userId)}`,
                    { headers: { Accept: "application/json" } },
                  );
                  if (!response.ok) {
                    throw new Error("Failed to fetch project manager");
                  }
                  const user = (await response.json()) as User;
                  users.push(user);
                }
              }
              return users;
            },
            enabled: !!projectsQuery.data && uniqueProjectManagerIds.length > 0,
            staleTime: 30000,
            gcTime: 5 * 60 * 1000,
            retry: 2,
          }),
        ] as [SequentialUserQueryResult];

  // Create maps for efficient lookups
  // You can extend these maps to:
  // - Add data transformation
  // - Add validation
  // - Add default values
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

  const projectManagerMap = Object.fromEntries(
    options.strategy === "parallel"
      ? (projectManagerQueries as UserQueryResult[])
          .map((query, index) => {
            const user = query.data;
            return [uniqueProjectManagerIds[index], isUser(user) ? user : null];
          })
          .filter(
            (entry): entry is [number, User | null] =>
              typeof entry[0] === "number",
          )
      : ((projectManagerQueries[0]?.data ?? []) as User[])
          .map((user, index) => [
            uniqueProjectManagerIds[index],
            isUser(user) ? user : null,
          ])
          .filter(
            (entry): entry is [number, User | null] =>
              typeof entry[0] === "number",
          ),
  );

  // Map status and project manager data to projects
  // You can extend this mapping to:
  // - Add data transformation
  // - Add computed fields
  // - Add validation
  const projectsWithData = projectsQuery.data?.pages.map((page) => ({
    ...page,
    items: page.items.map((project: Project) => {
      const status = project.statusId
        ? statusMap[project.statusId] ?? null
        : null;
      const projectManagerData = project.projectManager
        ? projectManagerMap[project.projectManager] ?? null
        : null;
      return {
        ...project,
        status,
        projectManagerData,
      };
    }),
  }));

  // Track loading state across all queries
  // You can extend this to:
  // - Add granular loading states
  // - Add loading progress
  const isLoading =
    projectsQuery.isLoading ||
    (options.strategy === "parallel"
      ? (statusQueries as StatusQueryResult[]).some((query) => query.isLoading) ||
        (projectManagerQueries as UserQueryResult[]).some((query) => query.isLoading)
      : (statusQueries[0]?.isLoading ?? false) ||
        (projectManagerQueries[0]?.isLoading ?? false));

  // Track error state across all queries
  // You can extend this to:
  // - Add granular error states
  // - Add error details
  // - Add retry functionality
  const isError =
    projectsQuery.isError ||
    (options.strategy === "parallel"
      ? (statusQueries as StatusQueryResult[]).some((query) => query.isError) ||
        (projectManagerQueries as UserQueryResult[]).some((query) => query.isError)
      : (statusQueries[0]?.isError ?? false) ||
        (projectManagerQueries[0]?.isError ?? false));

  // Return combined query results
  // You can extend this return object to include:
  // - Additional metadata
  // - Computed values
  // - Helper functions
  return {
    projects: {
      ...projectsQuery,
      data:
        projectsWithData && projectsQuery.data
          ? {
              pages: projectsWithData,
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