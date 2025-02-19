import type { ColumnDef } from "@tanstack/react-table";
import { format } from "date-fns";
import { motion } from "framer-motion";
import type { Project } from "@/types/api.types";

// Helper function to determine status color
const getStatusColor = (text: string): string => {
	// Simple hash function that works with strings
	let hash = 0;
	for (let i = 0; i < text.length; i++) {
		hash = ((hash << 5) - hash) + text.charCodeAt(i);
		hash = hash & hash; // Convert to 32-bit integer
	}

	// Use the hash to select from a predefined set of tailwind-compatible colors
	const colors = [
		"blue",    // Default blue
		"green",   // Success green
		"yellow",  // Warning yellow
		"red",     // Error red
		"purple",  // Purple
		"pink",    // Pink
		"indigo",  // Indigo
		"orange",  // Orange
		"teal",    // Teal
		"cyan"     // Cyan
	] as const;

	// Ensure positive index
	const index = Math.abs(hash) % colors.length;
	return colors[index] as string;
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
			const status = row.original.status;
			const statusText = status ? status.name : "No Status";
			return (
				<motion.div 
					className="status-indicator"
					whileHover={{ scale: 1.01 }}
					transition={{ duration: 0.2 }}
				>
					<span className={`status-dot ${getStatusColor(statusText)}`} />
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
			const statusColor = getStatusColor(statusText);
			return (
				<div className="flex items-center gap-2">
					<span className={`status-dot ${statusColor}`} />
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
					{format(new Date(row.getValue("startDate")), "MMM dd")}
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
					{format(new Date(row.getValue("endDate")), "MMM dd")}
				</div>
			);
		},
	},
]; 