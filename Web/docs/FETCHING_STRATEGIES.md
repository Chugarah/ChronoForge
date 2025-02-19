# Fetching Strategies Guide

This guide explains how to use different fetching strategies in the application for optimizing data loading performance.

## Available Strategies

The application supports two fetching strategies for loading projects and their associated statuses:

1. **Parallel** (Default)
2. **Sequential**

## Usage

### Basic Usage

```typescript
import { useProjectsAndStatus } from "@/lib/hooks/use-items";

// Default (Parallel) Strategy
const { projects, isLoading, isError } = useProjectsAndStatus(1, 10);

// Explicit Parallel Strategy
const { projects, isLoading, isError } = useProjectsAndStatus(1, 10, { 
  strategy: 'parallel' 
});

// Sequential Strategy
const { projects, isLoading, isError } = useProjectsAndStatus(1, 10, { 
  strategy: 'sequential' 
});
```

### When to Use Each Strategy

#### Parallel Fetching (Default)
Best for:
- Fast loading of multiple items
- When you need data as quickly as possible
- Modern browsers and good network conditions
- When the server can handle multiple concurrent requests

```typescript
// Example: Loading projects in a dashboard where speed is crucial
function Dashboard() {
  const { projects, isLoading } = useProjectsAndStatus(1, 10, {
    strategy: 'parallel'
  });

  if (isLoading) return <LoadingSpinner />;
  
  return <ProjectList projects={projects} />;
}
```

#### Sequential Fetching
Best for:
- Reducing server load
- Limited bandwidth situations
- When you need to control the rate of requests
- Older browsers or devices with limited resources

```typescript
// Example: Loading projects in a resource-conscious manner
function ProjectViewer() {
  const { projects, isLoading } = useProjectsAndStatus(1, 10, {
    strategy: 'sequential'
  });

  if (isLoading) return <LoadingSpinner />;
  
  return <ProjectList projects={projects} />;
}
```

## Performance Considerations

### Parallel Fetching
- **Pros:**
  - Faster overall loading time
  - Better user experience for fast connections
  - Reduced total waiting time
- **Cons:**
  - Higher server load
  - More concurrent network requests
  - Higher memory usage

### Sequential Fetching
- **Pros:**
  - Controlled server load
  - Predictable network usage
  - Better for limited resources
- **Cons:**
  - Longer total loading time
  - May not be ideal for time-sensitive applications
  - User might wait longer for complete data

## Best Practices

1. **Default to Parallel:**
   ```typescript
   // Good: Using parallel by default for better performance
   const { projects } = useProjectsAndStatus(1, 10);
   ```

2. **Use Sequential for Resource Constraints:**
   ```typescript
   // Good: Using sequential when needed
   const { projects } = useProjectsAndStatus(1, 10, {
     strategy: 'sequential'
   });
   ```

3. **Consider Dynamic Strategy Selection:**
   ```typescript
   // Better: Choosing strategy based on conditions
   const strategy = networkQuality === 'low' ? 'sequential' : 'parallel';
   const { projects } = useProjectsAndStatus(1, 10, { strategy });
   ```

4. **Handle Loading States:**
   ```typescript
   // Good: Proper loading state handling
   const { projects, isLoading, isError } = useProjectsAndStatus(1, 10);
   
   if (isLoading) return <LoadingSpinner />;
   if (isError) return <ErrorMessage />;
   ```

## Error Handling

Both strategies include built-in error handling:

```typescript
const { 
  projects, 
  isLoading, 
  isError,
  error 
} = useProjectsAndStatus(1, 10, {
  strategy: 'parallel' // or 'sequential'
});

if (isError) {
  console.error('Failed to load projects:', error);
  return <ErrorBoundary error={error} />;
}
```

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

## Real-World Examples

### 1. Project List with Parallel Loading
```typescript
// components/main/Project/ListProjects.tsx
const ListProjects = () => {
  const { 
    projects,
    isLoading, 
    isError, 
    fetchNextPage, 
    hasNextPage,
    isFetchingNextPage
  } = useProjectsAndStatus(1, 10, {
    strategy: 'parallel' // Best for main list views
  });

  const projectsData = projects.data?.pages.flatMap(page => 
    page.items.map(item => ({
      ...item,
      status: item.status || null
    }))
  ) ?? [];

  // ... rest of the component
};
```

