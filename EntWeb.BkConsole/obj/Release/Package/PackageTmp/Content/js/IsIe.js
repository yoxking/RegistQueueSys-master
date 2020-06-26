
function isIe6() {
    // ie6是不支持window.XMLHttpRequest的
    return isIe() && !window.XMLHttpRequest;
}
function isIe7() {
    //只有IE8+才支持document.documentMode
    return isIe() && window.XMLHttpRequest && !document.documentMode;
}

function isIe8() {
    // alert(!-[1,])//->IE678返回NaN 所以!NaN为true 标准浏览器返回-1 所以!-1为false
    return isIe() && !-[1, ] && document.documentMode;
}
function isIe() {
    return ("ActiveXObject" in window);
}