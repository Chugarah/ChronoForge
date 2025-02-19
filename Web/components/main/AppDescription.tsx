"use client";

import { motion } from "framer-motion";
import type { Metadata } from "next";
import type { FC } from "react";

export const metadata: Metadata = {
	title: "Next.js Enterprise Boilerplate",
	twitter: {
		card: "summary_large_image",
	},
	openGraph: {
		url: "https://next-enterprise.vercel.app/",
		images: [
			{
				width: 1200,
				height: 630,
				url: "https://raw.githubusercontent.com/Blazity/next-enterprise/main/.github/assets/project-logo.png",
			},
		],
	},
};

const AppDescription: FC = () => {
	return (
		<>
			{/* Business Section */}
			<section className="bg-theme">
				<div className="py-24 md:py-32 lg:py-5">
					<div className="items-center">
						{/* Icon Grid */}
						<div className="gridgap-1">
							<motion.div
								className="col-span-2 flex flex-col items-center justify-center rounded-lg py-1 md:py-16 lg:py-10 px-8 bg-theme-secondary"
								initial={{ opacity: 0, y: 20 }}
								animate={{ opacity: 1, y: 0 }}
								transition={{
									duration: 0.8,
									ease: [0.22, 1, 0.36, 1],
								}}
							>
								<h1 className="text-4xl md:text-5xl lg:text-6xl font-bold text-theme text-balance max-w-3xl text-center">
									Mattin-Lassei Group AB
                  Building Better{" "}
									<span className="text-theme-primary">Worlds.</span>
								</h1>
							</motion.div>
							<motion.div
								className="col-span-2 flex flex-col items-center justify-center rounded-lg py-8 md:py- px-6 bg-theme-secondary"
								initial={{ opacity: 0, y: 20 }}
								animate={{ opacity: 1, y: 0 }}
								transition={{
									delay: 0.2,
									duration: 0.8,
									ease: [0.22, 1, 0.36, 1],
								}}
							>
								<p className="text-lg md:text-xl text-theme-muted text-balance max-w-2xl text-center leading-relaxed">
									Empowering developers to build better worlds.
								</p>
							</motion.div>
						</div>
					</div>
				</div>
			</section>
		</>
	);
};

export default AppDescription;