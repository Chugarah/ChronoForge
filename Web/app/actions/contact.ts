"use server";

import { contactFormSchema } from "@/lib/validation/schemas/contactForm";
import type { ContactFormState } from "@/types/forms/contact";

export async function submitContactForm(
  prevState: ContactFormState,
  formData: FormData
): Promise<ContactFormState> {
  try {
    // Validate form data
    const validatedFields = contactFormSchema.safeParse({
      fullName: formData.get("fullName"),
      email: formData.get("email"),
      specialist: formData.get("specialist"),
    });

    // Return early if validation fails
    if (!validatedFields.success) {
      return {
        message: null,
        error: "You need to construct additional pylons! Please check your inputs.",
      };
    }

    // Make API request
    const response = await fetch(
      "https://win24-assignment.azurewebsites.net/api/forms/contact",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(validatedFields.data),
      }
    );

    if (!response.ok) {
      throw new Error("Failed to submit form");
    }

    return {
      message: "Thank you for contacting us! Our Starcraft Master will contact you shortly to discuss your Pylon needs.",
      error: null,
    };
  } catch (error) {
    return {
      message: null,
      error: "Not enough minerals! Please try again later.",
    };
  }
} 