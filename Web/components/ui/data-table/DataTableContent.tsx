'use client'

import { flexRender } from '@tanstack/react-table'
import type { Table } from '@tanstack/react-table'
import { TableCell, TableRow } from '@/components/ui/table'

interface DataTableContentProps<TData> {
  table: Table<TData>
}

export function DataTableContent<TData>({ table }: DataTableContentProps<TData>) {
  return table.getRowModel().rows.map((row) => (
    <TableRow
      key={row.id}
      data-state={row.getIsSelected() && "selected"}
      className="transition-colors duration-200 hover:bg-[#27282B]"
    >
      {row.getVisibleCells().map((cell) => (
        <TableCell key={cell.id}>
          {flexRender(cell.column.columnDef.cell, cell.getContext())}
        </TableCell>
      ))}
    </TableRow>
  ))
} 