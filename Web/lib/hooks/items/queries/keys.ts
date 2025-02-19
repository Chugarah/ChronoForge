// Query keys for React Query cache management
// These keys are used to identify and cache query results
export const itemKeys = {
  // Base key for all items
  all: ["items"] as const,

  // Keys for list operations
  lists: () => [...itemKeys.all, "list"] as const,
  list: (pageSize: number) => [...itemKeys.lists(), { pageSize }] as const,

  // Keys for detail operations
  details: () => [...itemKeys.all, "detail"] as const,
  detail: (id: string) => [...itemKeys.details(), id] as const,

  // Project-specific query keys
  // You can extend with additional parameters like:
  // - filter?: string
  // - sort?: string
  // - status?: number
  projects: (page: number, pageSize: number) =>
    ["projects", { page, pageSize }] as const,

  // Status-specific query keys
  // You can add variations like:
  // - statusByName: (name: string) => ["status", "by-name", name]
  // - activeStatuses: () => ["status", "active"]
  status: (statusId: number) => ["status", statusId] as const,
  statuses: (ids: number[]) => ["statuses", ids] as const,

  // Project manager (User) query keys
  // You can add variations like:
  // - projectManagerByEmail: (email: string) => ["projectManager", "by-email", email]
  // - activeProjectManagers: () => ["projectManager", "active"]
  projectManager: (userId: number) => ["projectManager", userId] as const,
}; 