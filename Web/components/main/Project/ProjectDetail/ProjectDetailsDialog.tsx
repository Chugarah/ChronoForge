import { AnimatePresence } from "framer-motion";
import { X } from "lucide-react";
import { Button } from "@/components/ui/button";
import {
	Dialog,
	DialogContent,
	DialogDescription,
	DialogOverlay,
	DialogPortal,
	DialogTitle,
} from "@/components/ui/dialog";
import { cn } from "@/lib/utils";
import type { Project } from "@/types/api.types";
import { ProjectDetailsContent } from "./ProjectDetailsContent";

interface ProjectDetailsDialogProps {
	project: Project;
	open: boolean;
	onOpenChange: (open: boolean) => void;
}

export function ProjectDetailsDialog({
	project,
	open,
	onOpenChange,
}: ProjectDetailsDialogProps) {
	return (
		<AnimatePresence>
			{open && (
				<Dialog open={open} onOpenChange={onOpenChange}>
					<DialogPortal forceMount>
						<DialogOverlay
							className={cn(
								"fixed inset-0 z-50",
								"bg-background/80 backdrop-blur-sm",
								"data-[state=open]:animate-in data-[state=closed]:animate-out",
								"data-[state=closed]:fade-out-0 data-[state=open]:fade-in-0",
								"duration-300",
							)}
						/>
						<DialogContent
							className={cn(
								"fixed left-[50%] top-[50%] z-50 grid w-full max-w-[80vw] max-h-[80vh] translate-x-[-50%] translate-y-[-50%] gap-0 p-0 bg-[hsl(var(--background))] shadow-lg",
								"duration-200",
								"data-[state=open]:animate-in data-[state=closed]:animate-out",
								"data-[state=closed]:fade-out-0 data-[state=open]:fade-in-0",
								"data-[state=closed]:zoom-out-95 data-[state=open]:zoom-in-95",
								"data-[state=closed]:slide-out-to-left-1/2 data-[state=closed]:slide-out-to-top-[48%]",
								"data-[state=open]:slide-in-from-left-1/2 data-[state=open]:slide-in-from-top-[48%]",
								"rounded-lg border border-[hsl(var(--border))]",
							)}
							style={{
								width: "min(80vw, 1200px)",
								height: "min(80vh, 800px)",
							}}
						>
							<div className="flex h-full flex-col overflow-hidden">
								<div className="flex-none border-b border-[hsl(var(--border))] px-6 py-4">
									<div className="flex items-center justify-between">
										<div>
											<DialogTitle className="text-xl font-semibold text-[hsl(var(--foreground))]">
												{project.title}
											</DialogTitle>
											<DialogDescription className="text-sm text-[hsl(var(--muted-foreground))]">
												You can view project details here.
											</DialogDescription>
										</div>
										<Button
											variant="ghost"
											size="icon"
											className="h-8 w-8 hover:bg-[hsl(var(--muted))]"
											onClick={() => onOpenChange(false)}
											aria-label="Close dialog"
										>
											<X className="h-4 w-4" />
										</Button>
									</div>
								</div>
								<div className="flex-1 overflow-hidden">
									<ProjectDetailsContent project={project} />
								</div>
							</div>
						</DialogContent>
					</DialogPortal>
				</Dialog>
			)}
		</AnimatePresence>
	);
}
