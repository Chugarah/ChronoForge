'use client'

import type { ColumnDef } from '@tanstack/react-table'
import { Skeleton } from '@/components/ui/skeleton'
import { TableCell, TableRow } from '@/components/ui/table'

interface DataTableLoadingProps<TData, TValue> {
  columns: ColumnDef<TData, TValue>[]
  rowCount?: number
}

export function DataTableLoading<TData, TValue>({ 
  columns,
  rowCount = 5 
}: DataTableLoadingProps<TData, TValue>) {
  return Array.from({ length: rowCount }).map(() => (
    <TableRow key={`skeleton-row-${crypto.randomUUID()}`}>
      {columns.map((column) => (
        <TableCell key={`skeleton-cell-${column.id}-${crypto.randomUUID()}`}>
          <Skeleton className="h-6 w-full" />
        </TableCell>
      ))}
    </TableRow>
  ))
} 