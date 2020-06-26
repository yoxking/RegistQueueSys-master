/**
 * 分页
 */

function goNavigUrl(obj) {
    location.href = obj;
}

function firstPage() {
    var curPage = $('#pageIndex').attr('value');
    if (curPage > 1) {
        $('#pageIndex').attr('value', 1);
        $('#pageForm').submit();
    }
}
function lastPage() {
    var pageCount = $("#pageCount").attr('value');
    var curPage = $('#pageIndex').attr('value');
    if (curPage < pageCount) {
        $('#pageIndex').attr('value', pageCount);
        $('#pageForm').submit();
    }
}
function previousPage() {
    var curPage = $('#pageIndex').attr('value');
    var pageCount = $("#pageCount").attr('value');
    if (curPage > 1) {
        curPage = parseInt(curPage) - 1;
        $('#pageIndex').attr('value', curPage);
        $('#pageForm').submit();
    }
}
function nextPage() {
    var curPage = $('#pageIndex').attr('value');
    var pageCount = $("#pageCount").attr('value');

    if (parseInt(curPage) < parseInt(pageCount)) {
        curPage = parseInt(curPage) + 1;
        $('#pageIndex').attr('value', curPage);
        $('#pageForm').submit();
    }
}
function goPage(index) {
    var curPage = $('#pageIndex').attr('value');
    if (index != curPage) {
        var pageCount = $("#pageCount").attr('value');
        if (parseInt(index) > 0 && parseInt(index) <= parseInt(pageCount)) {
            curPage = index;
        }
        $('#pageIndex').attr('value', curPage);
        $('#pageForm').submit();
    }
}

 