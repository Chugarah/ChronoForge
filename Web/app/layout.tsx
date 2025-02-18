import type { Metadata } from "next";
import { ThemeProvider } from "@contexts/theme/ThemeProvider";
import type React from "react";

/**
 * Metadata configuration for the application
 * @type {Metadata}
 * @property {string} title - The title of the application shown in browser tabs and search results
 * @property {string} description - A brief description of the application for SEO purposes
 */
export const metadata: Metadata = {
	title: "Mattin-Lassei Group AB AB 1.0.0",
	description: "We manage your projects",
};

/**
 * Root layout component that wraps the entire application
 * This component provides the base HTML structure and theme context for all pages
 *
 * @component
 * @param {Object} props - Component properties
 * @param {React.ReactNode} props.children - Child components to be rendered within the layout
 * @returns {JSX.Element} The root HTML structure with theme provider
 *
 * @remarks
 * The layout includes:
 * - Basic HTML structure with lang attribute
 * - Theme management through ThemeProvider
 * - Skeleton wrapper for layout structure
 *
 * @example
 * ```tsx
 * <Layout>
 *   <HomePage />
 * </Layout>
 * ```
 */
export default function Layout({ children }: { children: React.ReactNode }) {
	return (
		<html lang="en">
			<body>
				<div className="skeleton-wrapper">
					<ThemeProvider>{children}</ThemeProvider>
				</div>
			</body>
		</html>
	);
}
