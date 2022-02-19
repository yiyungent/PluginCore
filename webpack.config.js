var path = require('path')
const TerserPlugin = require("terser-webpack-plugin");
const CssMinimizerPlugin = require('css-minimizer-webpack-plugin');

// 目前配置: css, fonts, 小图等会被打包到一个 js 中, js 混淆压缩，去除注释, css不会去除注释

module.exports = {
  mode: 'production',
  entry: './src/main.js',
  output: {
    filename: "PluginCore.min.js",
    library: {
        name: 'PluginCore',
        type: 'umd',
    },
    // 注意: 直接暴露 .default，这样 在浏览器中 就可以直接使用 PluginCore
    // 加上后: PluginCore:  ƒ r(){this.util=n}
    // 不加此项，就需要再次 PluginCore.default
    // PluginCore: Module {__esModule: true, Symbol(Symbol.toStringTag): "Module", default: ƒ r()}
    libraryExport: "default",
    umdNamedDefine: true,
    path: path.resolve(__dirname, "dist"),
    publicPath: '/dist/',
    clean: true,
  },
  module: {
    rules: [
      {
        test: /\.css$/,
        use: [
          { loader: "style-loader" },
          { loader: "css-loader" }
        ],
      },
      {
        test: /\.m?js$/,
        exclude: /(node_modules|bower_components)/,
        use: {
          loader: 'babel-loader',
          options: {
            presets: ['@babel/preset-env'],
            plugins: ['@babel/plugin-transform-runtime']
          }
        }
      },
      {
        test: /\.(png|jpg|gif|svg)$/,
        loader: 'file-loader',
        options: {
          name: '/imgs/[name].[ext]?[hash]'
        }
      },
      {
        test: /\.(png|woff|woff2|svg|ttf|eot)(\?.*)?$/,
        use: {
          loader: 'url-loader',
          options: {
            limit: 100000,  //这里要足够大这样所有的字体图标都会打包到css中
          }
        }
      }
    ]
  },
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src')
    },
    extensions: ['*', '.js', '.json']
  },
  devServer: {
    contentBase: "./",
  },
  // devtool: '#eval-source-map',
  optimization: {
    minimize: true,
    minimizer: [

      new TerserPlugin({
        terserOptions: {
          format: {
            comments: false,
          },
        },
        extractComments: false,
      }),

      new CssMinimizerPlugin({
        minimizerOptions: {
          preset: [
            'default',
            {
              discardComments: { removeAll: true },
            },
          ],
        },
      }),

    ],
  },
  plugins: [
    
  ]
}

