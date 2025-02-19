import { useMutation, useQueryClient } from "@tanstack/react-query";
import { API_CONFIG } from "@/lib/config";
import {
  createItemWithBaseUrl,
  deleteItemWithBaseUrl,
  updateItemWithBaseUrl,
} from "@/lib/services/api.service";
import type { Project } from "@/types/api.types";
import { itemKeys } from "../queries/keys";
import type { QueryData } from "../types";

// Hook for creating a new item
export function useCreateItem() {
  const queryClient = useQueryClient();
  const createItem = createItemWithBaseUrl(
    `${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.PROJECTS}`,
  );

  return useMutation({
    mutationFn: createItem,
    onMutate: async (newItem) => {
      await queryClient.cancelQueries({ queryKey: itemKeys.lists() });
      const previousItems = queryClient.getQueryData<QueryData>(itemKeys.lists());

      queryClient.setQueryData<QueryData>(itemKeys.lists(), (old) => {
        if (!old) return { pages: [], pageParams: [] };
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
        };
      });

      return { previousItems };
    },
    onError: (_err, _newItem, context) => {
      if (context?.previousItems) {
        queryClient.setQueryData<QueryData>(itemKeys.lists(), context.previousItems);
      }
    },
    onSettled: () => {
      queryClient.invalidateQueries({ queryKey: itemKeys.lists() });
    },
  });
}

// Hook for updating an item
export function useUpdateItem() {
  const queryClient = useQueryClient();
  const updateItem = updateItemWithBaseUrl(
    `${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.PROJECTS}`,
  );

  return useMutation({
    mutationFn: ({ id, data }: { id: number; data: Partial<Project> }) =>
      updateItem(id, data),
    onMutate: async ({ id, data }) => {
      await queryClient.cancelQueries({ queryKey: itemKeys.lists() });
      const previousItems = queryClient.getQueryData<QueryData>(itemKeys.lists());

      queryClient.setQueryData<QueryData>(itemKeys.lists(), (old) => {
        if (!old) return { pages: [], pageParams: [] };
        return {
          ...old,
          pages: old.pages.map((page) => ({
            ...page,
            items: page.items.map((item) =>
              item.id === id ? { ...item, ...data } : item,
            ),
          })),
        };
      });

      return { previousItems };
    },
    onError: (_err, _vars, context) => {
      if (context?.previousItems) {
        queryClient.setQueryData<QueryData>(itemKeys.lists(), context.previousItems);
      }
    },
    onSettled: () => {
      queryClient.invalidateQueries({ queryKey: itemKeys.lists() });
    },
  });
}

// Hook for deleting an item
export function useDeleteItem() {
  const queryClient = useQueryClient();
  const deleteItem = deleteItemWithBaseUrl(
    `${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.PROJECTS}`,
  );

  return useMutation({
    mutationFn: deleteItem,
    onMutate: async (deletedId) => {
      await queryClient.cancelQueries({ queryKey: itemKeys.lists() });
      const previousItems = queryClient.getQueryData<QueryData>(itemKeys.lists());

      queryClient.setQueryData<QueryData>(itemKeys.lists(), (old) => {
        if (!old) return { pages: [], pageParams: [] };
        return {
          ...old,
          pages: old.pages.map((page) => ({
            ...page,
            items: page.items.filter((item) => item.id !== deletedId),
          })),
        };
      });

      return { previousItems };
    },
    onError: (_err, _id, context) => {
      if (context?.previousItems) {
        queryClient.setQueryData<QueryData>(itemKeys.lists(), context.previousItems);
      }
    },
    onSettled: () => {
      queryClient.invalidateQueries({ queryKey: itemKeys.lists() });
    },
  });
} 