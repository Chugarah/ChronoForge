"use client";
import FaqError from "@/components/common/Faq/FaqError";
import FaqAccordionSkeleton from "@/components/skeletons/FaqAccordionSkeleton";
import { Button } from "@/components/ui/button";
import { cn } from "@/lib/utils";
import { faChevronUp } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { motion, AnimatePresence } from "framer-motion";
import { useEffect, useState } from "react";
import type { FaqItem } from "@/types/faq";

const useResponsive = () => {
	const [isMobile, setIsMobile] = useState(false);

	useEffect(() => {
		const checkMobile = () => {
			setIsMobile(window.matchMedia("(max-width: 768px)").matches);
		};

		checkMobile();
		window.addEventListener("resize", checkMobile);
		return () => window.removeEventListener("resize", checkMobile);
	}, []);

	return { isMobile };
};

async function getFaqs() {
	const res = await fetch('/api/faqs');
	if (!res.ok) throw new Error('Failed to fetch FAQs');
	return res.json() as Promise<FaqItem[]>;
}

function FaqAccordion() {
	const [faq, setFaq] = useState<FaqItem[]>([]);
	const [isLoading, setIsLoading] = useState(true);
	const [error, setError] = useState<Error | null>(null);
	const [activeId, setActiveId] = useState<string | null>(null);
	const { isMobile } = useResponsive();

	useEffect(() => {
		getFaqs()
			.then(data => {
				setFaq(data);
				if (data.length > 0) {
					setActiveId(data[0].id);
				}
			})
			.catch(err => setError(err))
			.finally(() => setIsLoading(false));
	}, []);

	const toggleAccordion = (id: string) => {
		setActiveId((currentId) => (currentId === id ? null : id));
	};

	const variants = {
		mobile: {
			initial: { height: 0 },
			animate: {
				height: "auto",
				transition: { duration: 0.15 },
			},
			exit: {
				height: 0,
				transition: { duration: 0.15 },
			},
		},
		desktop: {
			initial: { height: 0 },
			animate: {
				height: "auto",
				transition: { duration: 0.2 },
			},
			exit: {
				height: 0,
				transition: { duration: 0.2 },
			},
		},
	};

	if (isLoading) {
		return <FaqAccordionSkeleton />;
	}

	if (error) {
		return <FaqError />;
	}

	return (
		<div className="faq__faq-accordion">
			{faq.map((faqData, index) => (
				<div
					key={faqData.id}
					className={cn({
						"faq__faq-accordion__container": true,
						"faq__faq-accordion__container--border-radius-top": index === 0,
						"faq__faq-accordion__container--border-radius-bottom":
							index === faq.length - 1,
						"faq__faq-accordion__container--active": activeId === faqData.id,
					})}
				>
					<div
						className="faq__faq-accordion__container__title"
						onClick={() => toggleAccordion(faqData.id)}
						onKeyDown={(e) => {
							if (e.key === "Enter" || e.key === " ") {
								e.preventDefault();
								toggleAccordion(faqData.id);
							}
						}}
					>
						<p>{faqData.title}</p>
						<Button
							variant="outline"
							size="sm"
							className={cn("faq__faq-accordion__container__title__button", {
								"faq__faq-accordion__container__title__button--active":
									activeId === faqData.id,
							})}
						>
							<FontAwesomeIcon
								icon={faChevronUp}
								className={cn("faq__faq-accordion__container__title__icon", {
									"faq__faq-accordion__container__title__icon--active":
										activeId === faqData.id,
								})}
								size="sm"
								style={{
									transform:
										activeId === faqData.id ? "rotate(0deg)" : "rotate(180deg)",
									transition: "transform 0.2s ease",
								}}
							/>
						</Button>
					</div>
					<AnimatePresence mode="wait">
						{activeId === faqData.id && (
							<motion.div
								className={cn("faq__faq-accordion__container__content", {
									"faq__faq-accordion__container__content--mobile": isMobile,
									"faq__faq-accordion__container__content--desktop": !isMobile,
								})}
								variants={isMobile ? variants.mobile : variants.desktop}
								initial="initial"
								animate="animate"
								exit="exit"
								viewport={{
									once: true,
									amount: 0.25,
								}}
								style={{ overflow: "hidden" }}
							>
								<div
									className={cn("faq__faq-accordion__container", {
										"faq__faq-accordion__container__content--mobile": isMobile,
										"faq__faq-accordion__container__content--desktop":
											!isMobile,
									})}
								>
									<p>{faqData.content}</p>
								</div>
							</motion.div>
						)}
					</AnimatePresence>
				</div>
			))}
		</div>
	);
}

export default FaqAccordion;
