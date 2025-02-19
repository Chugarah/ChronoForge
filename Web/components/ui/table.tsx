import * as React from "react";
import { cn } from "@/lib/utils";

const Table = React.forwardRef<
	HTMLTableElement,
	React.HTMLAttributes<HTMLTableElement>
>(({ className, ...props }, ref) => (
	<div className="w-full overflow-hidden rounded-lg">
		<table
			ref={ref}
			className={cn("datatable w-full", className)}
			{...props}
		/>
	</div>
));
Table.displayName = "Table";

const TableHeader = React.forwardRef<
	HTMLTableSectionElement,
	React.HTMLAttributes<HTMLTableSectionElement>
>(({ className, ...props }, ref) => (
	<thead 
		ref={ref} 
		className={cn(
			"sticky top-0 z-10 border-b border-[hsl(var(--table-border))] backdrop-blur-sm",
			"bg-[hsl(var(--table-header-bg))]",
			className
		)} 
		{...props} 
	/>
));
TableHeader.displayName = "TableHeader";

const TableBody = React.forwardRef<
	HTMLTableSectionElement,
	React.HTMLAttributes<HTMLTableSectionElement>
>(({ className, ...props }, ref) => (
	<tbody
		ref={ref}
		className={cn(
			"divide-y divide-[hsl(var(--table-border))]",
			"bg-[hsl(var(--table-row-default))]",
			className
		)}
		{...props}
	/>
));
TableBody.displayName = "TableBody";

const TableFooter = React.forwardRef<
	HTMLTableSectionElement,
	React.HTMLAttributes<HTMLTableSectionElement>
>(({ className, ...props }, ref) => (
	<tfoot
		ref={ref}
		className={cn(
			"w-full sticky bottom-0 z-10",
			"bg-[hsl(var(--table-header-bg))]",
			className
		)}
		{...props}
	/>
));
TableFooter.displayName = "TableFooter";

const TableRow = React.forwardRef<
	HTMLTableRowElement,
	React.HTMLAttributes<HTMLTableRowElement>
>(({ className, ...props }, ref) => (
	<tr
		ref={ref}
		className={cn(
			"w-full transition-colors duration-200",
			"hover:bg-[hsl(var(--table-row-hover))]",
			"data-[state=selected]:bg-[hsl(var(--table-row-selected))]",
			className
		)}
		{...props}
	/>
));
TableRow.displayName = "TableRow";

const TableHead = React.forwardRef<
	HTMLTableCellElement,
	React.ThHTMLAttributes<HTMLTableCellElement>
>(({ className, ...props }, ref) => (
	<th
		ref={ref}
		className={cn(
			"h-10 px-4 text-left align-middle",
			"text-xs font-medium uppercase tracking-wider",
			"text-[hsl(var(--table-header-muted))]",
			"transition-colors duration-200",
			"[&:has([role=checkbox])]:pr-0",
			className
		)}
		{...props}
	/>
));
TableHead.displayName = "TableHead";

const TableCell = React.forwardRef<
	HTMLTableCellElement,
	React.TdHTMLAttributes<HTMLTableCellElement>
>(({ className, ...props }, ref) => (
	<td
		ref={ref}
		className={cn(
			"h-[48px] px-4 align-middle",
			"text-sm text-[hsl(var(--table-text-primary))]",
			"transition-all duration-200 ease-in-out",
			"first:pl-4 last:pr-4",
			"[&:has([role=checkbox])]:pr-0",
			className
		)}
		{...props}
	/>
));
TableCell.displayName = "TableCell";

const TableCaption = React.forwardRef<
	HTMLTableCaptionElement,
	React.HTMLAttributes<HTMLTableCaptionElement>
>(({ className, ...props }, ref) => (
	<caption
		ref={ref}
		className={cn(
			"mt-4 text-sm text-[hsl(var(--table-text-secondary))]",
			className
		)}
		{...props}
	/>
));
TableCaption.displayName = "TableCaption";

export {
	Table,
	TableHeader,
	TableBody,
	TableFooter,
	TableHead,
	TableRow,
	TableCell,
	TableCaption,
};
