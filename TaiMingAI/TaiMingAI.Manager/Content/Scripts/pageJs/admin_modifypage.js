layui.use(['layer', 'table', 'form', 'jquery'], function () {
    var layer = layui.layer;
    var table = layui.table;
    var form = layui.form;
    var $ = layui.$;
    var roleId, roleName;

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
   
    //重置表单
    $("#admin_edit_form_reset").click(function () {
        var id = $("#admin_edit_page form [name='Id']").val();
        $("form[lay-filter='admin_edit_form']")[0].reset();
        $("#admin_edit_page form [name='Id']").val(id);
        return false;
    });
});