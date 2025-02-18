import type { UseFormRegister, UseFormSetValue } from "react-hook-form";
import type { ContactFormSchema } from "@/lib/validation/schemas/contactForm";
import type { apiResponseProp } from "@/types/api/global";

export interface FormInputProps {
  register: UseFormRegister<ContactFormSchema>;
  name: keyof ContactFormSchema;
  label: string;
  type?: string;
  error?: boolean;
  setApiMessage: (message: apiResponseProp<{ message: string }> | null) => void;
  isLoading: boolean;
}

export interface FormSelectProps {
  name: keyof ContactFormSchema;
  label: string;
  error?: boolean;
  setApiMessage: (message: apiResponseProp<{ message: string }> | null) => void;
  setValue: UseFormSetValue<ContactFormSchema>;
  isLoading: boolean;
}

export type ContactFormState = {
  message: string | null;
  error: string | null;
}; 