### 2. Project Details with Sequential Loading
```typescript
// components/main/Project/ProjectDetails.tsx
const ProjectDetails = ({ projectId }: { projectId: number }) => {
  const { 
    projects,
    isLoading 
  } = useProjectsAndStatus(1, 1, {
    strategy: 'sequential' // Better for single item views
  });

  const project = projects.data?.pages[0]?.items[0];

  return (
    <div>
      {isLoading ? (
        <LoadingSpinner />
      ) : project ? (
        <div>
          <h1>{project.title}</h1>
          <StatusBadge status={project.status} />
          {/* ... other project details */}
        </div>
      ) : (
        <div>Project not found</div>
      )}
    </div>
  );
};
```

### 3. Dashboard with Network-Aware Loading
```typescript
// components/Dashboard/ProjectsDashboard.tsx
const ProjectsDashboard = () => {
  // Example of network-condition based strategy selection
  const [networkQuality, setNetworkQuality] = useState<'high' | 'low'>('high');

  useEffect(() => {
    // Check network conditions
    const connection = (navigator as any).connection;
    if (connection) {
      const updateNetworkQuality = () => {
        setNetworkQuality(
          connection.downlink >= 1.5 ? 'high' : 'low'
        );
      };
      connection.addEventListener('change', updateNetworkQuality);
      updateNetworkQuality();
      return () => connection.removeEventListener('change', updateNetworkQuality);
    }
  }, []);

  const { 
    projects,
    isLoading 
  } = useProjectsAndStatus(1, 10, {
    strategy: networkQuality === 'high' ? 'parallel' : 'sequential'
  });

  return (
    <DashboardLayout>
      <ProjectMetrics projects={projects} />
      <ProjectTimeline projects={projects} />
      {/* ... other dashboard components */}
    </DashboardLayout>
  );
};
```

### 4. Infinite Scroll with Strategy Switching
```typescript
// components/main/Project/InfiniteProjectList.tsx
const InfiniteProjectList = () => {
  const [pageSize, setPageSize] = useState(10);
  const [currentPage, setCurrentPage] = useState(1);

  const { 
    projects,
    isLoading,
    fetchNextPage,
    hasNextPage,
    isFetchingNextPage 
  } = useProjectsAndStatus(currentPage, pageSize, {
    // Use parallel for initial load, sequential for subsequent pages
    strategy: currentPage === 1 ? 'parallel' : 'sequential'
  });

  const loadMore = () => {
    if (hasNextPage && !isFetchingNextPage) {
      setCurrentPage(prev => prev + 1);
      fetchNextPage();
    }
  };

  return (
    <div>
      <ProjectGrid projects={projects} />
      {hasNextPage && (
        <Button 
          onClick={loadMore}
          disabled={isFetchingNextPage}
        >
          {isFetchingNextPage ? 'Loading...' : 'Load More'}
        </Button>
      )}
    </div>
  );
};
```

### 5. Error Recovery with Strategy Fallback
```typescript
// components/main/Project/ResilientProjectList.tsx
const ResilientProjectList = () => {
  const [fetchStrategy, setFetchStrategy] = useState<'parallel' | 'sequential'>('parallel');
  const [retryCount, setRetryCount] = useState(0);

  const { 
    projects,
    isLoading,
    isError,
    error 
  } = useProjectsAndStatus(1, 10, {
    strategy: fetchStrategy
  });

  useEffect(() => {
    if (isError && retryCount < 3) {
      // If parallel fetching fails, try sequential
      setFetchStrategy('sequential');
      setRetryCount(prev => prev + 1);
    }
  }, [isError, retryCount]);

  if (isError && retryCount >= 3) {
    return (
      <ErrorDisplay 
        error={error}
        onRetry={() => {
          setRetryCount(0);
          setFetchStrategy('parallel');
        }}
      />
    );
  }

  return (
    <div>
      <ProjectTable 
        data={projects}
        isLoading={isLoading}
        fetchStrategy={fetchStrategy}
      />
    </div>
  );
};
```

These examples demonstrate different scenarios where each strategy might be appropriate, along with practical implementations and considerations for:
- Network conditions
- User experience
- Error handling
- Performance optimization
- Resource management

// ... rest of existing documentation ... 