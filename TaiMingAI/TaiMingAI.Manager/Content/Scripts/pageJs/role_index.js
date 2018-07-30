layui.use(['layer', 'table', 'form'], function () {
    var layer = layui.layer;
    var table = layui.table;
    var form = layui.form;
    var treeObj, lookTreeObj, zNodes;
    var setting = {
        check: {
            enable: true
        },
        data: {
            simpleData: {
                enable: true
            }
        },
        callback: {
            beforeCheck: beforeCheck
        }
    };

    function beforeCheck(treeId, treeNode) {
        return (treeNode.doCheck !== false);
    }
    /**
     * 加载zTree
     */
    function loadzTree() {
        zNodes = $("#zTreeJson").val();
        if (!!zNodes) {
            zNodes = JSON.parse(zNodes);
        }
        treeObj = $.fn.zTree.init($("#treeLimits"), setting, zNodes);
        $.each(zNodes, function () {
            var node = this;
            node.doCheck = false;
        })
        lookTreeObj = $.fn.zTree.init($("#look_navbar_zTree"), setting, zNodes);
    }
    /**
     *查看权限
     * @param {object} data：权限
     */
    function lookzTree(data) {
        layer.open({
            title: "查看权限",
            type: 1,
            area: "300px",
            content: $("#look_navbar_page"),
            closeBtn: 2,
            shadeClose: true,
            offset: "100px",
            success: function (layero, index) {
                lookTreeObj.checkAllNodes(false);
                if (!!data) {
                    for (var i = 0; i < data.length; i++) {
                        var id = Number(data[i]);
                        if (!isNaN(id) && id !== 0) {
                            var node = lookTreeObj.getNodeByParam("id", id);
                            if (!!node) {
                                lookTreeObj.checkNode(node, true, false);
                            }
                        }
                    }
                }
            }
        });
    }

    /**
     * 编辑角色弹窗
     * @param {String} title：标题
     * @param {Object} data:数据；可为空
     */
    function openEditRolePage(title, data) {
        layer.open({
            title: title,
            type: 1,
            content: $("#role_edit_page"),
            area: "550px",
            cancel: function (index, layero) {
                layero.find("form")[0].reset()
            },
            success: function (layero, index) {
                treeObj.checkAllNodes(false);
                if (!!data) {
                    form.val("role_edit_form", data);
                    if (!!data.Limits) {
                        var limits = data.Limits.split(",");

                        for (var i = 0; i < limits.length; i++) {
                            var id = Number(limits[i]);
                            if (!isNaN(id) && id !== 0) {
                                var node = treeObj.getNodeByParam("id", id);
                                if (!!node) {
                                    treeObj.checkNode(node, true, false);
                                }
                            }
                        }
                    }
                }
            }
        });
    }

    //加载角色列表
    table.render({
        elem: '#role_list' //指定原始表格元素选择器（推荐id选择器）
        , cols: [[
            { type: "numbers", width: 50, fixed: "left" }
            , { field: 'Name', title: '角色名称', width: 180, fixed: "left" }
            //, { field: 'Limits', title: '权限范围' }
            , { field: 'Description', title: '角色描述' }
            , { field: 'IsUseStr', title: '状态', width: 80 }
            , { field: 'UpdateTimeStr', title: '更新时间', width: 200 }
            , { toolbar: '#role_list_toolbar', title: '操作', width: 350, fixed: "right" }
        ]]
        , url: "/Role/GetRoleList"
        , method: "POST"
    });

    //监听工具条
    table.on('tool(role_table)', function (obj) {
        var data = obj.data; //获得当前行数据
        var layEvent = obj.event; //获得 lay-event 对应的值（也可以是表头的 event 参数对应的值）
        var tr = obj.tr; //获得当前行 tr 的DOM对象

        if (layEvent === 'del') { //删除
            layer.confirm('真的删除行么', function (index) {
                //删除对应行（tr）的DOM结构，并更新缓存
                obj.del();
                layer.close(index);
                //向服务端发送删除指令
            });
        } else if (layEvent === 'edit') {
            openEditRolePage("编辑角色", data);
        }
        else if (layEvent === "look") {
            if (!data.Limits) return false;
            var limits = data.Limits.split(",");
            lookzTree(limits);
        }
        return true;
    });

    //提交角色信息
    form.on('submit(role_submit_btn)', function (data) {
        data = data.field;
        data.IsUse = data.IsUse === "on" ? true : false;
        var nodes = treeObj.getCheckedNodes(true);
        var ids = "";
        if (!!nodes && nodes.length > 0) {
            $.each(nodes, function () {
                ids += this.id + ",";
            });
            ids = ids.substring(0, ids.length - 1);
        }
        data.Limits = ids;
        Common.Ajax("/Role/SubmitRole", data, function (result) {
            var msg = (!!result.Message ? result.Message : "") + (!!result.ErrMessage ? result.ErrMessage : "")
            if (!!msg) {
                layer.msg(msg);
            }
            if (result.IsSuccess) {
                $("form[lay-filter='role_edit_form']")[0].reset();
                layer.closeAll('page');
                table.reload("role_list");
            }
        })
        return false;
    });

    //新增角色
    $("#role_add_btn").click(function () {
        openEditRolePage("新增角色");
    });
    //重置表单
    $("#role_edit_form_reset").click(function () {
        var id = $("#role_edit_page form [name='Id']").val();
        $("form[lay-filter='role_edit_form']")[0].reset();
        treeObj.checkAllNodes(false);
        $("#role_edit_page form [name='Id']").val(id);
        return false;
    });
    loadzTree();
});