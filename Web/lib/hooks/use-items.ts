'use client'

import { useInfiniteQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { API_ENDPOINTS } from '@/lib/config'
import { 
  createItemWithBaseUrl,
  deleteItemWithBaseUrl,
  fetchDataItems,
  updateItemWithBaseUrl,
} from '@/lib/services/api.service'
import type { PaginatedResponse, Project } from '@/types/api.types'

// Query keys for caching and invalidation
export const itemKeys = {
  all: ['items'] as const,
  lists: () => [...itemKeys.all, 'list'] as const,
  list: (pageSize: number) => [...itemKeys.lists(), { pageSize }] as const,
  details: () => [...itemKeys.all, 'detail'] as const,
  detail: (id: string) => [...itemKeys.details(), id] as const,
}

interface QueryData {
  pages: PaginatedResponse<Project>[]
  pageParams: number[]
}

// Hook for fetching paginated items
export function useItems(initialPage: number, pageSize: number) {
  return useInfiniteQuery({
    queryKey: itemKeys.list(pageSize),
    queryFn: ({ pageParam = initialPage }) => fetchDataItems<Project>(API_ENDPOINTS.projects, pageParam, pageSize),
    getNextPageParam: (lastPage) => {
      if (lastPage.page < lastPage.totalPages) {
        return lastPage.page + 1
      }
      return undefined
    },
    initialPageParam: initialPage,
  })
}

// Hook for creating a new item
export function useCreateItem() {
  const queryClient = useQueryClient()
  const createItem = createItemWithBaseUrl(API_ENDPOINTS.projects)

  return useMutation({
    mutationFn: createItem,
    onMutate: async (newItem) => {
      await queryClient.cancelQueries({ queryKey: itemKeys.lists() })
      const previousItems = queryClient.getQueryData<QueryData>(itemKeys.lists())

      queryClient.setQueryData<QueryData>(itemKeys.lists(), (old) => {
        if (!old) return { pages: [], pageParams: [] }
        return {
          ...old,
          pages: old.pages.map((page) => ({
            ...page,
            items: [
              {
                ...newItem,
                id: Date.now(),
                startDate: new Date().toISOString(),
                endDate: new Date().toISOString(),
              } as Project,
              ...page.items,
            ],
          })),
        }
      })

      return { previousItems }
    },
    onError: (_err, _newItem, context) => {
      if (context?.previousItems) {
        queryClient.setQueryData<QueryData>(itemKeys.lists(), context.previousItems)
      }
    },
    onSettled: () => {
      queryClient.invalidateQueries({ queryKey: itemKeys.lists() })
    },
  })
}

// Hook for updating an item
export function useUpdateItem() {
  const queryClient = useQueryClient()
  const updateItem = updateItemWithBaseUrl(API_ENDPOINTS.projects)

  return useMutation({
    mutationFn: ({ id, data }: { id: number; data: Partial<Project> }) =>
      updateItem(id, data),
    onMutate: async ({ id, data }) => {
      await queryClient.cancelQueries({ queryKey: itemKeys.lists() })
      const previousItems = queryClient.getQueryData<QueryData>(itemKeys.lists())

      queryClient.setQueryData<QueryData>(itemKeys.lists(), (old) => {
        if (!old) return { pages: [], pageParams: [] }
        return {
          ...old,
          pages: old.pages.map((page) => ({
            ...page,
            items: page.items.map((item) =>
              item.id === id ? { ...item, ...data } : item
            ),
          })),
        }
      })

      return { previousItems }
    },
    onError: (_err, _vars, context) => {
      if (context?.previousItems) {
        queryClient.setQueryData<QueryData>(itemKeys.lists(), context.previousItems)
      }
    },
    onSettled: () => {
      queryClient.invalidateQueries({ queryKey: itemKeys.lists() })
    },
  })
}

// Hook for deleting an item
export function useDeleteItem() {
  const queryClient = useQueryClient()
  const deleteItem = deleteItemWithBaseUrl(API_ENDPOINTS.projects)

  return useMutation({
    mutationFn: deleteItem,
    onMutate: async (deletedId) => {
      await queryClient.cancelQueries({ queryKey: itemKeys.lists() })
      const previousItems = queryClient.getQueryData<QueryData>(itemKeys.lists())

      queryClient.setQueryData<QueryData>(itemKeys.lists(), (old) => {
        if (!old) return { pages: [], pageParams: [] }
        return {
          ...old,
          pages: old.pages.map((page) => ({
            ...page,
            items: page.items.filter((item) => item.id !== deletedId),
          })),
        }
      })

      return { previousItems }
    },
    onError: (_err, _id, context) => {
      if (context?.previousItems) {
        queryClient.setQueryData<QueryData>(itemKeys.lists(), context.previousItems)
      }
    },
    onSettled: () => {
      queryClient.invalidateQueries({ queryKey: itemKeys.lists() })
    },
  })
} 