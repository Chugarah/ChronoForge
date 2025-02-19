import { DataTable } from "@/components/ui/data-table/DataTable";
import type { Project } from "@/types/api.types";
import { projectColumns } from "./ProjectColumns";

interface ProjectTableProps {
	data: Project[];
	isLoading: boolean;
	isError: boolean;
	pageCount: number;
	onPaginationChange: () => void;
	onRowClick: (id: number) => void;
	onRefresh: () => Promise<void>;
}

export const ProjectTable = ({
	data,
	isLoading,
	isError,
	pageCount,
	onPaginationChange,
	onRowClick,
	onRefresh
}: ProjectTableProps) => {
	return (
		<div className="border border-[hsl(var(--table-border))] bg-[hsl(var(--table-row-default))] rounded-lg overflow-hidden">
			<DataTable
				columns={projectColumns}
				data={data}
				isLoading={isLoading}
				isError={isError}
				pageCount={pageCount}
				onPaginationChange={onPaginationChange}
				onRowClick={onRowClick}
				onRefresh={onRefresh}
			/>
		</div>
	);
}; 