import { 
  Calendar,
  LayoutDashboard,
  LogIn,
  Menu,
  Settings,
  Sparkles,
  Users2
} from "lucide-react"
import Link from 'next/link'
import { Logo } from "@/components/header/Logo"
import { type NavigationItem, NavigationItems } from "@/components/header/NavigationItems"
import { Button } from "@/components/ui/button"
import { Separator } from "@/components/ui/separator"
import {
  Sheet,
  SheetContent,
  SheetDescription,
  SheetTitle,
  SheetTrigger,
} from "@/components/ui/sheet"

export function MobileNav() {
  return (
    <Sheet>
      <SheetTrigger asChild className="min-(--breakpoint-md):hidden">
        <Button 
          variant="ghost" 
          size="icon" 
          className="mr-2 hover:bg-theme-elevated text-theme"
        >
          <Menu className="h-5 w-5" />
          <span className="sr-only">Toggle menu</span>
        </Button>
      </SheetTrigger>
      <SheetContent 
        side="left" 
        className="w-[300px] sm:w-[400px] p-0 bg-theme border-border/40"
      >
        <div className="border-b border-border/40 px-6 py-4 bg-theme-elevated">
          <Logo />
          <SheetTitle className="sr-only">Navigation Menu</SheetTitle>
          <SheetDescription className="sr-only">
            Main navigation menu for mobile devices
          </SheetDescription>
        </div>
        <nav className="flex flex-col space-y-1 p-6 bg-theme">
          {NavigationItems.map((item: NavigationItem) => {
            // Map route to icon
            const IconComponent = {
              '/': LayoutDashboard,
              '/calendar': Calendar,
              '/team': Users2,
              '/settings': Settings
            }[item.href] || LayoutDashboard;

            return (
              <Link
                key={item.href}
                href={item.href}
                className="group flex items-center space-x-3 rounded-lg px-3 py-2.5 text-sm font-medium text-theme hover:bg-theme-elevated hover:text-theme-primary hover-elevated"
              >
                <IconComponent className="h-4 w-4 text-muted-foreground group-hover:text-theme-primary" />
                <span>{item.title}</span>
              </Link>
            );
          })}
          <Separator className="my-4 bg-border/40" />
          <div className="space-y-2">
            <Button 
              variant="outline" 
              className="w-full justify-start border-border/40 text-theme hover:bg-theme-elevated hover:text-theme-primary" 
              size="sm"
            >
              <LogIn className="mr-2 h-4 w-4" />
              Sign In
            </Button>
            <Button 
              className="w-full justify-start bg-theme-primary text-theme hover:bg-theme-primary/90" 
              size="sm"
            >
              <Sparkles className="mr-2 h-4 w-4" />
              Get Started
            </Button>
          </div>
        </nav>
      </SheetContent>
    </Sheet>
  )
} 