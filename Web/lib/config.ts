export const API_CONFIG = {
    BASE_URL: 'https://localhost:7228',
    ENDPOINTS: {
        PROJECTS: '/api/Project/GetAllProjects',
        STATUS: '/api/Status',
        STATUS_BY_ID: (id: number) => `/api/Status/${id}`,
        PROJECTMANAGER_BY_ID: (id: number) => `/api/User/${id}`
    }
} as const;

// Default headers for API requests
export const DEFAULT_HEADERS = {
  'Content-Type': 'application/json',
  'Accept': 'application/json',
} as const 