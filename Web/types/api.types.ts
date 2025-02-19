// Data Types
export interface DataItem {
  id: string
  title: string
  description: string
  status: 'pending' | 'completed' | 'failed'
  createdAt: string
}

// Response Types
export interface PaginatedResponse<T> {
  items: T[]
  total: number
  page: number
  pageSize: number
  totalPages: number
}

// Error Types
export interface APIErrorData {
  message?: string
  [key: string]: unknown
}

// API Error class for better error handling
export class APIError extends Error {
  constructor(
    message: string,
    public status: number,
    public data?: APIErrorData
  ) {
    super(message)
    this.name = 'APIError'
  }
}

export interface Project {
  id: number
  title: string
  description?: string
  startDate: string
  endDate: string
} 