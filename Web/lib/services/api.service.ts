import { unstable_noStore as noStore } from 'next/cache'
import { cache } from 'react'
import type { PaginatedResponse } from '@/types/api.types'

// Default headers for API requests
const DEFAULT_HEADERS = {
  'Content-Type': 'application/json',
} as const

// Minimum loading time in milliseconds for better UX
const MIN_LOADING_TIME = 1000 // Increased from 800ms to 1000ms for better visibility

// Helper function to ensure minimum loading time
async function withMinLoadingTime<T>(promise: Promise<T>): Promise<T> {
  const start = Date.now()
  const result = await promise
  const elapsed = Date.now() - start
  if (elapsed < MIN_LOADING_TIME) {
    await new Promise(resolve => setTimeout(resolve, MIN_LOADING_TIME - elapsed))
  }
  return result
}

// Helper function to handle API errors
function handleApiError(response: Response) {
  if (response.status === 404) {
    throw new Error('The requested resource was not found')
  }
  if (response.status === 401) {
    throw new Error('You are not authorized to access this resource')
  }
  if (response.status === 403) {
    throw new Error('You do not have permission to access this resource')
  }
  if (response.status === 500) {
    throw new Error('An internal server error occurred')
  }
  if (!response.ok) {
    throw new Error('An unexpected error occurred')
  }
}

// Preload pattern for eager data fetching
export const preloadData = <T>(baseUrl: string, page: number, pageSize: number) => {
  void fetchDataItems<T>(baseUrl, page, pageSize)
}

// Cached data fetching function
export const fetchDataItems = cache(async <T>(
  baseUrl: string,
  page = 1,
  pageSize = 10,
  options?: { noCache?: boolean }
): Promise<PaginatedResponse<T>> => {
  // Opt out of caching for dynamic data requirements
  if (options?.noCache) {
    noStore()
  }

  try {
    // Always use withMinLoadingTime to ensure consistent loading experience
    const response = await withMinLoadingTime(
      fetch(baseUrl, {
        headers: DEFAULT_HEADERS,
      })
    )
    
    handleApiError(response)
    
    const data = await response.json()
    
    // Return empty response if no data
    if (!data || !Array.isArray(data)) {
      const emptyResponse: PaginatedResponse<T> = {
        items: [],
        totalItems: 0,
        total: 0,
        page: page,
        pageSize: pageSize,
        totalPages: 0
      }
      return emptyResponse
    }
    
    // Convert array response to paginated format
    const items = data as T[]
    const totalItems = items.length
    const total = totalItems // For backward compatibility
    const totalPages = Math.ceil(totalItems / pageSize)
    const start = (page - 1) * pageSize
    const end = start + pageSize
    
    return {
      items: items.slice(start, end),
      totalItems,
      total,
      page,
      pageSize,
      totalPages
    }
  } catch (error) {
    console.error('Error fetching data:', error)
    throw error instanceof Error 
      ? error 
      : new Error('Something went wrong while fetching data')
  }
})

// Function to create a new item with baseUrl
export function createItemWithBaseUrl<T>(baseUrl: string) {
  return async (data: Partial<T>) => {
    const response = await withMinLoadingTime(
      fetch(baseUrl, {
        method: 'POST',
        headers: DEFAULT_HEADERS,
        body: JSON.stringify(data),
      })
    )
    
    handleApiError(response)
    return response.json() as Promise<T>
  }
}

// Function to update an item with baseUrl
export function updateItemWithBaseUrl<T>(baseUrl: string) {
  return async (id: string | number, data: Partial<T>) => {
    const response = await withMinLoadingTime(
      fetch(`${baseUrl}/${id}`, {
        method: 'PUT',
        headers: DEFAULT_HEADERS,
        body: JSON.stringify(data),
      })
    )
    
    handleApiError(response)
    return response.json() as Promise<T>
  }
}

// Function to delete an item with baseUrl
export function deleteItemWithBaseUrl(baseUrl: string) {
  return async (id: string | number) => {
    const response = await withMinLoadingTime(
      fetch(`${baseUrl}/${id}`, {
        method: 'DELETE',
        headers: DEFAULT_HEADERS,
      })
    )
    
    handleApiError(response)
    return response.json() as Promise<{ success: boolean }>
  }
}

// Function to revalidate data with baseUrl
export function revalidateDataWithBaseUrl(baseUrl: string) {
  return async () => {
    const response = await fetch(`${baseUrl}/revalidate`)
    handleApiError(response)
    return response.json()
  }
} 