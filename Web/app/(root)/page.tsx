import type { FC } from 'react';
import ListProjects from "@/components/main/Project/ListProjects";
import AppDescription from "components/main/AppDescription";

/**
 * HomePage component that serves as the main landing page of the application.
 * This component assembles and renders all the major sections of the homepage in a specific order.
 *
 * @remarks
 * The component renders the following sections in order:
 * - AppDescription: Provides detailed description of the app
 * - ListProjects: Shows testimonials section
 *
 * Each section is implemented as a separate component for better maintainability
 * and code organization. The sections are wrapped in a React Fragment to avoid
 * unnecessary DOM nodes.
 *
 * @example
 * ```tsx
 * // In _app.tsx or similar
 * <RootLayout>
 *   <HomePage />
 * </RootLayout>
 * ```
 */
const HomePage: FC = () => {
	return (
		<>
			<AppDescription />
			<ListProjects />
		</>
	);
}

export default HomePage;
