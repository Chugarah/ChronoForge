// Data Types - Base interface for general data items
// You can extend this for other item types if needed
export interface DataItem {
  id: string
  title: string
  description: string
  status: 'pending' | 'completed' | 'failed'
  createdAt: string
}

// Response Types - Used for paginated API responses
// Generic type T allows this to be used with any data type
export interface PaginatedResponse<T> {
  items: T[]
  page: number
  totalPages: number
  totalItems: number
  pageSize: number
  total: number
}

// Error Types - Used for API error handling
// You can extend APIErrorData with additional error properties if needed
export interface APIErrorData {
  message?: string
  [key: string]: unknown
}

// API Error class for better error handling
// You can extend this with additional error types if needed
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

// Status interface - Represents project status
// You can add more fields like color, icon, or order if needed
export interface Status {
  id: number;
  name: string;
}

// User interface - Represents project managers/users
// You can extend this with additional user properties like:
// - role: string
// - avatar: string
// - department: string
// - phoneNumber: string
export interface User {
  id: number;
  firstName: string;
  lastName: string;
}

// Project interface - Main project type
// You can extend this with additional project properties like:
// - priority: string
// - tags: string[]
// - category: string
// - budget: number
// - progress: number
export interface Project {
  id: number;
  title: string;
  description?: string;
  startDate: string;
  endDate: string;
  statusId: number;
  status: Status | null;
  projectManager: number;
  projectManagerData?: User | null;
}

// Task interface - Represents project tasks
// You can extend this with additional task properties like:
// - assignee: number
// - priority: string
// - tags: string[]
// - parentTaskId: number
export interface Task {
  id: number;
  title: string;
  description: string;
  status: 'todo' | 'in-progress' | 'completed';
  projectId: number;
  startDate: string;
  endDate: string;
  createdAt: string;
  updatedAt: string;
} 