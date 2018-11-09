layui.use(['layer', 'table', 'form', 'jquery'], function () {
    var layer = layui.layer;
    var table = layui.table;
    var form = layui.form;
    var $ = layui.$;
    var roleId, roleName;
    /**
     * 验证角色
     */
    function checkRole() {
        roleId = "";
        roleName = "";

        $.each($("#admin_edit_page [lay-filter='role_ckb']:checked"), function () {
            $ckb = $(this);
            roleId += "," + $ckb.val();
            roleName += "|" + $ckb.attr("title");
        });

        if (roleId.length > 0 && roleId.substring(0, 1) === ",") {
            roleId = roleId.substring(1, roleId.length);
            roleName = roleName.substring(1, roleName.length);
        }

        if (!!roleId) {
            $("#admin_edit_page form [name='role_mag']").hide();
            return true;
        }
        $("#admin_edit_page form [name='role_mag']").show();
        return false;
    }
    /**
     * 编辑角色弹窗
     * @param {String} title：标题
     * @param {Object} data:数据；可为空
     */
    function openEditAdminPage(title, data) {
        layer.open({
            title: title,
            type: 1,
            content: $("#admin_edit_page"),
            area: "550px",
            cancel: function (index, layero) {
                layero.find("form[lay-filter='admin_edit_form']")[0].reset();
            },
            success: function (layero, index) {
                layero.find("form[lay-filter='admin_edit_form'] [name='Password']").parent().parent().show();
                if (!!data) {
                    layero.find("form[lay-filter='admin_edit_form'] [name='Password']").parent().parent().hide();
                    if (!!data.Role) {
                        var arrRole = data.Role.split(",");
                        $.each(arrRole, function () {
                            var role = this;
                            var key = "Role[" + role + "]"
                            data[key] = true;
                        });
                        data.Sex = data.Sex.toString()
                    }
                    form.val("admin_edit_form", data);
                }
            }
        });
    }
    /**
     * 重置管理员密码
     * @param {int} id:用户ID
     * @param {String} loginName：登录名
     */
    function openResetAdminPage(id, loginName) {
        $("form[lay-filter='admin_reset_form'] [name='reset_mag']").css("color", "#999");
        $("form[lay-filter='admin_reset_form'] [name='reset_mag'] span").html("密码必须6到12位，且不能出现空格");
        layer.open({
            title: "重置[" + loginName + "]密码",
            area: "350px",
            type: 1,
            content: $("#admin_reset_page"),
            btn: ["重置", "取消"],
            yes: function (index, layero) {
                var pass = $("form[lay-filter='admin_reset_form'] [name='Password']:visible").val();
                var rePass = $("form[lay-filter='admin_reset_form'] [name='RePassword']:visible").val();
                var reg = /^[\S]{6,12}$/;
                if (!reg.test(pass)) {
                    $("form[lay-filter='admin_reset_form'] [name='reset_mag']").css("color", "#FF5722");
                    $("form[lay-filter='admin_reset_form'] [name='reset_mag'] span").html("密码必须6到12位，且不能出现空格");
                    return false;
                }
                if (pass !== rePass) {
                    $("form[lay-filter='admin_reset_form'] [name='reset_mag']").css("color", "#FF5722");
                    $("form[lay-filter='admin_reset_form'] [name='reset_mag'] span").html("两次密码输入不一致");
                    return false;
                }
                var data = {
                    dto: {
                        Id: id,
                        Password: pass
                    },
                    property: 1
                }
                Common.Ajax("/Administrator/UpdateAdminProperty", data, function (result) {
                    var msg = (!!result.Message ? result.Message : "") + (!!result.ErrMessage ? result.ErrMessage : "")
                    if (!!msg) {
                        layer.msg(msg);
                    }
                    if (result.IsSuccess) {
                        layero.find("form[lay-filter='admin_reset_form']")[0].reset();
                        layer.closeAll('page');
                        table.reload("admin_list");
                    }
                })
            },
            btn2: function (index, layero) {
                layero.find("form[lay-filter='admin_reset_form']")[0].reset();
            },
            cancel: function (index, layero) {
                layero.find("form[lay-filter='admin_reset_form']")[0].reset();
            }
        });
    }

    //加载角色列表
    table.render({
        elem: '#admin_list' //指定原始表格元素选择器（推荐id选择器）
        , cols: [[
            { field: 'Id', title: "ID", width: 50, fixed: "left" }
            , { field: 'LoginName', title: '登录名', width: 150, fixed: "left" }
            , { field: 'NickName', title: '昵称', width: 180 }
            , { field: 'Mobile', title: '手机号码', width: 150 }
            , { field: 'Email', title: '邮箱', width: 180 }
            , { field: 'RoleName', title: '角色' }
            , {
                field: 'StateStr', title: '状态', width: 100, templet: function (d) {
                    switch (d.State) {
                        case -1: return '<button class="layui-btn layui-btn-danger layui-btn-xs">' + d.StateStr + '</button>';
                        case 0: return '<button class="layui-btn layui-btn-primary layui-btn-xs">' + d.StateStr + '</button>';
                        case 1: return '<button class="layui-btn layui-btn-xs">' + d.StateStr + '</button>';
                        default: return "--";
                    }
                }
            }
            , { field: 'UpdateTimeStr', title: '更新时间', width: 200 }
            , { toolbar: '#admin_list_toolbar', title: '操作', width: 300, fixed: "right" }
        ]]
        , url: "/Administrator/GetAdminList"
        , method: "POST"
    });

    //监听工具条
    table.on('tool(admin_table)', function (obj) {
        var data = obj.data;
        var layEvent = obj.event;
        if (layEvent === "reset") {
            //重置密码
            openResetAdminPage(data.Id, data.LoginName);
        } else if (layEvent === 'edit') {
            //编辑
            openEditAdminPage("编辑管理员", data);
        }
        else if (layEvent === 'logout') {
            //注销
            layer.confirm('真的注销该管理员吗？', function (index) {
                layer.close(index);
                data = {
                    dto: {
                        Id: obj.data.Id,
                        State: -1
                    },
                    property: 2
                };

                Common.Ajax("/Administrator/UpdateAdminProperty", data, function (result) {
                    var msg = (!!result.Message ? result.Message : "") + (!!result.ErrMessage ? result.ErrMessage : "")
                    if (!!msg) {
                        layer.msg(msg);
                    }
                    if (result.IsSuccess) {
                        layer.closeAll('page');
                        table.reload("admin_list");
                    }
                })
            });
        }
        return true;
    });

    //提交角色信息
    form.on('submit(admin_submit_btn)', function (data) {
        if (!checkRole()) {
            return false;
        }

        data = data.field;
        data.State = data.State === "on" ? 1 : 0;
        data.Role = roleId;
        data.RoleName = roleName;

        Common.Ajax("/Administrator/SubmitAdmin", data, function (result) {
            var msg = (!!result.Message ? result.Message : "") + (!!result.ErrMessage ? result.ErrMessage : "")
            if (!!msg) {
                layer.msg(msg);
            }
            if (result.IsSuccess) {
                $("form[lay-filter='admin_edit_form']")[0].reset();
                layer.closeAll('page');
                table.reload("admin_list");
            }
        })
        return false;
    });

    //表单验证
    form.verify({
        LoginName: function (value, item) { //value：表单的值、item：表单的DOM对象
            if (!new RegExp("^[a-zA-Z0-9_]+$").test(value)) {
                return '登录名不能有特殊字符';
            }
            if (/(^\_)|(\__)|(\_+$)/.test(value)) {
                return '登录名首尾不能出现下划线\'_\'';
            }
            if (/^\d+\d+\d$/.test(value)) {
                return '登录名不能全为数字';
            }
            if (value.length < 5) {
                return "登录名不能少于5个字符"
            }
        }
    });
    //新增管理员
    $("#admin_add_btn").click(function () {
        openEditAdminPage("新增管理员");
    });
    //重置表单
    $("#admin_edit_form_reset").click(function () {
        var id = $("#admin_edit_page form [name='Id']").val();
        $("form[lay-filter='admin_edit_form']")[0].reset();
        $("#admin_edit_page form [name='Id']").val(id);
        return false;
    });
});