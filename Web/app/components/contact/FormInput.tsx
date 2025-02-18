import { memo } from "react";
import { Input } from "@/components/ui/input";
import type { FormInputProps } from "@/types/forms/contact";

/**
 * A memoized form input component that integrates with React Hook Form
 *
 * This component provides a reusable form input with label and error handling.
 * It's optimized for performance using React.memo to prevent unnecessary re-renders.
 *
 * @component
 * @param {FormInputProps} props - Component props
 * @param {UseFormRegister<ContactFormSchema>} props.register - React Hook Form register function
 * @param {keyof ContactFormSchema} props.name - Field name from the form schema
 * @param {string} props.label - Label text for the input field
 * @param {string} [props.type="text"] - HTML input type
 * @param {boolean} [props.error] - Whether the field has validation errors
 * @param {Function} props.setApiMessage - Function to set API response messages
 * @param {boolean} props.isLoading - Whether the form is in loading state
 *
 * @example
 * ```tsx
 * <FormInput
 *   register={register}
 *   name="email"
 *   label="Email Address"
 *   type="email"
 *   error={!!errors.email}
 *   setApiMessage={setApiMessage}
 *   isLoading={isLoading}
 * />
 * ```
 *
 * @returns {React.ReactElement} A form input field with label
 */
const FormInput = memo(function FormInput({
	register,
	name,
	label,
	type = "text",
	error,
	setApiMessage,
	isLoading,
}: FormInputProps) {
	return (
		<div className="contact-form__container__form-item">
			<label htmlFor={`contact-form-${name}`}>{label}</label>
			<Input
				{...register(name)}
				onChange={async (e) => {
					try {
						await register(name).onChange(e);
						setApiMessage(null);
					} catch (error) {
						console.error("Error in input onChange:", error);
					}
				}}
				type={type}
				id={`contact-form-${name}`}
				className="contact-form__container__form-item__field-input"
				data-error={error}
				disabled={isLoading}
			/>
		</div>
	);
});

export default FormInput;
