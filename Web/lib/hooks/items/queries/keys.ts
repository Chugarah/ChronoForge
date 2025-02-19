export const itemKeys = {
  all: ["items"] as const,
  lists: () => [...itemKeys.all, "list"] as const,
  list: (pageSize: number) => [...itemKeys.lists(), { pageSize }] as const,
  details: () => [...itemKeys.all, "detail"] as const,
  detail: (id: string) => [...itemKeys.details(), id] as const,
  projects: (page: number, pageSize: number) =>
    ["projects", { page, pageSize }] as const,
  status: (statusId: number) => ["status", statusId] as const,
  statuses: (ids: number[]) => ["statuses", ids] as const,
}; 