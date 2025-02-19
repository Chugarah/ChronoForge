'use client'

import type { Table } from '@tanstack/react-table'
import { ChevronLeft, ChevronRight } from 'lucide-react'
import { Button } from '@/components/ui/button'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'

interface DataTablePaginationProps<TData> {
  table: Table<TData>
}

export function DataTablePagination<TData>({
  table,
}: DataTablePaginationProps<TData>) {
  return (
    <div className="w-full bg-[hsl(var(--table-header-bg))] border-t border-[hsl(var(--table-border))]">
      <div className="flex items-center justify-end px-4 py-3 space-x-6">
        {/* Rows per page */}
        <div className="flex items-center space-x-2">
          <p className="text-sm text-[hsl(var(--table-text-secondary))]">
            Rows per page
          </p>
          <Select
            value={`${table.getState().pagination.pageSize}`}
            onValueChange={(value) => {
              table.setPageSize(Number(value))
            }}
          >
            <SelectTrigger className="h-8 w-[70px] bg-[hsl(var(--table-search-bg))] border-[hsl(var(--table-border))]">
              <SelectValue placeholder={table.getState().pagination.pageSize} />
            </SelectTrigger>
            <SelectContent 
              side="top" 
              className="bg-[hsl(var(--table-search-bg))] border-[hsl(var(--table-border))]"
            >
              {[5, 10, 20, 30, 40, 50].map((pageSize) => (
                <SelectItem 
                  key={pageSize} 
                  value={`${pageSize}`}
                  className="text-sm h-8 flex items-center justify-center hover:bg-[hsl(var(--table-row-hover))] cursor-pointer select-none [&>span:has(svg)]:hidden"
                >
                  {pageSize}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
        </div>

        {/* Page navigation */}
        <div className="flex items-center space-x-2">
          <Button
            variant="outline"
            size="icon"
            className="h-8 w-8 bg-[hsl(var(--table-search-bg))] border-[hsl(var(--table-border))] disabled:opacity-50"
            onClick={() => table.previousPage()}
            disabled={!table.getCanPreviousPage()}
          >
            <ChevronLeft className="h-4 w-4" />
          </Button>
          <div className="text-sm text-[hsl(var(--table-text-secondary))] whitespace-nowrap px-2">
            Page {table.getState().pagination.pageIndex + 1} of{" "}
            {table.getPageCount()}
          </div>
          <Button
            variant="outline"
            size="icon"
            className="h-8 w-8 bg-[hsl(var(--table-search-bg))] border-[hsl(var(--table-border))] disabled:opacity-50"
            onClick={() => table.nextPage()}
            disabled={!table.getCanNextPage()}
          >
            <ChevronRight className="h-4 w-4" />
          </Button>
        </div>
      </div>
    </div>
  )
} 