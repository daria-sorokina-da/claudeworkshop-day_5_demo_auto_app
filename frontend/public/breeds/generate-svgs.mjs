// Generates a distinct stylized horse SVG for each breed.
// Run with: node generate-svgs.mjs
import { writeFileSync } from 'node:fs';

/**
 * Each breed varies: background palette, coat color, mane/tail color,
 * lower-leg color (for socks/feathering), and optional features
 * (spots for Appaloosa, blaze, feathering, metallic sheen).
 */
const breeds = [
  { id: 'arabian',       bg: ['#f3d9b1', '#e6b87a'], coat: '#9c5a2c', mane: '#3d2410', legs: '#9c5a2c', blaze: true,  dished: true },
  { id: 'thoroughbred',  bg: ['#cfe3c9', '#9cc290'], coat: '#6e4326', mane: '#2c1a0e', legs: '#241208', blaze: true },
  { id: 'friesian',      bg: ['#d8d2e0', '#a99fc0'], coat: '#1c1c1c', mane: '#000000', legs: '#1c1c1c', feather: true, lush: true },
  { id: 'andalusian',    bg: ['#e7dccb', '#c9b596'], coat: '#c7c2bb', mane: '#8a8682', legs: '#c7c2bb', lush: true },
  { id: 'mustang',       bg: ['#e8cf9f', '#cf9b5c'], coat: '#b07b43', mane: '#3a2614', legs: '#2c1a0c', dun: true },
  { id: 'clydesdale',    bg: ['#cdd9e6', '#9fb4cc'], coat: '#7a4a28', mane: '#241208', legs: '#f4efe6', feather: true, big: true, blaze: true },
  { id: 'appaloosa',     bg: ['#e6dccf', '#c2a98a'], coat: '#caa46a', mane: '#5a3c1e', legs: '#caa46a', spots: true },
  { id: 'lipizzaner',    bg: ['#dce4ec', '#b4c2d4'], coat: '#eceae6', mane: '#d4d0c8', legs: '#eceae6', lush: true },
  { id: 'icelandic',     bg: ['#cfe0e6', '#94b8c4'], coat: '#a8632e', mane: '#e8d4a8', legs: '#a8632e', lush: true, big: true },
  { id: 'quarter-horse', bg: ['#e8d3a8', '#cfa05c'], coat: '#a8521f', mane: '#7a3a14', legs: '#a8521f' },
  { id: 'akhal-teke',    bg: ['#efe0b0', '#d8b85c'], coat: '#d9a93c', mane: '#a87a24', legs: '#d9a93c', metallic: true },
  { id: 'morgan',        bg: ['#e3d0bb', '#c4a17a'], coat: '#7a3e1c', mane: '#2c1608', legs: '#2c1608', blaze: true },
];

