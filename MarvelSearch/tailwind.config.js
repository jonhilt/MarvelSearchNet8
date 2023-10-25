/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["**/*.razor", "**/*.cshtml", "**/*.html"],
  theme: {
    extend: {
      fontFamily: {
        'sans': ['Open Sans', 'sans-serif'],
      }
    },
  },
  plugins: [],
}

