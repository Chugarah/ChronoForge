import { format } from "date-fns";
import { motion } from "framer-motion";
import { Calendar, Info, User2 } from "lucide-react";
import { ScrollArea } from "@/components/ui/scroll-area";
import {
	Table,
	TableBody,
	TableCaption,
	TableCell,
	TableFooter,
	TableHead,
	TableHeader,
	TableRow,
} from "@/components/ui/table";
import type { Project } from "@/types/api.types";

interface ProjectDetailsContentProps {
	project: Project;
}

const invoices = [
	{
		invoice: "INV001",
		paymentStatus: "Paid",
		totalAmount: "$250.00",
	},
	{
		invoice: "INV002",
		paymentStatus: "Pending",
		totalAmount: "$150.00",
	},
	{
		invoice: "INV003",
		paymentStatus: "Unpaid",
		totalAmount: "$350.00",
	},
	{
		invoice: "INV004",
		paymentStatus: "Paid",
		totalAmount: "$450.00",
	},
	{
		invoice: "INV005",
		paymentStatus: "Paid",
		totalAmount: "$550.00",
	},
	{
		invoice: "INV006",
		paymentStatus: "Pending",
		totalAmount: "$200.00",
	},
	{
		invoice: "INV007",
		paymentStatus: "Unpaid",
		totalAmount: "$300.00",
	},
];

export function ProjectDetailsContent({ project }: ProjectDetailsContentProps) {
	return (
		<ScrollArea className="h-full">
			<div className="flex flex-col gap-8 p-6">
				{/* Metadata Grid */}
				<motion.div
					initial={{ opacity: 0, y: 10 }}
					animate={{ opacity: 1, y: 0 }}
					transition={{ delay: 0.3 }}
					className="grid grid-cols-2 gap-6"
				>
					{/* Timeline Section */}
					<div className="space-y-2">
						<div className="flex items-center gap-2">
							<Calendar className="h-8 w-8 text-[hsl(var(--primary))]" />
							<span className="text-base font-semibold text-[hsl(var(--primary))]">
								Timeline
							</span>
						</div>
						<p className="text-sm text-[hsl(var(--muted-foreground))]">
							Start Date:{" "}
							<span className="text-[hsl(var(--foreground))]">
								{format(new Date(project.startDate), "yyyy-MM-dd")}
							</span>
						</p>
						<p className="text-sm text-[hsl(var(--muted-foreground))]">
							End Date:{" "}
							<span className="text-[hsl(var(--foreground))]">
								{format(new Date(project.endDate), "yyyy-MM-dd")}
							</span>
						</p>
					</div>
					{/* User Details Section */}
					<div className="space-y-2">
						<div className="flex items-center gap-2">
							<User2 className="h-8 w-8 text-[hsl(var(--primary))]" />
							<span className="text-base font-semibold text-[hsl(var(--primary))]">
								Project Manager
							</span>
						</div>
						<p className="text-sm text-[hsl(var(--muted-foreground))]">
							First Name:{" "}
							<span className="text-[hsl(var(--foreground))]">
								{project.projectManagerData?.firstName ?? "Not assigned"}
							</span>
						</p>
						<p className="text-sm text-[hsl(var(--muted-foreground))]">
							Last Name:{" "}
							<span className="text-[hsl(var(--foreground))]">
								{project.projectManagerData?.lastName ?? "Not assigned"}
							</span>
						</p>
					</div>
				</motion.div>

				{/* Description Section */}
				<motion.div
					initial={{ opacity: 0, y: 10 }}
					animate={{ opacity: 1, y: 0 }}
					transition={{ delay: 0.4 }}
					className="space-y-3"
				>
					<div className="space-y-2">
						<div className="flex items-center gap-2">
							<Info className="h-8 w-8 text-[hsl(var(--primary))]" />
							<span className="text-base font-semibold text-[hsl(var(--primary))]">
								Description
							</span>
						</div>
						<div className="rounded-lg border border-[hsl(var(--border))] bg-[hsl(var(--muted)/.05)] p-4">
							<p className="text-sm text-[hsl(var(--foreground))]">
								{project.description || "No description provided"}
							</p>
						</div>
					</div>
				</motion.div>
				{/* Invoices Table */}
				<motion.div
					initial={{ opacity: 0, y: 10 }}
					animate={{ opacity: 1, y: 0 }}
					transition={{ delay: 0.6 }}
					className="space-y-3 flex gap-4"
				>
					<div className="space-y-2 w-full">
						<div className="flex items-center gap-2">
							<Info className="h-8 w-8 text-[hsl(var(--primary))]" />
							<span className="text-base font-semibold text-[hsl(var(--primary))]">
								Services
							</span>
						</div>
						<Table>
							<TableCaption>Project Services.</TableCaption>
							<TableHeader>
								<TableRow>
									<TableHead className="w-[100px]">Invoice</TableHead>
									<TableHead>Status</TableHead>
									<TableHead className="text-right">Amount</TableHead>
								</TableRow>
							</TableHeader>
							<TableBody>
								{invoices.map((invoice) => (
									<TableRow key={invoice.invoice}>
										<TableCell className="font-medium">
											{invoice.invoice}
										</TableCell>
										<TableCell>{invoice.paymentStatus}</TableCell>
										<TableCell>{invoice.totalAmount}</TableCell>
									</TableRow>
								))}
							</TableBody>
							<TableFooter>
								<TableRow>
									<TableCell colSpan={2}>Total</TableCell>
									<TableCell className="text-right">$2,500.00</TableCell>
								</TableRow>
							</TableFooter>
						</Table>
					</div>
					<div className="space-y-2 w-full">
						<div className="flex items-center gap-2">
							<Info className="h-8 w-8 text-[hsl(var(--primary))]" />
							<span className="text-base font-semibold text-[hsl(var(--primary))]">
								Customers
							</span>
						</div>
						<Table>
							<TableCaption>Project Customers.</TableCaption>
							<TableHeader>
								<TableRow>
									<TableHead className="w-[100px]">Invoice</TableHead>
									<TableHead>Status</TableHead>
									<TableHead className="text-right">Amount</TableHead>
								</TableRow>
							</TableHeader>
							<TableBody>
								{invoices.map((invoice) => (
									<TableRow key={invoice.invoice}>
										<TableCell className="font-medium">
											{invoice.invoice}
										</TableCell>
										<TableCell>{invoice.paymentStatus}</TableCell>
										<TableCell>{invoice.totalAmount}</TableCell>
									</TableRow>
								))}
							</TableBody>
							<TableFooter>
								<TableRow>
									<TableCell colSpan={2}>Total</TableCell>
									<TableCell className="text-right">$2,500.00</TableCell>
								</TableRow>
							</TableFooter>
						</Table>
					</div>
				</motion.div>
			</div>
		</ScrollArea>
	);
}
