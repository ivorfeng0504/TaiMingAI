layui.use(['form', 'element', 'layer', 'jquery', 'laytpl'], function () {
    var element = layui.element;
    var form = layui.form;
    var layer = layui.layer;
    var $ = layui.$;
    var laytpl = layui.laytpl;
    /**
     * 加载导航菜单
     * @param {JSON} navbarJson:导航json数据
     */
    function loadNavbar(navbarJson, navbarDic) {
        if (!navbarJson) {
            navbarJson = $("#all_NavBarJson").val();
            navbarDic = $("#all_NavbarDic").val();
            if (!navbarJson) return;
        }
        if (typeof (navbarJson) === "string") {
            navbarJson = JSON.parse(navbarJson);
            navbarDic = JSON.parse(navbarDic);
        }
        if (navbarJson.lenght === 0) return;

        var ulHtml = '';
        $.each(navbarJson, function () {
            var item = this;
            item.json = JSON.stringify(item);
            if (item.children !== undefined && item.children.length > 0) {
                ulHtml += '<li class="layui-nav-item layui-nav-itemed">';
                if (item.icon !== undefined && item.icon !== '') {
                    ulHtml += laytpl(temp_navbar_a_icon.innerHTML).render(item);
                }
                else {
                    ulHtml += laytpl(temp_navbar_a.innerHTML).render(item);
                }
                ulHtml += getNavbarHtml(item.children);

            } else {
                ulHtml += '<li class="layui-nav-item">';
                if (item.icon !== undefined && item.icon !== '') {
                    ulHtml += laytpl(temp_navbar_a_icon.innerHTML).render(item);
                }
                else {
                    ulHtml += laytpl(temp_navbar_a.innerHTML).render(item);
                }
            }
            ulHtml += '</li>';
        });
        $("ul[lay-filter='navbar_list']").html(ulHtml);

        var option = '<option value="0">无</option>';
        $.each(navbarDic, function () {
            var dic = this;
            option += '<option value="' + dic.key + '">' + dic.value + '</option>';
        });
        $("form[lay-filter='formNavbar'] [name='ParentId']").html("").append(option);

        element.render('nav');
        form.render('select');
    }
    /**
     * 拼接导航菜单html
     * @param {JSON} childrenList:子集
     */
    function getNavbarHtml(childrenList) {
        var html = "";
        if (!childrenList) return html;
        html += '<dl class="layui-nav-child">'
        $.each(childrenList, function () {
            var children = this;
            children.json = JSON.stringify(children);

            if (children.children != undefined && children.children.length > 0) {
                html += '<dd class="layui-nav-itemed">';
                if (children.icon != undefined && children.icon != '') {
                    html += laytpl(temp_navbar_a_icon.innerHTML).render(children);
                }
                else {
                    html += laytpl(temp_navbar_a.innerHTML).render(children);
                }
                html += getNavbarHtml(children.children);
            }
            else {
                html += '<dd>';
                if (children.icon != undefined && children.icon != '') {
                    html += laytpl(temp_navbar_a_icon.innerHTML).render(children);
                }
                else {
                    html += laytpl(temp_navbar_a.innerHTML).render(children);
                }
            }
            html += '</dd>';
        });

        html += '</dl>'
        return html;
    }

    //提交数据
    form.on('submit(btnSubmit)', function (data) {
        data = data.field;
        data.spread = data.spread === "on" ? true : false;
        data.IsShow = data.IsShow === "on" ? true : false;
        data.target = data._target === "on" ? true : false;
        data.icon = !!data.icon ? escape(data.icon) : "";
        data.href = !!data.href ? encodeURI(data.href) : "";

        Common.Ajax("/Navbar/SubmitNavbar", data, function (result) {
            if (result.IsSuccess) {
                var arr = result.Message.split("$#$");
                loadNavbar(arr[1], arr[0])
                $("form[lay-filter='formNavbar']")[0].reset();
            }
            else {
                layer.msg(result.Message + result.ErrMessage);
            }
        })
        return false;
    });
    $("ul[lay-filter='navbar_list']").on("click", "a", function () {
        var data = $(this).data("json");
        data._target = data.target;
        form.val("formNavbar", data)
        return false;
    });

    loadNavbar();
});