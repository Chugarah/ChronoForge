"use client";

import { useQueryClient } from '@tanstack/react-query';
import { useState } from "react";
import { itemKeys, useProjectsAndStatus } from "@/lib/hooks/items";
import type { Project } from "@/types/api.types";
import { ProjectDetailsDialog } from "./ProjectDetailsDialog";
import { ProjectTable } from "./ProjectTable";

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
		strategy: 'parallel' // Use parallel for better performance in list view
	});

	// Ensure we properly map projects with their statuses
	const projectsData = projects.data?.pages.flatMap(page => 
		page.items.map(item => ({
			...item,
			// Status is already attached by useProjectsAndStatus
			status: item.status || null
		}))
	) ?? [];

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

	if (isError) {
		return (
			<div className="flex flex-col items-center justify-center p-4">
				<p className="text-red-500">Error loading projects</p>
				<button 
					onClick={handleRefresh}
					type="button"
					className="mt-2 px-4 py-2 bg-primary text-white rounded hover:bg-primary/90"
				>
					Retry
				</button>
			</div>
		);
	}

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
