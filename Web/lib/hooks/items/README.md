# Items Hooks Documentation

This directory contains a collection of React Query hooks for managing project and status data in the application.

## Directory Structure

```md
lib/hooks/items/
├── mutations/
│   └── index.ts           # Mutation hooks (create, update, delete)
├── queries/
│   ├── base-queries.ts    # Basic query hooks
│   ├── combined-queries.ts # Combined project and status queries
│   └── keys.ts            # Query key definitions
├── types/
│   └── index.ts           # Type definitions
└── index.ts               # Main exports
```

## Core Features

### 1. Query Hooks (`queries/`)

#### Base Queries (`base-queries.ts`)

- `useItems<T>`: Generic hook for fetching paginated data
- `useProjects`: Hook for fetching project data
- `useStatus`: Hook for fetching status data
- `useStatusById`: Hook for fetching a specific status

```typescript
// Example: Fetching projects
const { data, isLoading } = useProjects(1, 10);

// Example: Fetching a specific status
const { data } = useStatusById(statusId);
```

#### Combined Queries (`combined-queries.ts`)

- `useProjectsAndStatus`: Hook that combines project and status fetching with two strategies:
  - Parallel: Fetches all statuses simultaneously
  - Sequential: Fetches statuses one after another

```typescript
// Parallel fetching (default)
const { projects, isLoading } = useProjectsAndStatus(1, 10);

// Sequential fetching
const { projects, isLoading } = useProjectsAndStatus(1, 10, {
  strategy: 'sequential'
});
```

### 2. Mutation Hooks (`mutations/`)

- `useCreateItem`: Hook for creating new projects
- `useUpdateItem`: Hook for updating existing projects
- `useDeleteItem`: Hook for deleting projects

```typescript
// Example: Creating a new project
const createMutation = useCreateItem();
createMutation.mutate(newProjectData);

// Example: Updating a project
const updateMutation = useUpdateItem();
updateMutation.mutate({ id: projectId, data: updatedData });

// Example: Deleting a project
const deleteMutation = useDeleteItem();
deleteMutation.mutate(projectId);
```

### 3. Query Keys (`queries/keys.ts`)

Centralized query key definitions for cache management:

```typescript
export const itemKeys = {
  all: ["items"],
  projects: (page, pageSize) => ["projects", { page, pageSize }],
  status: (statusId) => ["status", statusId],
  // ... other keys
};
```

### 4. Types (`types/`)

Core type definitions for the hooks:

- `QueryData`: Type for paginated project data
- `FetchOptions`: Configuration options for fetching strategies
- `StatusQueryResult`: Type for status query results
- `SequentialStatusQueryResult`: Type for sequential status fetching

## Best Practices

### 1. Fetching Strategy Selection

Choose the appropriate strategy based on your needs:

```typescript
// Use parallel for better performance in list views
const ListProjects = () => {
  const { projects } = useProjectsAndStatus(1, 10, {
    strategy: 'parallel'
  });
  // ...
};

// Use sequential for resource-constrained situations
const DetailView = () => {
  const { projects } = useProjectsAndStatus(1, 10, {
    strategy: 'sequential'
  });
  // ...
};
```

### 2. Error Handling

All hooks include built-in error handling:

```typescript
const { projects, isError, isLoading } = useProjectsAndStatus(1, 10);

if (isLoading) return <LoadingSpinner />;
if (isError) return <ErrorMessage />;
```

### 3. Optimistic Updates

Mutations include optimistic updates with automatic rollback:

```typescript
const updateMutation = useUpdateItem();

// Updates UI immediately, rolls back on error
updateMutation.mutate({
  id: projectId,
  data: { title: "New Title" }
});
```

## Performance Considerations

### Parallel Fetching

- **Pros**: Faster overall loading, better UX for fast connections
- **Cons**: Higher server load, more concurrent requests

### Sequential Fetching

- **Pros**: Controlled server load, better for limited resources
- **Cons**: Longer total loading time

## Migration Guide

### From Sequential to Parallel

```typescript
// Before
const { projects } = useProjectsAndStatus(1, 10, {
  strategy: 'sequential'
});

// After
const { projects } = useProjectsAndStatus(1, 10, {
  strategy: 'parallel'
});
```

### From Parallel to Sequential

```typescript
// Before
const { projects } = useProjectsAndStatus(1, 10);

// After
const { projects } = useProjectsAndStatus(1, 10, {
  strategy: 'sequential'
});
```

## Contributing

When adding new features:

1. Place hooks in appropriate directories
2. Update types as needed
3. Add query keys for new data types
4. Include proper error handling
5. Add optimistic updates for mutations
6. Update this documentation
