"use client"

import { motion } from "framer-motion"
import { DesktopNav } from "@/components/header/DesktopNav"
import { Logo } from "@/components/header/Logo"
import { MobileNav } from "@/components/header/MobileNav"
import { UserActions } from "@/components/header/UserActions"

const headerVariants = {
  hidden: { 
    opacity: 0,
    y: -20
  },
  visible: {
    opacity: 1,
    y: 0,
    transition: {
      duration: 0.6,
      ease: [0.22, 1, 0.36, 1], // Linear's custom ease
    }
  }
}

export function Header() {
  return (
    <motion.header
      initial="hidden"
      animate="visible"
      variants={headerVariants}
      className="sticky top-0 z-50 w-full border-b border-border/40 bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60"
    >
      <div className="container flex h-16 items-center justify-between">
        <motion.div 
          className="flex items-center gap-4"
          initial={{ opacity: 0, x: -20 }}
          animate={{ opacity: 1, x: 0 }}
          transition={{ 
            delay: 0.2,
            duration: 0.5,
            ease: [0.22, 1, 0.36, 1]
          }}
        >
          <MobileNav />
          <Logo />
        </motion.div>
        <div className="flex-1 flex justify-center">
          <DesktopNav />
        </div>
        <motion.div
          initial={{ opacity: 0, x: 20 }}
          animate={{ opacity: 1, x: 0 }}
          transition={{ 
            delay: 0.3,
            duration: 0.5,
            ease: [0.22, 1, 0.36, 1]
          }}
        >
          <UserActions />
        </motion.div>
      </div>
    </motion.header>
  )
} 