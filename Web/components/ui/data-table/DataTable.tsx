"use client";

import {
	type ColumnDef,
	type ColumnFiltersState,
	getCoreRowModel,
	getFacetedRowModel,
	getFacetedUniqueValues,
	getFilteredRowModel,
	getPaginationRowModel,
	getSortedRowModel,
	type SortingState,
	useReactTable,
	type VisibilityState,
} from "@tanstack/react-table";
import { flexRender } from "@tanstack/react-table";
import { AnimatePresence, motion } from "framer-motion";
import { Loader2 } from "lucide-react";
import * as React from "react";
import { useInView } from "react-intersection-observer";
import { DataTablePagination } from "@/components/ui/data-table/DataTablePagination";
import { DataTableToolbar } from "@/components/ui/data-table/DataTableToolbar";
import {
	Table,
	TableBody,
	TableCell,
	TableHead,
	TableHeader,
	TableRow,
} from "@/components/ui/table";
import { DataTableLoading } from "./DataTableLoading";

interface DataTableProps<TData, TValue> {
	columns: ColumnDef<TData, TValue>[];
	data: TData[];
	isLoading?: boolean;
	isError?: boolean;
	pageCount: number;
	onPaginationChange?: (page: number) => void;
	onRowClick?: (id: number) => void;
	onRefresh?: () => Promise<void>;
}

export function DataTable<TData extends { id: number }, TValue>({
	columns,
	data,
	isLoading,
	isError,
	pageCount,
	onPaginationChange,
	onRowClick,
	onRefresh,
}: DataTableProps<TData, TValue>) {
	const [sorting, setSorting] = React.useState<SortingState>([]);
	const [columnFilters, setColumnFilters] = React.useState<ColumnFiltersState>(
		[],
	);
	const [columnVisibility, setColumnVisibility] =
		React.useState<VisibilityState>({});
	const { ref, inView } = useInView();

	React.useEffect(() => {
		if (inView && !isLoading && onPaginationChange) {
			const currentPage = table.getState().pagination.pageIndex;
			onPaginationChange(currentPage + 1);
		}
	}, [inView, isLoading, onPaginationChange]);

	const table = useReactTable({
		data,
		columns,
		state: {
			sorting,
			columnFilters,
			columnVisibility,
		},
		enableRowSelection: false,
		enableMultiRowSelection: false,
		enableSubRowSelection: false,
		onSortingChange: setSorting,
		onColumnFiltersChange: setColumnFilters,
		onColumnVisibilityChange: setColumnVisibility,
		getCoreRowModel: getCoreRowModel(),
		getFilteredRowModel: getFilteredRowModel(),
		getPaginationRowModel: getPaginationRowModel(),
		getSortedRowModel: getSortedRowModel(),
		getFacetedRowModel: getFacetedRowModel(),
		getFacetedUniqueValues: getFacetedUniqueValues(),
		pageCount: pageCount,
	});

	if (isError) {
		return (
			<AnimatePresence mode="wait">
				<motion.div
					initial={{ opacity: 0, y: 20 }}
					animate={{ opacity: 1, y: 0 }}
					exit={{ opacity: 0, y: -20 }}
					transition={{ duration: 0.3, ease: "easeOut" }}
					className="flex flex-col items-center justify-center p-12 text-center"
				>
					<motion.div
						initial={{ scale: 0.8 }}
						animate={{ scale: 1 }}
						transition={{ delay: 0.1 }}
						className="mb-6 rounded-full bg-red-500/10 p-4"
					>
						<svg
							className="h-8 w-8 text-red-500"
							fill="none"
							stroke="currentColor"
							viewBox="0 0 24 24"
							aria-hidden="true"
						>
							<path
								strokeLinecap="round"
								strokeLinejoin="round"
								strokeWidth={2}
								d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z"
							/>
						</svg>
					</motion.div>
					<h3 className="mb-2 text-lg font-medium text-table-text-primary">
						Error Loading Data
					</h3>
					<p className="text-sm text-table-text-secondary">
						There was a problem loading your data. Please try again later.
					</p>
				</motion.div>
			</AnimatePresence>
		);
	}

	return (
		<div className="space-y-2.5">
			<DataTableToolbar table={table} onRefresh={onRefresh} isLoading={isLoading} />
			<div className="relative overflow-hidden border-table-border">
				{/* This is the overlay that appears when the data is loading */}
				<AnimatePresence mode="wait">
					{isLoading && (
						<motion.div
							initial={{ opacity: 0 }}
							animate={{ opacity: 1 }}
							exit={{ opacity: 0 }}
							className="absolute inset-0 z-50 flex items-center justify-center bg-black/30 backdrop-blur-sm"
						>
							<motion.div
								initial={{ scale: 0.8 }}
								animate={{ scale: 1 }}
								exit={{ scale: 0.8 }}
								className="flex flex-col items-center gap-2"
							>
								<Loader2 className="h-8 w-8 animate-spin text-table-header-action-gradient-from" />
								<span className="text-sm font-medium text-table-text-secondary">
									Loading data...
								</span>
							</motion.div>
						</motion.div>
					)}
				</AnimatePresence>
				<div className="rounded-lg">
					<Table>
						<TableHeader>
							{/* This is the header row where the column headers are defined */}
							{table.getHeaderGroups().map((headerGroup) => (
								<TableRow
									key={headerGroup.id}
									className="bg-table-header border-b border-table-border"
								>
									{headerGroup.headers.map((header) => {
										return (
											<TableHead key={header.id} colSpan={header.colSpan}>
												{header.isPlaceholder ? null : (
													<div className="flex items-center gap-2">
														{flexRender(
															header.column.columnDef.header,
															header.getContext(),
														)}
													</div>
												)}
											</TableHead>
										);
									})}
								</TableRow>
							))}
						</TableHeader>
						<TableBody className="bg-table-row-DEFAULT divide-y divide-table-border">
							{isLoading ? (
								<DataTableLoading columns={columns} />
							) : table.getRowModel().rows?.length ? (
								table.getRowModel().rows.map((row) => (
									<TableRow
										key={row.id}
										onClick={() => onRowClick?.(row.original.id)}
										className="cursor-pointer hover:bg-[hsl(var(--table-row-hover))]"
									>
										{row.getVisibleCells().map((cell) => (
											<TableCell key={cell.id}>
												{flexRender(
													cell.column.columnDef.cell,
													cell.getContext(),
												)}
											</TableCell>
										))}
									</TableRow>
								))
							) : (
								<TableRow>
									<TableCell
										colSpan={columns.length}
										className="h-[52px] text-center text-table-text-secondary"
									>
										No results.
									</TableCell>
								</TableRow>
							)}
							{/* Intersection observer target for infinite loading */}
							<tr ref={ref} className="h-1" />
						</TableBody>
					</Table>
				</div>
			</div>
			<DataTablePagination table={table} />
		</div>
	);
} 