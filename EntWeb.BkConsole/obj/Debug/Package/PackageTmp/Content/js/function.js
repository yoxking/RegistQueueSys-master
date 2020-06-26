///*
//* Author: huafg
//* Date: 2015-10-30
//* Desc: 自定义JS工具公共类库
//*/
//时间
laydate.skin('molv');
//时间选择器
function ldate(obj, format) {
    if (format == '') { format = "YYYY-MM-DD"; }
    var de = {
        elem: '#' + obj,
        format: format,
        min: '1900-01-01', //设定最小日期为当前日期
        max: '2099-06-16 23:59:59', //最大日期
        istime: true,
        istoday: false,
        choose: function (datas) {
            //end.min = datas; //开始日选好后，重置结束日的最小日期
            //end.start = datas //将结束日的初始值设定为开始日
        }
    };
    laydate(de);
}

/*时间判断，DateOne是开始时间,DateTwo结束时间，根据情况判断*/
function compareDate(DateOne, DateTwo) {
    var OneMonth = DateOne.substring(5, DateOne.lastIndexOf("-"));
    var OneDay = DateOne.substring(DateOne.length, DateOne.lastIndexOf("-") + 1);
    var OneYear = DateOne.substring(0, DateOne.indexOf("-"));

    var TwoMonth = DateTwo.substring(5, DateTwo.lastIndexOf("-"));
    var TwoDay = DateTwo.substring(DateTwo.length, DateTwo.lastIndexOf("-") + 1);
    var TwoYear = DateTwo.substring(0, DateTwo.indexOf("-"));

    if (Date.parse(OneMonth + "/" + OneDay + "/" + OneYear) <= Date.parse(TwoMonth + "/" + TwoDay + "/" + TwoYear))
        return true;
    else
        return false;
}
//提交表单
function submit() {
    document.forms[0].submit();
}

//增删改提交ajax
var SubAjax = {
    Loading: function () {
        $(".btn-save").attr("disabled", "disabled").find("span").html("正在保存中...")
    },
    Success: function (result) {
        if (result.Status == undefined) {
            document.writeln(result);
        } else if (result.Status == "Success") {
            dig.alertSuccess("提示", result.Message, function () {
                var dialog = top.dialog.get(window);
                dialog.close('Success').remove();
            });
        } else {
            dig.alertError("错误提示：", result.Message);
            SubAjax.Complete();
        }
    },
    SuccessBack: function (result) {
        if (result.Status == "Success") {
            dig.alertSuccess("提示", result.Message, function () {
                history.go(-1);
            });
        } else {
            dig.alertError("错误提示：", result.Message);
            SubAjax.Complete();
        }
    },
    Failure: function () {
        dig.alertError("错误提示：", "网络超时,请稍后再试...");
        SubAjax.Complete();
    },
    Complete: function () {
        $(".btn-save").attr("disabled", false).find("span").html("确定保存");
    }
};


$(function () {
    //全选 反选
    $('input[name="checkall"]').change(function () {
        if ($(this).prop("checked")) {
            $("input[name='checkbox_name']:checkbox,input[name='checkall']:checkbox").each(function () {
                if ($(this).prop("checked") == false) {
                    $(this).prop("checked", true);
                }
            });
        }
        else {
            $("input[name='checkbox_name']:checkbox,input[name='checkall']:checkbox").each(function () {
                if ($(this).prop("checked") == true) {
                    $(this).prop("checked", false);
                }
            });
        }
    });
    //单机行，选中复选框
    $("#example tr").slice(1).each(function (g) {
        var p = this;
        $(this).children().slice(1).click(function () {
            $($(p).children()[0]).children().each(function () {
                if (this.type == "checkbox") {
                    if (!this.checked) {
                        this.checked = true;
                    } else {
                        this.checked = false;
                    }
                }
            });
        });
    });
    //单机行，选中单选框
    $("#example tr").slice(1).each(function (g) {
        var p = this;
        $(this).children().slice(1).click(function () {
            $($(p).children()[0]).children().each(function () {
                if (this.type == "radio") {
                    if (!this.checked) {
                        this.checked = true;
                    } else {
                        this.checked = false;
                    }
                }
            });
        });
    });

    //使用col插件实现表格头宽度拖拽
    $(".table").colResizable();
    //返回
    $("#btn-back").click(function () {
        history.go(-1);
    });
    //关闭弹窗
    $("#btn-dig-close").click(function () {
        dig.remove();
    });
    //列表选择删除
    $('#delete').click(function () {
        var vals = '';
        $('input[name="checkbox_name"]:checked').each(function () {
            vals += $(this).val() + ',';
        });
        if (vals == '' || vals == ',') {
            dig.alertError("提示", "对不起，请选中您要操作的记录！");
            return;
        }
        var url = window.location.href.split('?')[0].toLowerCase();
        if (url.lastIndexOf('/index') > 0) {
            url = url.substring(0, url.indexOf('/index'));
        }
        url = url + '/Delete';
        var msg = "删除记录后不可恢复，您确定吗？";
        dig.confirm("删除确认", msg, function () {
            $.post(url, { idList: vals }, function (res) {
                if (res.Status == "y") {
                    dig.alertSuccess('提示', '删除成功', function () {
                        window.location.reload();
                    });
                }
                else {
                    dig.alertError("提示", res.Msg);
                }
            }, "json");
        });
    });
}); 