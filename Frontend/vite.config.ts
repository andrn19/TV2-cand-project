import { defineConfig } from 'vitest/config'
import react from '@vitejs/plugin-react'
import tailwindcss from 'tailwindcss'


export default defineConfig({
  plugins: [react()],
  test: {
    environment: 'jsdom',
    globals: true,
    setupFiles: './tests/setup.ts',
  },
  css: {
    postcss: {
      plugins: [tailwindcss()],
    },
  }
})
