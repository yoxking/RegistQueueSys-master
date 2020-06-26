var f = {
    signUpFile: function (upInput) {
        var isImg = $('input[name="filestype"]:checked').val();
        var subUrl = "/Com/Upload/SignUpFile?isThum=0&fileUp=fileUrl&isImg=" + (isImg == "0" ? true : false);
        $("#forms").ajaxSubmit({
            beforeSubmit: function () {
                $(".sign-up").attr("disabled", "disabled").html(" 正在上传...");
            },
            success: function (data) {
                if (data.Status == "y") {
                    var res = eval('(' + data.Data + ')');
                    $('#fileUrl').val(res.path);
                    $('#jsons').val(data.Data);
                    //定位
                    var pa = res.path.substring(0, res.path.lastIndexOf('/') + 1);
                    $('#path').val(pa);
                    initFiles(pa)
                } else {
                    dig.alertError("消息", data.Msg);
                }
                $(".sign-up").attr("disabled", false).html(" 单文件上传");
                $("#fileUp").unbind();
            },
            error: function (e) {
                $(".sign-up").attr("disabled", false).html(" 单文件上传");
                console.log(e);
            },
            url: subUrl,
            type: "post",
            dataType: "json",
            timeout: 600000
        });
    }
}
function delFolder(p, fold) {
    dig.confim("删除提示", "确定要删除选择的文件吗？", function () {
        var ajaxUrl = "/Com/Upload/DeleteBy";
        var isFile = (fold == "文件夹" ? 0 : 1);
        $.post(ajaxUrl, { path: $("#path").val() + p, isfile: isFile }, function (res) {
            if (res.Status == "y")
                initFiles($("#path").val());
            else {
                dig.alertError("提示", res.Msg);
            }
        }, "json");
    });
}
//预览
function loadFile(f) {
    var path = $('#path').val() + f;
    top.dialog({
        title: '预览图片',
        url: '/Com/Upload/Show',
        width: 500,
        height: 450,
        data: path, // 给 iframe 的数据
        onclose: function () {
        },
        oniframeload: function () {
        }
    }).showModal();
    return false;
}

function OpenParentFolder() {
    var p = $("#path").val();
    if (p == spath) return;
    p = p.substring(0, p.lastIndexOf('/'));
    p = p.substring(0, p.lastIndexOf('/')) + "/";
    $("#path").val(p);
    initFiles(p);
}

function OpenFolder(p) {
    var npath = $("#path").val() + p + "/";
    $("#path").val(npath);
    initFiles(npath);
}

function SetFile(f) {
    $("#fileUrl").val($("#path").val() + f);
    $('#jsons').val('{"path":"' + $("#path").val() + f + '"}');
}

function initFiles(path) {
    if (path == spath) {
        $("#fback").addClass("hidden");
    } else {
        $("#fback").removeClass("hidden");
    }
    var collSum = 5, tbody = $("#tables > tbody"), tdata = $("#tlist");
    $.post("/Com/Upload/GetFileData", { path: path }, function (res) {
        if (res.Status == "y") {
            if (res.Data == "" || res.Data == null) {
                dig.alertError("提示", "该目录下没有文件了！");
                OpenParentFolder();
            } else {
                tbody.empty();
                tdata.tmpl(res.Data).appendTo('#trows');
                $(".table").colResizable();

                var ext = ',.jpg.jpeg.bmp.gif.png';
                $('.img-show').each(function () {
                    if (ext.indexOf($(this).attr('data-ext').toLowerCase()) > 0) {
                        $(this).html('预览');
                    }
                });
            }
        } else {
            dig.alertError("错误提示", res.Msg);
        }
    }, "json");
}

var spath = "/upload/";
$(function () {
    //获取IFrame传递过来的值
    var dialog = top.dialog.get(window);
    var data = dialog.data;
    $("#fileUrl").val(data);
    if (data != '' && data != undefined) {
        //文件定位
        var p = data.substring(0, data.lastIndexOf('/') + 1);
        $('#path').val(p);
        $('#jsons').val('{"path":"' + data + '"}');
    }
    initFiles($("#path").val());
    //保存
    $(".fsave").click(function () {
        dialog.close($("#jsons").val());
        dialog.remove();
    });
    //返回上级
    $("#fback > button").click(function () {
        OpenParentFolder();
    });
    //关闭
    $(".fclose").click(function () {
        dig.remove();
    });
    //上传
    $(".sign-up").click(function () {
        $("#fileUp").unbind();
        $("#fileUp").click();
        //$("#fileUp").change(function () {
        //    f.signUpFile("");
        //});
    });
});