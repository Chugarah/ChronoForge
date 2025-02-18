import { memo } from "react";
import {
	Select,
	SelectValue,
	SelectTrigger,
	SelectContent,
	SelectItem,
} from "@/components/ui/select";
import type { FormSelectProps } from "@/types/forms/contact";
import { cn } from "@/lib/utils";

const FormSelect = memo(function FormSelect({
	name,
	label,
	error,
	setApiMessage,
	setValue,
	isLoading,
}: FormSelectProps) {
	return (
		<div className="contact-form__container__form-item">
			<label htmlFor={`contact-form-${name}`}>{label}</label>
			<Select
				onValueChange={(value) => {
					setValue(name, value, {
						shouldValidate: true,
						shouldDirty: true,
					});
					setApiMessage(null);
				}}
				disabled={isLoading}
			>
				<SelectTrigger
					id={`contact-form-${name}`}
					data-error={error}
					className={cn(
						"contact-form__container__form-item__field-input",
						error && "border-system-error dark:border-system-error",
					)}
				>
					<SelectValue placeholder="Select specialist" />
				</SelectTrigger>
				<SelectContent>
					<SelectItem value="starcraft">Starcraft Master</SelectItem>
					<SelectItem value="warhammer">
						Warhammer 4000k Server Skull
					</SelectItem>
					<SelectItem value="billing">Billing Question</SelectItem>
					<SelectItem value="other">Other</SelectItem>
				</SelectContent>
			</Select>
		</div>
	);
});

export default FormSelect;
