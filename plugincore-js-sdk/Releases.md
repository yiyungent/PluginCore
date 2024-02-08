


# # @yiyungent/plugincore-v0.5.0

## Added

- 支持 `headers` 自定义请求头
  - `plugincoreInstance.start(null, {'X-Custom-Header': 'foobar'});`



# @yiyungent/plugincore-v0.4.0

## Added

- 支持加载 `外部 css`, 即 `<link rel="stylesheet" href="https://unpkg.com/aplayer@1.10.1/dist/APlayer.min.css">`
- `addEventListener` 公开, 即 `window.plugincore.addEventListener = addEventListener;`


# @yiyungent/plugincore-v0.3.0

## Added

- 支持加载 `<script src="https://unpkg.com/aplayer@1.10.1/dist/APlayer.min.js"></script>`
  - [ ] 暂不支持 `<link rel="stylesheet" href="https://unpkg.com/aplayer@1.10.1/dist/APlayer.min.css">`, 但已加入计划
- 支持 `p.addEventListener("load", (src)=>{})` 监听事件, 用于插件 `script` 在确保加载了需要的 js 资源后再执行

> 示例

```html
<link rel="stylesheet" href="https://unpkg.com/aplayer@1.10.1/dist/APlayer.min.css">
<div id="aplayer"></div>
<script src="https://unpkg.com/aplayer@1.10.1/dist/APlayer.min.js"></script>
<script>
    // p 为 PluginCore 对象实例
    p.addEventListener("load", (src) => {

        if (src != "https://unpkg.com/aplayer@1.10.1/dist/APlayer.min.js") {
            return;
        }

        // 参考:  
        // https://aplayer.js.org/config.js
        // https://api.i-meto.com/meting/api/index.php?server=netease
        const ap = new APlayer({
            container: document.getElementById('aplayer'),
            theme: '#F57F17',
            lrcType: 3,
            audio: [{
                name: '光るなら',
                artist: 'Goose house',
                url: 'https://api.i-meto.com/meting/api?server=netease&type=url&id=35847388&auth=ea47006318283154c3dc1e60fe3b37f0a20a2657',
                cover: 'https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951164852190706&auth=b55cdaa7ac51009b74388bf15a392a3a6adabd17',
                lrc: 'https://api.i-meto.com/meting/api?server=netease&type=lrc&id=1464110291&auth=c1dd39b39f416702f68fa15d44953e8c4950bdd2',
                theme: '#ebd0c2'
            }, {
                name: 'トリカゴ',
                artist: 'XX:me',
                url: 'https://api.i-meto.com/meting/api?server=netease&type=url&id=33166086&auth=35fd43fe4c336c1368c99b707e2d688dbf4d2fb2',
                cover: 'https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951164852190706&auth=b55cdaa7ac51009b74388bf15a392a3a6adabd17',
                lrc: 'https://api.i-meto.com/meting/api?server=netease&type=lrc&id=1464110291&auth=c1dd39b39f416702f68fa15d44953e8c4950bdd2',
                theme: '#46718b'
            }, {
                name: '前前前世',
                artist: 'RADWIMPS',
                url: 'https://api.i-meto.com/meting/api?server=netease&type=url&id=35847388&auth=ea47006318283154c3dc1e60fe3b37f0a20a2657',
                cover: 'https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951164852190706&auth=b55cdaa7ac51009b74388bf15a392a3a6adabd17',
                lrc: 'https://api.i-meto.com/meting/api?server=netease&type=lrc&id=1464110291&auth=c1dd39b39f416702f68fa15d44953e8c4950bdd2',
                theme: '#505d6b'
            }]
        });

    })
</script>
```


