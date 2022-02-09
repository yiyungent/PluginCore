/*
 * @Author: yiyun
 * @Description:
 */
export default (options) => {
    // default options
    const defaultOption = {
      baseUrl: options.baseUrl || "",
    };
    // 附上默认值
    for (const defaultKey in defaultOption) {
      if (
        defaultOption.hasOwnProperty(defaultKey) &&
        !options.hasOwnProperty(defaultKey)
      ) {
        options[defaultKey] = defaultOption[defaultKey];
      }
    }
  
    return options;
  };