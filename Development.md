 # 参与开发 指南


## git cz

Commitizen是一个撰写合格 Commit message 的工具。

安装命令如下。

```shell
npm install -g commitizen
```

然后，在项目目录里，运行下面的命令，使其支持 Angular 的 Commit message 格式。

```shell
commitizen init cz-conventional-changelog --save --save-exact
```

以后，凡是用到 `git commit` 命令，一律改为使用 `git cz` 。这时，就会出现选项，用来生成符合格式的 Commit message。


