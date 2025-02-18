import { NextResponse } from 'next/server';
import type { FaqItem } from '@/types/faq';

const faqs: FaqItem[] = [
  {
    id: '1',
    title: 'What is ChronoForge?',
    content: 'ChronoForge is a powerful time management and productivity platform...'
  },
  // Add more FAQ items as needed
];

export async function GET() {
  return NextResponse.json(faqs);
} 