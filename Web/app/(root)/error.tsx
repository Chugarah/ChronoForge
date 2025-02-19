"use client";

import { useEffect } from "react";
import { Button } from "@/components/ui/button";

/**
 * Error Boundary Component for handling and displaying runtime errors in Next.js applications
 *
 * @description This component serves as a fallback UI when runtime errors occur in the application.
 * It provides error logging functionality and a retry mechanism to recover from errors.
 * The component is automatically used by Next.js when an error occurs in the component tree.
 *
 * Features:
 * - Automatic error logging (configurable for production environments)
 * - User-friendly error message display
 * - Retry mechanism through a reset function
 * - Fully styled with Tailwind CSS
 * - Accessible button for error recovery
 *
 * @component
 * @param {Object} props - Component properties
 * @param {Error & { digest?: string }} props.error - The error object containing:
 *   - message: Error message describing what went wrong
 *   - stack: Stack trace of the error
 *   - digest: Optional unique error identifier (Next.js specific)
 * @param {() => void} props.reset - Function to reset the error boundary and retry rendering
 *   When called, it will attempt to re-render the failed component tree
 *
 * @example
 * // The component is automatically used by Next.js when an error occurs
 * <ErrorBoundary
 *   error={new Error("Something went wrong")}
 *   reset={() => {
 *     // Reset logic here
 *     // This could include clearing cache, resetting state, etc.
 *   }}
 * />
 *
 * @returns {JSX.Element} A centered error UI containing an error message and retry button
 *
 * @throws {Error} This component handles errors thrown by child components
 *
 * @see {@link https://nextjs.org/docs/advanced-features/error-handling} Next.js Error Handling
 */
export default function ErrorBoundary({
	error,
	reset,
}: {
	error: Error & { digest?: string };
	reset: () => void;
}) {
	useEffect(() => {
		/**
		 * Logs the error to console or error reporting service
		 *
		 * @description This effect runs when the component mounts or when the error changes.
		 * In production, this should be replaced with a proper error reporting service
		 * like Sentry, LogRocket, or custom error tracking solution.
		 *
		 * @param {Error} error - The error object to be logged
		 */
		console.error(error);
	}, [error]);

	return (
		<main className="flex h-full flex-col items-center justify-center">
			<div className="space-y-4 text-center">
				<h2 className="text-xl font-semibold text-[hsl(var(--foreground))]">
					Something went wrong!
				</h2>
				<p className="text-[hsl(var(--muted-foreground))]">
					{error.message || "An unexpected error occurred"}
				</p>
				<Button
					onClick={() => reset()}
					className="bg-[hsl(var(--primary))] text-[hsl(var(--primary-foreground))] hover:bg-[hsl(var(--primary)_/_0.9)]"
				>
					Try again
				</Button>
			</div>
		</main>
	);
}
