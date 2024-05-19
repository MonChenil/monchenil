/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,js,ts}"],
  plugins: [require("daisyui")],
  daisyui: {
    themes: [
      {
        monchenil: {
          primary: "#00bd70",
          "primary-content": "#ffffff",
          secondary: "#00ABFF",
          accent: "#FF6254",
          neutral: "#2b3440",
          "base-100": "#ffffff",
        },
      },
    ],
  },
};
