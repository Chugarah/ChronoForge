"use client";

import type { FC } from "react";

/**
 * ClientsAreLovingOurApp Component
 *
 * @component
 * @description Renders a section displaying client testimonials for the application.
 * The component includes a header section with introductory text and a collection
 * of testimonial cards rendered by the TestimonialCards component.
 *
 * This component is wrapped in the TestimonialsProvider context to handle testimonial data fetching
 * and state management. It uses the "use client" directive for client-side rendering.
 *
 * The component follows BEM naming conventions for CSS classes and includes proper ARIA labels
 * for accessibility.
 *
 * @example
 * ```jsx
 * <TestimonialsProvider>
 *   <ClientsAreLovingOurApp />
 * </TestimonialsProvider>
 * ```
 *
 * @returns {JSX.Element} A section containing client testimonials
 */
const ListProjects: FC = () => {
	return (
		<section
			aria-label="Clients Are Loving Our App"
			className="clients-app-wrapper"
		>
			<div className="clients-app">
				<div className="clients-app__client-intro">
					<p>Clients Are Loving Our App</p>
				</div>

				{/* Testimonial cards container */}
			Hello World, loading table data Projects
			</div>
		</section>
	);
}

export default ListProjects;