function svg(b) {
  const [sky, ground] = b.bg;
  const id = b.id.replace(/[^a-z]/g, '');
  const scale = b.big ? 1.08 : 1;
  const cx = 200, cy = 150;

  // ── feature: Appaloosa leopard spots over the body ──
  const spots = b.spots ? `
    <g fill="#3a2410" opacity="0.85" clip-path="url(#body-${id})">
      <ellipse cx="180" cy="135" rx="9" ry="7"/>
      <ellipse cx="215" cy="150" rx="11" ry="8"/>
      <ellipse cx="245" cy="132" rx="8" ry="6"/>
      <ellipse cx="160" cy="158" rx="7" ry="6"/>
      <ellipse cx="200" cy="170" rx="10" ry="7"/>
      <ellipse cx="235" cy="165" rx="7" ry="6"/>
      <ellipse cx="265" cy="148" rx="6" ry="5"/>
    </g>` : '';

  // ── feature: white blaze down the face ──
  const blaze = b.blaze ? `<path d="M 96 96 L 102 96 L 100 138 L 95 138 Z" fill="#f4efe6"/>` : '';

  // ── feature: feathered lower legs (Clydesdale/Friesian) ──
  const feather = b.feather ? `
    <g fill="${b.legs}">
      <path d="M 150 196 q -10 8 -8 22 q 9 -4 18 0 q 2 -14 -2 -22 Z"/>
      <path d="M 178 196 q -10 8 -8 22 q 9 -4 18 0 q 2 -14 -2 -22 Z"/>
      <path d="M 234 196 q -10 8 -8 22 q 9 -4 18 0 q 2 -14 -2 -22 Z"/>
      <path d="M 262 196 q -10 8 -8 22 q 9 -4 18 0 q 2 -14 -2 -22 Z"/>
    </g>` : '';

  // ── feature: dorsal stripe for dun coloring (Mustang) ──
  const dorsal = b.dun ? `<path d="M 110 108 Q 200 96 280 150" stroke="#3a2614" stroke-width="5" fill="none" opacity="0.6"/>` : '';

  // metallic sheen overlay for Akhal-Teke
  const coatFill = b.metallic ? `url(#sheen-${id})` : b.coat;

  return `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 400 250" width="400" height="250" role="img" aria-label="${b.id} horse">
  <defs>
    <linearGradient id="bg-${id}" x1="0" y1="0" x2="0" y2="1">
      <stop offset="0%" stop-color="${sky}"/>
      <stop offset="100%" stop-color="${ground}"/>
    </linearGradient>
    <radialGradient id="sheen-${id}" cx="0.4" cy="0.35" r="0.9">
      <stop offset="0%" stop-color="#f6e08a"/>
      <stop offset="55%" stop-color="${b.coat}"/>
      <stop offset="100%" stop-color="#9c7820"/>
    </radialGradient>
    <clipPath id="body-${id}">
      <ellipse cx="210" cy="150" rx="78" ry="44"/>
    </clipPath>
  </defs>

  <rect width="400" height="250" fill="url(#bg-${id})"/>
  <ellipse cx="200" cy="226" rx="150" ry="14" fill="#000" opacity="0.12"/>

  <g transform="translate(${cx} ${cy}) scale(${scale}) translate(${-cx} ${-cy})">
    <!-- shadow under belly -->
    <ellipse cx="210" cy="208" rx="90" ry="12" fill="#000" opacity="0.10"/>

    <!-- tail -->
    <path d="M 285 130 q 34 18 24 70 q -6 14 -16 18 q 8 -28 -2 -50 q -8 -18 -22 -30 Z" fill="${b.mane}"/>

    <!-- back legs -->
    <rect x="248" y="178" width="16" height="44" rx="6" fill="${b.legs}"/>
    <rect x="276" y="178" width="16" height="44" rx="6" fill="${b.coat}"/>
    <!-- hooves -->
    <rect x="247" y="218" width="18" height="8" rx="3" fill="#2c2218"/>
    <rect x="275" y="218" width="18" height="8" rx="3" fill="#2c2218"/>

    <!-- front legs -->
    <rect x="148" y="178" width="16" height="44" rx="6" fill="${b.legs}"/>
    <rect x="176" y="178" width="16" height="44" rx="6" fill="${b.coat}"/>
    <rect x="147" y="218" width="18" height="8" rx="3" fill="#2c2218"/>
    <rect x="175" y="218" width="18" height="8" rx="3" fill="#2c2218"/>

    <!-- body -->
    <ellipse cx="210" cy="150" rx="78" ry="44" fill="${coatFill}"/>
    ${dorsal}
    ${spots}

    <!-- chest/neck -->
    <path d="M 150 150 Q 120 150 108 110 Q 100 84 112 72 L 140 96 Q 152 120 168 132 Z" fill="${coatFill}"/>

    <!-- head -->
    <path d="M 112 72 Q 96 70 88 ${b.dished ? '92' : '88'} Q 82 108 90 134 Q 94 142 104 140 L 116 100 Z" fill="${coatFill}"/>
    ${blaze}

    <!-- muzzle -->
    <ellipse cx="92" cy="132" rx="11" ry="9" fill="${b.dished ? b.coat : '#000'}" opacity="${b.dished ? '0.9' : '0.55'}"/>

    <!-- ears -->
    <path d="M 118 70 L 124 50 L 132 72 Z" fill="${coatFill}"/>
    <path d="M 132 72 L 142 54 L 146 76 Z" fill="${coatFill}"/>

    <!-- mane along the neck -->
    <path d="M 124 52 Q 150 70 168 118 Q 150 108 138 116 Q 132 84 118 70 Z" fill="${b.mane}"/>
    ${b.lush ? `<path d="M 138 116 Q 158 128 170 150 Q 150 140 140 146 Z" fill="${b.mane}"/>` : ''}

    <!-- forelock -->
    <path d="M 110 74 Q 100 84 96 98 Q 108 90 116 96 Z" fill="${b.mane}"/>

    <!-- eye -->
    <circle cx="106" cy="100" r="3.4" fill="#1a120a"/>
    <circle cx="107" cy="99" r="1" fill="#fff" opacity="0.8"/>

    ${feather}
  </g>
</svg>`;
}

for (const b of breeds) {
  writeFileSync(new URL(`./${b.id}.svg`, import.meta.url), svg(b));
  console.log('wrote', b.id + '.svg');
}
console.log('Done:', breeds.length, 'breeds');
