import type { ComponentType, ReactNode } from 'react'

export interface NavigationComponent {
  title: string
  href: string
  description: string
  icon: ComponentType<{ className?: string }>
}

export interface NavigationListItemProps {
  className?: string
  title: string
  children?: ReactNode
  href: string
  icon?: ComponentType<{ className?: string }>
} 