import { IconDefinition } from '@fortawesome/fontawesome-svg-core';
import { 
  faArrowRight,
  faBars,
  faChevronDown,
  faChevronUp,
  // Add more solid icons as needed
} from '@fortawesome/free-solid-svg-icons';

import {
  faEnvelope,
  faUser,
  // Add more regular icons as needed
} from '@fortawesome/free-regular-svg-icons';

// Export commonly used icons
export const Icons = {
  // Solid icons
  arrowRight: faArrowRight,
  bars: faBars,
  chevronDown: faChevronDown,
  chevronUp: faChevronUp,
  
  // Regular icons
  envelope: faEnvelope,
  user: faUser,
} as const;

// Type for our icon keys
export type IconKey = keyof typeof Icons;

// Helper function to get icon by key
export const getIcon = (key: IconKey): IconDefinition => Icons[key];

// Export individual icons for direct imports
export {
  faArrowRight,
  faBars,
  faChevronDown,
  faChevronUp,
  faEnvelope,
  faUser,
}; 