

import { AnimatePresence, motion } from "framer-motion";
import { X } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader } from "@/components/ui/card";
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
								"fixed left-[50%] top-[50%] z-50 grid w-full max-w-[80vw] max-h-[80vh] translate-x-[-50%] translate-y-[-50%] gap-4 p-0 bg-[hsl(var(--background))] shadow-lg",
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
							<Card className="h-full border-0 shadow-none">
								<CardHeader className="sticky top-0 z-10 bg-[hsl(var(--background))] border-b border-[hsl(var(--border))] px-6 py-4">
									<div className="flex items-center justify-between">
										<div>
											<DialogTitle className="text-xl font-semibold text-[hsl(var(--foreground))]">
												{project.title}
											</DialogTitle>
											<DialogDescription className="text-sm text-[hsl(var(--muted-foreground))]">
												View and manage project details and settings
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
								</CardHeader>
								<CardContent className="flex-1 overflow-auto p-6">
									<motion.div
										initial={{ opacity: 0, y: 20 }}
										animate={{ opacity: 1, y: 0 }}
										transition={{ delay: 0.1 }}
										className="space-y-6"
									>
										{/* Project Details Content */}
										<div className="space-y-4">
											<motion.div
												initial={{ opacity: 0, x: -20 }}
												animate={{ opacity: 1, x: 0 }}
												transition={{ delay: 0.2 }}
											>
												<h3 className="text-sm font-medium text-[hsl(var(--muted-foreground))]">
													Timeline
												</h3>
												<p className="mt-1 text-sm text-[hsl(var(--foreground))]">
													{new Date(project.startDate).toLocaleDateString()} -{" "}
													{new Date(project.endDate).toLocaleDateString()}
												</p>
											</motion.div>

											<motion.div
												initial={{ opacity: 0, x: -20 }}
												animate={{ opacity: 1, x: 0 }}
												transition={{ delay: 0.3 }}
											>
												<h3 className="text-sm font-medium text-[hsl(var(--muted-foreground))]">
													Description
												</h3>
												<p className="mt-1 text-sm text-[hsl(var(--foreground))]">
													{project.description || "No description provided"}
												</p>
											</motion.div>

											<motion.div
												initial={{ opacity: 0, x: -20 }}
												animate={{ opacity: 1, x: 0 }}
												transition={{ delay: 0.4 }}
											>
												<h3 className="text-sm font-medium text-[hsl(var(--muted-foreground))]">
													Status
												</h3>
												<div className="mt-2">
													<span className="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-[hsl(var(--primary))] text-[hsl(var(--primary-foreground))]">
														Active
													</span>
												</div>
											</motion.div>
										</div>
									</motion.div>
								</CardContent>
							</Card>
						</DialogContent>
					</DialogPortal>
				</Dialog>
			)}
		</AnimatePresence>
	);
} 