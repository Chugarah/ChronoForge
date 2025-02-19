import type { Metadata } from 'next'
import { Inter } from 'next/font/google'
import './globals.css'
import { Footer } from '@/components/layout/Footer'
import { Header } from '@/components/layout/Header'
import { ThemeProvider } from '@/components/theme/theme-provider'
import { QueryProvider } from '@/lib/providers/query-provider'
import { cn } from '@/lib/utils'

const inter = Inter({ subsets: ['latin'] })

export const metadata: Metadata = {
  title: 'ChronoForge',
  description: 'Empowering developers to build better software through efficient time management.',
}

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="en" suppressHydrationWarning>
      <body 
        className={cn(
          inter.className,
          "min-h-screen flex flex-col antialiased",
          "bg-[hsl(var(--background))] text-[hsl(var(--foreground))]",
          "selection:bg-[hsl(var(--primary))] selection:text-[hsl(var(--primary-foreground))]"
        )}
      >
        <ThemeProvider
          attribute="class"
          defaultTheme="system"
          enableSystem
          disableTransitionOnChange
        >
          <QueryProvider>
            <div className="flex flex-col min-h-screen">
              <div className="mx-auto w-full max-w-[var(--container-width)] px-[var(--spacing-4)] flex-1 flex flex-col">
                <Header />
                <main className="flex-1 w-full">
                  {children}
                </main>
                <Footer />
              </div>
            </div>
          </QueryProvider>
        </ThemeProvider>
      </body>
    </html>
  )
}
