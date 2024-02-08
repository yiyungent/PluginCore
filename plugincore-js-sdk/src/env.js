// import packageJson from '../package.json' // 报错
const { version } = require('../package.json')

module.exports = {
  version: version, // process.env.npm_package_version,
}
