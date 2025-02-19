export interface NavigationItem {
  title: string
  href: string
  description: string
}

export const NavigationItems: NavigationItem[] = [
  {
    title: "Projects",
    href: "/project",
    description: "View our showcase of projects and achievements.",
  },
  {
    title: "About",
    href: "/about",
    description: "Learn more about our mission and team.",
  },
  {
    title: "Contact",
    href: "/contact",
    description: "Get in touch with us for any inquiries.",
  },
] 