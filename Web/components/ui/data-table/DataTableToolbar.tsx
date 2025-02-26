"use client";

import type { Table } from "@tanstack/react-table";
import { Plus, RotateCw, Search, X } from "lucide-react";
import { Button } from "@/components/ui/button";
import { DataTableViewOptions } from "@/components/ui/data-table/DataTableViewOptions";
import { Input } from "@/components/ui/input";

interface DataTableToolbarProps<TData> {
	table: Table<TData>;
	onNewProject?: () => void;
	onRefresh?: () => Promise<void>;
	isLoading?: boolean;
}

export function DataTableToolbar<TData>({
	table,
	onNewProject,
	onRefresh,
	isLoading
}: DataTableToolbarProps<TData>) {
	const isFiltered = table.getState().columnFilters.length > 0;

	return (
		<div className="flex items-center justify-between px-4 py-1 min-h-[3.5rem] border-b border-table-border bg-table-header">
			<div className="flex items-center gap-3">
				<div className="relative">
					<Search className="absolute left-2 top-1/2 h-4 w-4 -translate-y-1/2 text-table-text-secondary" />
					<Input
						placeholder="Search projects..."
						value={(table.getColumn("title")?.getFilterValue() as string) ?? ""}
						onChange={(event) =>
							table.getColumn("title")?.setFilterValue(event.target.value)
						}
						className="h-8 w-[200px] pl-8 bg-table-header-search-bg hover:bg-table-header-search-hover border-table-header-search-border text-sm text-table-text-primary placeholder:text-table-header-search-placeholder focus:ring-1 focus:ring-table-header-search-ring rounded-md transition-colors duration-200"
					/>
				</div>
				<Button
					variant="outline"
					size="sm"
					onClick={async () => {
						try {
							await onRefresh?.();
						} catch (error) {
							console.error('Error refreshing:', error);
						}
					}}
					disabled={isLoading}
					className="group text-[hsl(var(--table-text-secondary))] hover:text-[hsl(var(--primary))] hover:border-[hsl(var(--primary))] hover:bg-[hsl(var(--primary)_/_0.1)] transition-all duration-200 cursor-pointer"
					type="button"
				>
					<RotateCw className={`h-4 w-4 ${isLoading ? 'animate-spin' : 'group-hover:-rotate-90'} transition-all duration-300`} />
				</Button>
				<DataTableViewOptions table={table} />
			</div>
			<Button
				onClick={onNewProject}
				size="sm"
				variant="rainbow"
			>
				<Plus className="mr-2 h-4 w-4" />
				New Project
			</Button>

			{isFiltered && (
				<Button
					variant="ghost"
					size="sm"
					onClick={() => table.resetColumnFilters()}
					className="absolute left-[220px] top-1/2 -translate-y-1/2"
				>
					<X className="h-4 w-4" />
				</Button>
			)}
		</div>
	);
} 