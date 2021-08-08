const Mock = require('mockjs')

const data = Mock.mock({
  'items|30': [{
    pluginID: '@id',
    displayName: '@name(5, 10)',
    description: '@sentence(5, 10)',
    'status|1': ['enabled', 'disabled', 'uninstalled'],
    author: '@name(5, 10)',
    version: '1.2.5'
  }]
})

module.exports = [
  {
    url: '/api/admin/plugins/list',
    type: 'get',
    response: config => {
      const items = data.items
      return {
        code: 20000,
        data: {
          total: items.length,
          items: items
        }
      }
    }
  }
]
