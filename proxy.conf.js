const PROXY_CONFIG = [
  {
    context: ["/api"], // Defines the routes to be proxied
    target: "https://localhost:7258", // Your backend API URL
    secure: false, // Set to true if your backend uses HTTPS
    changeOrigin: true, // Adjusts the "Origin" header to match the target URL
    logLevel: "debug" // Provides detailed logging for proxy activity
  }
];

module.exports = PROXY_CONFIG;

