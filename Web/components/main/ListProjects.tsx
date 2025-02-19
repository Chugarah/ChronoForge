"use client";

import { useQueryClient } from '@tanstack/react-query';
import { useState } from "react";
import { useProjectsAndStatus } from "@/lib/hooks/items/queries/combined-queries";
import { itemKeys } from "@/lib/hooks/items/queries/keys";
import type { Project } from "@/types/api.types";
import { ProjectDetailsDialog } from "./Project/ProjectDetail/ProjectDetailsDialog";
import { ProjectTable } from "./Project/ProjectTable";

const ListProjects = () => {
	const queryClient = useQueryClient();
	const [selectedProject, setSelectedProject] = useState<Project | null>(null);
	
	const { 
		projects,
		isLoading, 
		isError, 
		fetchNextPage, 
		hasNextPage,
		isFetchingNextPage
	} = useProjectsAndStatus(1, 10, {
		strategy: 'parallel' // Explicitly set parallel strategy for main list
	});

	const projectsData = (projects.data?.pages.flatMap(page => 
		page.items.map(item => ({
			...item,
			status: item.status || null
		}))
	) ?? []) as Project[];
	const totalPages = projects.data?.pages[0]?.totalPages ?? 0;

	const handlePaginationChange = () => {
		if (hasNextPage && !isFetchingNextPage) {
			fetchNextPage();
		}
	};

	const handleRefresh = async (): Promise<void> => {
		try {
			// Reset both projects and any status queries
			await queryClient.resetQueries({ 
				queryKey: itemKeys.projects(1, 10)
			});
			// Also invalidate any status queries
			await queryClient.invalidateQueries({
				queryKey: ['status']
			});
		} catch (error) {
			console.error('Error refreshing data:', error);
		}
	};

	const handleRowClick = (id: number) => {
		const project = projectsData.find(p => p.id === id);
		if (project) {
			setSelectedProject(project);
		}
	};

	return (
		<div className="flex flex-col flex-1">
			{/* Main Content */}
			<main className="flex-1 w-full">
				<div className="flex flex-col gap-6">
					<ProjectTable
						data={projectsData}
						isLoading={isLoading}
						isError={isError}
						pageCount={totalPages}
						onPaginationChange={handlePaginationChange}
						onRowClick={handleRowClick}
						onRefresh={handleRefresh}
					/>
				</div>
			</main>

			{/* Project Details Dialog */}
			{selectedProject && (
				<ProjectDetailsDialog
					project={selectedProject}
					open={!!selectedProject}
					onOpenChange={(open) => !open && setSelectedProject(null)}
				/>
			)}
		</div>
	);
};

export default ListProjects; 