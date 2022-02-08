var util = {};

// post 方式发送, 内容以 queryString 格式组织: ?a=2&b=3 添加到url?后, 最终整个 url 是经过编码的
util.httpGet = function(url,success,error){
    $.ajax({
        url:url,
        type: "get",
        async: true, // 异步: true
        success: success,
        error: error
    });
};

// post 方式发送, 内容以 json 格式组织: {'name':'jack','sex':'man'} 放于 请求体
util.httpPost = function(url,data,success,error){
    $.ajax({
        url:url,
        type: "post",
        async: true,
        contentType: "application/json", // 发送的内容 的数据格式类型
        dataType: "json", // 预期服务器返回的数据格式类型, 如果不指定, jQuery 将自动根据 HTTP 包 MIME 信息来智能判断
        data:data,
        success: success,
        error: error
    });
};


// post 方式发送, 内容以 form-data 格式组织: key1=value1&key2=value2 放于 请求体
// form-data 对应的是 以form表单提交传值的情形
// x-www-form-urlencoded
// 即 application/x-www-from-urlencoded，将表单内的数据转换为 Key-Value。
// POST  HTTP/1.1
// Host: test.app.com
// Content-Type: application/x-www-form-urlencoded

// key1=value1&key2=value2
util.httpPostForm = function(url,data,success,error){
    $.ajax({
        url:url,
        type: "post",
        async: true,
        contentType: "application/x-www-form-urlencoded", // 发送的内容 的数据格式类型
        data:data,
        success: success,
        error: error
    });
};




// post 方式发送, 内容以 键值对 key1:val1 格式组织: a:1 放于 请求体, contentType 为自动计算，用于多部分文件上传(file=blob, 其后有文件内容定界符boundary)
// multipart/form-data: 由于有boundary隔离，所以multipart/form-data既可以上传文件，也可以上传键值对，它采用了键值对的方式，所以可以上传多个文件
util.httpFormData = function(url,data,success,error){
    $.ajax({
        url:url,
        type: "post",
        data:data,
        contentType: false,//"multipart/form-data",
        processData: false,
        success: success,
        error: error
    });
};

// 字符串格式化
util.format = function(src){
    if (arguments.length == 0) return null;
    let args = Array.prototype.slice.call(arguments, 1);
    return src.replace(/\{(\d+)\}/g, function(m, i){
        return args[i];
    });
};

// 设置cookie的函数  （名字，值，过期时间（天））
util.setCookie = function (cname, cvalue, exdays) {
    let d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
};

//获取cookie
//取cookie的函数(名字) 取出来的都是字符串类型 子目录可以用根目录的cookie，根目录取不到子目录的 大小4k左右
util.getCookie = function(cname) {
    let name = cname + "=";
    let ca = document.cookie.split(';');
    for(let i=0; i<ca.length; i++)
    {
        let c = ca[i].trim();
        if (c.indexOf(name)===0) return c.substring(name.length,c.length);
    }
    return "";
};

util.percent = function (v) {
    let n = parseFloat(v);
    return util.format("{0}%", n.toFixed(2))
};

util.str2Int = function (v) {
    return parseInt(v)
};

util.unit = new Array("B", "KB", "MB", "GB");

util.b2string = function(v,rate) {
    let n = v;
    let i = 0;
    for (;n > rate;) {
        n /= rate;
        i++;
        if (i === util.unit.length){
            break
        }
    }
    return util.format("{0}{1}",Math.round(n), util.unit[i])
};

function showTips(msg,t) {
    let tip = document.getElementById('tips');
    tip.style.display = "block";

    let m = document.getElementById("tips-msg");
    m.innerText=msg;
    setTimeout(function(){ tip.style.display = "none"},t)
}

// 全部选中，checkAll状态为true。 有一个选中批量操作按钮可点击，否则不能
function checkState(isChecked) {
    let chk_list = document.getElementsByName("checkbox");
    let checkedNum = 0;
    for(let i=0;i<chk_list.length;i++){
        if (chk_list[i].checked){
            checkedNum++
        }
    }

    if (checkedNum === chk_list.length){
        document.getElementById("checkAll").checked = true;
    }else {
        document.getElementById("checkAll").checked = false;
    }

    let batchBtn = document.getElementsByClassName("batch-btn");
    if (checkedNum === 0){
        for (let i =0;i < batchBtn.length;i++){
            batchBtn[i].disabled = true
        }
    }else {
        for (let i =0;i < batchBtn.length;i++){
            batchBtn[i].disabled = false
        }
    }

}

function checkAllState(isChecked){
    let chk_list = document.getElementsByName("checkbox");
    for(let i=0;i<chk_list.length;i++){
        chk_list[i].checked=isChecked;
    }

    let batchBtn = document.getElementsByClassName("batch-btn");
    if (!isChecked){
        for (let i =0;i < batchBtn.length;i++){
            batchBtn[i].disabled = true
        }
    }else {
        for (let i =0;i < batchBtn.length;i++){
            batchBtn[i].disabled = false
        }
    }
}

export { util };