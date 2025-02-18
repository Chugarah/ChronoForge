/// <reference types="next" />
/// <reference types="next/image-types/global" />

// NOTE: This file should not be edited
// see https://nextjs.org/docs/basic-features/typescript for more information.

// Global type augmentations
declare global {
	interface Window {
		// Add custom window properties
	}
}

// Module declarations
declare module "*.css" {
	const styles: { [className: string]: string };
	export default styles;
}

declare module "*.woff2" {
	const content: string;
	export default content;
}

declare module "*.ttf" {
	const content: string;
	export default content;
}

declare module '*.css' {
	const content: { [className: string]: string };
	export default content;
}

declare module '*.module.css' {
	const classes: { [key: string]: string };
	export default classes;
}

// Add support for importing images
declare module '*.svg' {
	const content: React.FunctionComponent<React.SVGAttributes<SVGElement>>;
	export default content;
}

declare module '*.png' {
	const content: string;
	export default content;
}

declare module '*.jpg' {
	const content: string;
	export default content;
}

declare module '*.jpeg' {
	const content: string;
	export default content;
}

declare module '*.gif' {
	const content: string;
	export default content;
}

declare module '*.webp' {
	const content: string;
	export default content;
}

export {};
