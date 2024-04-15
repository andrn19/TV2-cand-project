import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import tailwindcss from 'tailwindcss'


export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/gateway/list-endpoints': 'http://localhost:52001'
    }
  },
  css: {
    postcss: {
      plugins: [tailwindcss()],
    },
  }
})
