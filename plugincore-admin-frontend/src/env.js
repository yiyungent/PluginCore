// import packageJson from '../package.json' // 报错
const { version } = require('../package.json')

module.exports = {
  version: version, // process.env.npm_package_version,
  baseUrl: process.env.NODE_ENV === 'cdn' ? `https://cdn.jsdelivr.net/gh/yiyungent/plugincore-admin-frontend@${version}/dist-cdn` : '/PluginCore/Admin'
}
