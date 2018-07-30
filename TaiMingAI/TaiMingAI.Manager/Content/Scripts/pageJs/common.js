var Common = function () {
    /**
     * Ajax公用方法
     * @param {string} url：请求地址
     * @param {object} data：请求数据
     * @param {function} successFunc:成功回调
     * @param {string} type:默认POST
     * @param {boolean} async
     */
    var commonAjax = function (url, data, successFunc, type, async) {
        layui.use(['layer', 'jquery'], function (laytpl) {
            var layer = layui.layer;
            $ = layui.$;

            async = async == undefined ? true : async;
            type = !!type ? type : "POST";
            $.ajax({
                url: url,
                type: type,
                async: async,
                dataType: "json",
                data: data,
                beforeSend: function () { layer.load(2); },
                success: function (response) {
                    try {
                        if (typeof successFunc === "function") {
                            successFunc(response);
                        }
                    } catch (e) {
                        layer.msg("操作异常,请重新登录操作");
                        console.log(e);
                    }
                },
                error: function (XMLHttpRequest, errorInfo) {
                    layer.msg("请求失败,请重新登录or联系管理员");
                    console.log(XMLHttpRequest);
                    console.log(errorInfo);
                },
                complete: function () { layer.closeAll("loading"); }
            });
        });
    }
    /**
     * 获取当前时间：h:m:s ms
     */
    var getTime = function () {
        var now = new Date(),
            h = now.getHours(),
            m = now.getMinutes(),
            s = now.getSeconds(),
            ms = now.getMilliseconds();
        return (h + ":" + m + ":" + s + " " + ms);
    }

    return {
        Ajax: commonAjax,
        GetTime: getTime
    }
}();