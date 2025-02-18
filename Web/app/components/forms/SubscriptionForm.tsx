"use client";

// UI Components https://ui.shadcn.com/docs/components/form
// Form Engine https://react-hook-form.com/ts
// Form Validation https://github.com/fabien0102/ts-to-zod
// Used AI Phind to help me with documentation and some of the tricky part of implementing
// validation and form submission, the 100% AI Generated part is
// Also god inspired and copied some of the code from Hans
// 1. https://www.youtube.com/watch?v=CZ89e00h8dY&t=26s (Modern way)
// 2. https://www.youtube.com/watch?v=qkDkUI4sgmI&t=6s (Basic and important)
// 3. https://www.youtube.com/watch?v=E8lOEg6MmtY (Posting Data to API Server by Hans)
/**
function SubscriptionForm() {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<SubscriptionSchema>({
    resolver: zodResolver(subscriptionSchema),
  });
 */
import { zodResolver } from "@hookform/resolvers/zod";
import { faEnvelope } from "@fortawesome/free-regular-svg-icons";
import { IconFA } from "@common/IconFA";
import { Input } from "@ui/input";
import ButtonForm from "@forms/ButtonForm";
import {
	subscriptionSchema,
	type SubscriptionSchema,
} from "@lib/validation/schemas/subscription";
import { useForm } from "react-hook-form";
import FormError from "@forms/FormError";
import { useState, useEffect } from "react";
import * as React from "react";
import { cn } from "@lib/utils";
import type { apiResponseProp } from "@api-types/global";

function SubscriptionForm() {
	// We need to add Loading Indicator when we are Submitting our form
	const [isLoading, setIsLoading] = useState(false);

	// This was painful to solve, We used typescript here and AI helped me a lot here. We a
	// are using our global.d.ts API Response type where we have generic parameter as input,
	// we are extracting the message and we set it that it can be null so we don't linting errors
	// of possible null
	const [apiMessage, setApiMessage] = useState<apiResponseProp<{ message: string }> | null>(null);

	const {
		register,
		handleSubmit,
		watch,
		formState: { errors },
	} = useForm<SubscriptionSchema>({
		resolver: zodResolver(subscriptionSchema),
	});

	// We got an small bug where if we change our mail after we made an api response that error out
	// we get normal error message overlapping our api response, to fix that we created an
	// useEffect to watch the whole form and if any of form field is changed.
	useEffect(() => {
		const subscription = watch(() => {
			// Reset API message whenever any field changes
			setApiMessage(null);
		});
		return () => subscription.unsubscribe();
	}, [watch]);

	// This is where we handle the submit if validation is completed

	const onSubmit = async (data: SubscriptionSchema) => {
		setIsLoading(true);
		setApiMessage(null);

		try {
			// Simulate loading for better UX
			await new Promise(resolve => setTimeout(resolve, 1000));

			const response = await fetch(
				"https://win24-assignment.azurewebsites.net/api/forms/subscribe",
				{
					method: "POST",
					headers: {
						"Content-Type": "application/json",
					},
					body: JSON.stringify({ email: data.email }),
				},
			);

			if (!response.ok) {
				throw new Error("Failed to subscribe");
			}

			const result = await response.json();
			setApiMessage({
				error: null,
				message: result.message || "Successfully subscribed!",
				isLoading: false,
				data: { message: result.message }
			});
		} catch (err) {
			setApiMessage({
				error: err instanceof Error ? err : new Error("Failed to subscribe"),
				message: "Failed to subscribe to newsletter",
				isLoading: false,
				data: null
			});
		} finally {
			setIsLoading(false);
		}
	};

	return (
		<>
			<form onSubmit={handleSubmit(onSubmit)} className="newsletter" noValidate>
				<div className="newsletter__field">
					<IconFA
						icon={faEnvelope}
						classNames="newsletter__field-icon"
						title="Email Icon"
					/>
					{/* This is the magic happens for Zod :) validation*/}
					<Input
						{...register("email", {
							onChange: () => setApiMessage(null),
						})}
						type="email"
						placeholder="Your Email"
						className={cn(
							"newsletter__field-input",
							errors.email && "border-red-500",
						)}
						disabled={isLoading}
					/>
					{/* Display Error message */}
					{errors.email && (
						<FormError
							id="email-error"
							className={`newsletter__${errors.email ? "error" : ""}`}
							message={errors.email.message}
						/>
					)}
					{/* Display API message */}
					{/* We are using coalescent if statement and attacking css suffix */}
					{apiMessage && (
						<FormError
							id="api-response"
							className={`newsletter__${apiMessage.error ? "error" : "success"}`}
							message={apiMessage.message}
						/>
					)}
				</div>

				{/* We took inspiration and documentation from ChadCN UO
        https://ui.shadcn.com/docs/components/button*/}
				<ButtonForm
					type="submit"
					variant={isLoading ? "secondary" : "default"}
					className="newsletter__submit"
					iconPosition="hidden"
					disabled={isLoading}
					data-loading-text="Please wait..."
				>
					Subscribe
				</ButtonForm>
			</form>
		</>
	);
}

export default SubscriptionForm;
