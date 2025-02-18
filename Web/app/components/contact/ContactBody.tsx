"use client";

import AdressCard from "@/components/contact/AdressCard";
import dynamic from "next/dynamic";
import { Suspense } from "react";

const MapIframe = dynamic(() => import("./MapIframe"), {
	loading: () => <div className="h-[540px] w-full animate-pulse bg-gray-200 rounded-lg" />,
	ssr: false
});

function ContactBody() {
	return (
		<div className="py-[10em] piff-puff">
			<div className="contact-page-wrapper--white ">
				<div className="contact-page-wrapper__container">
					{/* Header Contact Area */}
					<div className="contact-page-wrapper__container__contact-header">
						<div className="contact-page-wrapper__container__contact-header__contact-info--body">
							<Suspense fallback={<div className="h-[540px] w-full animate-pulse bg-gray-200 rounded-lg" />}>
								<MapIframe />
							</Suspense>
						</div>
						<div className="contact-page-wrapper__container__contact-header__contact-form--body">
							<AdressCard />
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}

export default ContactBody;
