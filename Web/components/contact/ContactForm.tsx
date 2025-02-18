"use client";
import { useActionState } from "react";
import { useFormStatus } from "react-dom";
import { submitContactForm } from "@/actions/contact";
import FormError from "@/components/forms/FormError";
import {
	contactFormSchema,
	type ContactFormSchema,
} from "@/lib/validation/schemas/contactForm";
import type { apiResponseProp } from "@/types/api/global";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import ButtonForm from "@/components/forms/ButtonForm";
import FormInput from "@/components/contact/FormInput";
import FormSelect from "@/components/contact/FormSelect";
import { cn } from "@/lib/utils";

const initialState = {
	message: null,
	error: null,
};

function SubmitButton() {
	const { pending } = useFormStatus();

	return (
		<ButtonForm
			type="submit"
			variant="default"
			className={cn(
				"contact-form__container__form-item__button",
				pending && "opacity-50 cursor-not-allowed"
			)}
			iconPosition="hidden"
			disabled={pending}
			data-loading-text="Please wait..."
		>
			{pending ? "Building pylons..." : "Build more pylons!"}
		</ButtonForm>
	);
}

/**
 * ContactForm component for handling online consultation requests.
 * Provides a form with validation, loading states, and API integration.
 *
 * @component
 * @returns {JSX.Element} A form component for contact submissions
 */
function ContactForm() {
	const [state, formAction] = useActionState(submitContactForm, initialState);
	const { register, setValue, formState: { errors } } = useForm<ContactFormSchema>({
		resolver: zodResolver(contactFormSchema),
	});

	const setApiMessage = (message: apiResponseProp<{ message: string }> | null) => {
		// This is a no-op since we're using server actions now
	};

	return (
		<form action={formAction} className="contact-form" noValidate>
			<div className="contact-form__container">
				<h2>Get Online Consultation</h2>
				
				<FormInput
					register={register}
					name="fullName"
					label="Full name"
					type="text"
					error={!!errors.fullName}
					setApiMessage={setApiMessage}
					isLoading={false}
				/>

				<FormInput
					register={register}
					name="email"
					label="Email address"
					type="email"
					error={!!errors.email}
					setApiMessage={setApiMessage}
					isLoading={false}
				/>

				<FormSelect
					name="specialist"
					label="Specialist"
					error={!!errors.specialist}
					setApiMessage={setApiMessage}
					setValue={setValue}
					isLoading={false}
				/>

				<div className="contact-form__container__form-item">
					<div className="flex flex-col">
						{state.error && (
							<FormError
								id="form-error"
								className="contact-form__error"
								message={state.error}
							/>
						)}
						{state.message && (
							<FormError
								id="form-success"
								className="contact-form__success"
								message={state.message}
							/>
						)}
					</div>
				</div>

				<div className="contact-form__container__form-item">
					<SubmitButton />
				</div>
			</div>
		</form>
	);
}

export default ContactForm;
