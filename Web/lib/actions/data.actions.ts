'use server'

import type { APIErrorData, DataItem } from '@/types/api.types'
import { APIError } from '@/types/api.types'

// Default headers for API requests
const DEFAULT_HEADERS = {
  'Content-Type': 'application/json',
} as const

// Utility function for handling API responses
async function handleResponse<T>(response: Response): Promise<T> {
  if (!response.ok) {
    const errorData = await response.json().catch(() => ({ message: 'An error occurred' })) as APIErrorData
    throw new APIError(
      errorData.message || 'An error occurred',
      response.status,
      errorData
    )
  }
  const data = await response.json()
  return data as T
}

// Create new item
export async function createItem(data: Omit<DataItem, 'id' | 'createdAt'>, baseUrl: string) {
  try {
    const response = await fetch(`${baseUrl}/items`, {
      method: 'POST',
      headers: DEFAULT_HEADERS,
      body: JSON.stringify(data),
    })

    return handleResponse<DataItem>(response)
  } catch (error) {
    console.error('Error creating item:', error)
    throw error instanceof APIError 
      ? error 
      : new APIError('Failed to create item', 500)
  }
}

// Update existing item
export async function updateItem(id: string, data: Partial<DataItem>, baseUrl: string) {
  try {
    const response = await fetch(`${baseUrl}/items/${id}`, {
      method: 'PATCH',
      headers: DEFAULT_HEADERS,
      body: JSON.stringify(data),
    })

    return handleResponse<DataItem>(response)
  } catch (error) {
    console.error('Error updating item:', error)
    throw error instanceof APIError 
      ? error 
      : new APIError('Failed to update item', 500)
  }
}

// Delete item
export async function deleteItem(id: string, baseUrl: string) {
  try {
    const response = await fetch(`${baseUrl}/items/${id}`, {
      method: 'DELETE',
      headers: DEFAULT_HEADERS,
    })

    return handleResponse<{ success: boolean }>(response)
  } catch (error) {
    console.error('Error deleting item:', error)
    throw error instanceof APIError 
      ? error 
      : new APIError('Failed to delete item', 500)
  }
}

// Utility function to revalidate cache
export async function revalidateData(baseUrl: string) {
  try {
    await fetch(`${baseUrl}/revalidate?tag=data-items`, { method: 'POST' })
  } catch (error) {
    console.error('Error revalidating data:', error)
    throw error instanceof APIError 
      ? error 
      : new APIError('Failed to revalidate data', 500)
  }
} 