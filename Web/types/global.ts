export interface ButtonProps extends React.ButtonHTMLAttributes<HTMLAnchorElement>, VariantProps<typeof button> {
  underline?: boolean
  href: string
  intent?: "primary" | "secondary"
  size?: "sm" | "lg"
}