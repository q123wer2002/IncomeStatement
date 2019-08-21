module.exports = {
  publicPath: process.env.NODE_ENV === 'production'
    ? '/AppTemplate/'
    : '/',
  outputDir: '../wwwroot'
};