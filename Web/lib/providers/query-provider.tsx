'use client'

import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { ReactQueryDevtools } from '@tanstack/react-query-devtools'
import { type ReactNode, useState } from 'react'

export function QueryProvider({ children }: { children: ReactNode }) {
  const [queryClient] = useState(
    () =>
      new QueryClient({
        defaultOptions: {
          queries: {
            // Disable automatic background refetching
            staleTime: 60 * 1000, // Consider data fresh for 1 minute
            gcTime: 5 * 60 * 1000, // Keep unused data in memory for 5 minutes
            retry: 1, // Only retry failed requests once
            refetchOnWindowFocus: false, // Disable refetching on window focus
          },
          mutations: {
            retry: 1,
          },
        },
      })
  )

  return (
    <QueryClientProvider client={queryClient}>
      {children}
      <ReactQueryDevtools initialIsOpen={false} />
    </QueryClientProvider>
  )
} 