"use client";

import { motion } from "framer-motion";
import { LayoutDashboard, Settings, Users2 } from "lucide-react";
import Link from "next/link";
import * as React from "react";
import {
	NavigationMenu,
	NavigationMenuContent,
	NavigationMenuItem,
	NavigationMenuLink,
	NavigationMenuList,
	NavigationMenuTrigger,
} from "@/components/ui/navigation-menu";
import { cn } from "@/lib/utils";
import type { NavigationComponent, NavigationListItemProps } from "@/types/navigation.types";

const components: NavigationComponent[] = [
	{
		title: "Projects",
		href: "/project",
		description: "View our showcase of projects and achievements.",
		icon: LayoutDashboard,
	},
	{
		title: "About",
		href: "/about",
		description: "Learn more about our mission and team.",
		icon: Users2,
	},
	{
		title: "Contact",
		href: "/contact",
		description: "Get in touch with us for any inquiries.",
		icon: Settings,
	},
];

export function DesktopNav() {
	return (
		<NavigationMenu className="max-(--breakpoint-md):hidden">
			<NavigationMenuList>
				<NavigationMenuItem>
					<NavigationMenuTrigger>Getting Started</NavigationMenuTrigger>
					<NavigationMenuContent>
						<ul className="grid gap-2 p-4 md:w-[400px] lg:w-[500px] lg:grid-cols-[1.1fr_0.9fr]">
							<li className="row-span-3">
								<NavigationMenuLink asChild>
									<Link
										href="/"
										className="flex h-full w-full select-none flex-col justify-center rounded-md bg-gradient-to-b from-muted/50 to-muted p-6 no-underline outline-none focus:shadow-md"
									>
										<div className="text-xl font-medium tracking-tight">
											Mattin-Lasse Group AB
										</div>
										<p className="mt-2 text-sm leading-relaxed text-muted-foreground">
											Building better worlds :). We are group 
                      of explorers and builders that are passionate about creating 
                      a better future.
										</p>
									</Link>
								</NavigationMenuLink>
							</li>
							<li className="col-span-1">
								<div className="flex justify-center items-center h-full min-h-[200px]">
									<motion.svg
										viewBox="0 0 100 120"
										className="w-32 h-32 opacity-70"
										fill="none"
										xmlns="http://www.w3.org/2000/svg"
										aria-labelledby="protossPylonTitle"
										whileHover={{ scale: 1.05 }}
										animate={{ 
											opacity: [0.7, 0.9, 0.7],
										}}
										transition={{
											duration: 2,
											repeat: Number.POSITIVE_INFINITY,
											ease: "easeInOut"
										}}
									>
										<title id="protossPylonTitle">Protoss Pylon Energy Crystal</title>
										<defs>
											<filter id="glow" x="-50%" y="-50%" width="200%" height="200%">
												<feGaussianBlur in="SourceGraphic" stdDeviation="2" result="blur" />
												<feColorMatrix in="blur" mode="matrix" values="1 0 0 0 0  0 1 0 0 0  0 0 1 0 0  0 0 0 18 -7" result="glow" />
												<feBlend in="SourceGraphic" in2="glow" mode="overlay" />
											</filter>
										</defs>
										<motion.g 
											filter="url(#glow)"
											animate={{ 
												scale: [1, 1.02, 1],
												rotate: [0, 1, 0],
											}}
											transition={{
												duration: 3,
												repeat: Number.POSITIVE_INFINITY,
												ease: "easeInOut"
											}}
										>
											<motion.path
												d="M50 0L20 30v60l30 30 30-30V30L50 0z"
												className="fill-[hsl(var(--primary))]"
												animate={{ opacity: [0.2, 0.3, 0.2] }}
												transition={{
													duration: 2,
													repeat: Number.POSITIVE_INFINITY,
													ease: "easeInOut"
												}}
											/>
											<motion.path
												d="M50 10L25 35v50l25 25 25-25V35L50 10z"
												className="fill-[hsl(var(--primary))]"
												animate={{ opacity: [0.4, 0.5, 0.4] }}
												transition={{
													duration: 2,
													repeat: Number.POSITIVE_INFINITY,
													ease: "easeInOut",
													delay: 0.2
												}}
											/>
											<motion.path
												d="M50 20L30 40v40l20 20 20-20V40L50 20z"
												className="fill-[hsl(var(--primary))]"
												animate={{ opacity: [0.6, 0.7, 0.6] }}
												transition={{
													duration: 2,
													repeat: Number.POSITIVE_INFINITY,
													ease: "easeInOut",
													delay: 0.4
												}}
											/>
											<motion.g
												animate={{ 
													scale: [1, 1.1, 1],
												}}
												transition={{
													duration: 1.5,
													repeat: Number.POSITIVE_INFINITY,
													ease: "easeInOut"
												}}
											>
												<motion.circle
													cx="50"
													cy="60"
													r={15}
													className="fill-[hsl(var(--primary))]"
													animate={{ 
														opacity: [0.8, 1, 0.8],
														scale: [1, 1.1, 1]
													}}
													transition={{
														duration: 1.5,
														repeat: Number.POSITIVE_INFINITY,
														ease: "easeInOut"
													}}
												/>
												<motion.path
													d="M50 35v50M25 60h50"
													stroke="hsl(var(--primary))"
													strokeWidth="2"
													animate={{ 
														opacity: [0.6, 0.8, 0.6],
														strokeWidth: [2, 2.5, 2]
													}}
													transition={{
														duration: 1.5,
														repeat: Number.POSITIVE_INFINITY,
														ease: "easeInOut"
													}}
												/>
											</motion.g>
										</motion.g>
									</motion.svg>
								</div>
							</li>
						</ul>
					</NavigationMenuContent>
				</NavigationMenuItem>
				<NavigationMenuItem>
					<NavigationMenuTrigger>Components</NavigationMenuTrigger>
					<NavigationMenuContent>
						<ul className="grid w-[400px] gap-3 p-4 md:w-[500px] md:grid-cols-2 lg:w-[600px]">
							{components.map((component) => (
								<ListItem
									key={component.title}
									title={component.title}
									href={component.href}
									icon={component.icon}
								>
									{component.description}
								</ListItem>
							))}
						</ul>
					</NavigationMenuContent>
				</NavigationMenuItem>

			</NavigationMenuList>
		</NavigationMenu>
	);
}

const ListItem = React.forwardRef<HTMLAnchorElement, NavigationListItemProps>(
	({ className, title, children, href, icon: Icon, ...props }, ref) => {
		return (
			<li>
				<NavigationMenuLink asChild>
					<Link
						ref={ref}
						href={href}
						className={cn(
							"block select-none space-y-1 rounded-md p-3 leading-none no-underline outline-none transition-colors",
							"hover:bg-[hsl(var(--accent))] hover:text-[hsl(var(--accent-foreground))]",
							"focus:bg-[hsl(var(--accent))] focus:text-[hsl(var(--accent-foreground))]",
							"active:bg-[hsl(var(--accent))] active:text-[hsl(var(--accent-foreground))]",
							className
						)}
						{...props}
					>
						{Icon && <Icon className="mb-2 h-6 w-6" />}
						<div className="text-sm font-medium leading-none">{title}</div>
						<p className="line-clamp-2 text-sm leading-snug text-[hsl(var(--muted-foreground))]">
							{children}
						</p>
					</Link>
				</NavigationMenuLink>
			</li>
		);
	}
); 