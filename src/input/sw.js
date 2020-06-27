const VERSION = '1.0.0';
const CACHE_NAME = 'developmomentum-offline-v' + VERSION;
const OFFLINE_PAGE = 'offline.html';

const SHELL_ASSETS = [
  'site.webmanifest',
  'browserconfig.xml',
  'assets/images/android-chrome-192x192.png',
  'assets/images/android-chrome-512x512.png',
  'assets/images/apple-touch-icon.png',
  'assets/images/favicon.ico',
  'assets/images/favicon-32x32.png',
  'assets/images/favicon-16x16.png',
  'assets/images/mstile-70x70.png',
  'assets/images/mstile-144x144.png',
  'assets/images/mstile-150x150.png',
  'assets/images/mstile-310x150.png',
  'assets/images/mstile-310x310.png',
  'assets/images/safari-pinned-tab.svg',
  'assets/images/logo.svg',
  'assets/fonts/dm-serif-display-v4-latin-regular.woff',
  'assets/fonts/dm-serif-display-v4-latin-regular.woff2',
  'assets/fonts/open-sans-v16-latin-regular.woff',
  'assets/fonts/open-sans-v16-latin-regular.woff2',
  'assets/fonts/questrial-v9-latin-regular.woff',
  'assets/fonts/questrial-v9-latin-regular.woff2',
  'assets/bootstrap/bootstrap.css',
  'assets/css/styles.css',
  'assets/js/themes.js',
  OFFLINE_PAGE
];

self.addEventListener('install', (event) => {
  event.waitUntil(
    caches.open(CACHE_NAME).then(async cache => {
      await cache.addAll(SHELL_ASSETS);
    })
  );
});

self.addEventListener('activate', (event) => {
  // remove old caches
  event.waitUntil(
    caches.keys().then(availableCaches => {
      return Promise.all(
        availableCaches
          .filter(cacheName => cacheName !== CACHE_NAME)
          .map(cacheName => caches.delete(cacheName))
      );
    })
  );

  self.clients.claim();
});

self.addEventListener('fetch', (event) => {
  if (event.request.mode === 'navigate') {
    // network first for documents
    event.respondWith((async () => {
      try {
        return await fetch(event.request);
      } catch (error) {
        // network error, so retrieve offline page from cache
        const cache = await caches.open(CACHE_NAME);
        return await cache.match(OFFLINE_PAGE);
      }
    })());
  } else {
    // cache first for assets
    event.respondWith(
      caches.open(CACHE_NAME).then(async cache => {
        return await cache.match(event.request) || fetch(event.request);
      })
    );
  }
});