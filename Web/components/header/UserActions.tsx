import { ThemeToggle } from "@/components/theme/theme-toggle"
import { Button } from "@/components/ui/button"

export function UserActions() {
  return (
    <div className="flex items-center space-x-4">
      <ThemeToggle />
      <div className="hidden sm:flex sm:items-center sm:space-x-4">
        <Button variant="ghost" size="sm" className="hover:bg-accent">
          Sign In
        </Button>
        <Button size="sm" className="bg-gradient-to-r from-indigo-500 via-purple-500 to-pink-500 text-white hover:opacity-90">
          Get Started
        </Button>
      </div>
    </div>
  )
} 