export const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL || 'https://localhost:7228'
export const API_ENDPOINTS = {
  projects: `${API_BASE_URL}/api/Project/GetAllProjects`
} as const

// Default headers for API requests
export const DEFAULT_HEADERS = {
  'Content-Type': 'application/json',
  'Accept': 'application/json',
} as const 