import type { ColumnDef } from "@tanstack/react-table";
import { format } from "date-fns";
import { motion } from "framer-motion";
import type { Project } from "@/types/api.types";


// Helper function to get color class from text
const getStatusColorClass = (text: string): string => {
	const hash = Array.from(text).reduce((acc, char) => acc + char.charCodeAt(0), 0);
	const colors = ['blue', 'green', 'yellow', 'red', 'purple', 'orange'] as const;
	return colors[Math.abs(hash) % colors.length] as string;
};


export const projectColumns: ColumnDef<Project>[] = [
	{
		accessorKey: "title",
		header: () => (
			<div className="datatable-column-header">
				<span className="text-[hsl(var(--table-header-muted))]">Title</span>
			</div>
		),
		cell: ({ row }) => {
			const title = row.getValue("title") as string;
			return (
				<motion.div 
					className="status-indicator"
					whileHover={{ scale: 1.01 }}
					transition={{ duration: 0.2 }}
				>
					<span className="font-medium">{title}</span>
				</motion.div>
			);
		},
	},
	{
		accessorKey: "status",
		header: () => (
			<div className="datatable-column-header">
				<span className="text-[hsl(var(--table-header-muted))]">Status</span>
			</div>
		),
		cell: ({ row }) => {
			const status = row.original.status;
			const statusText = status ? status.name : "No Status";
			const colorClass = getStatusColorClass(statusText);
			return (
				<div className="flex items-center gap-2">
					<span className={`status-dot ${colorClass}`} />
					<span className="text-[hsl(var(--table-text-secondary))]">
						{statusText}
					</span>
				</div>
			);
		},
	},
	{
		accessorKey: "startDate",
		header: () => (
			<div className="datatable-column-header">
				<span className="text-[hsl(var(--table-header-muted))]">Start Date</span>
			</div>
		),
		cell: ({ row }) => {
			return (
				<div className="text-[hsl(var(--table-text-secondary))]">
					{format(new Date(row.getValue("startDate")), "yyyy-MM-dd")}
				</div>
			);
		},
	},
	{
		accessorKey: "endDate",
		header: () => (
			<div className="datatable-column-header">
				<span className="text-[hsl(var(--table-header-muted))]">End Date</span>
			</div>
		),
		cell: ({ row }) => {
			return (
				<div className="text-[hsl(var(--table-text-secondary))]">
					{format(new Date(row.getValue("endDate")), "yyyy-MM-dd")}
				</div>
			);
		},
	},
]; 