
function GetDate(n, t) {
    if (n == null) return "";
    var i = new Date(parseInt(n.replace("/Date(", "").replace(")/", ""), 10));
    return i.format(t)
}
var dig, dateFormat; !
function () {
    function t(n) {
        var r = i[n],
        u = "exports";
        return "object" == typeof r ? r : (r[u] || (r[u] = {},
        r[u] = r.call(r[u], t, r[u], r) || r[u]), r[u])
    }
    function n(n, t) {
        i[n] = t
    }
    var i = {};
    n("jquery",
    function () {
        return jQuery
    });
    n("popup",
    function (n) {
        function i() {
            this.destroyed = !1;
            this.__popup = t("<div />").css({
                display: "none",
                position: "absolute",
                outline: 0
            }).attr("tabindex", "-1").html(this.innerHTML).appendTo("body");
            this.__backdrop = this.__mask = t("<div />").css({
                opacity: .5,
                background: "#000"
            });
            this.node = this.__popup[0];
            this.backdrop = this.__backdrop[0];
            u++
        }
        var t = n("jquery"),
        u = 0,
        r = !("minWidth" in t("html")[0].style),
        f = !r;
        return t.extend(i.prototype, {
            node: null,
            backdrop: null,
            fixed: !1,
            destroyed: !0,
            open: !1,
            returnValue: "",
            autofocus: !0,
            align: "bottom left",
            innerHTML: "",
            className: "ui-popup",
            show: function (n) {
                var u, e, o;
                return this.destroyed ? this : (u = this.__popup, e = this.__backdrop, (this.__activeElement = this.__getActive(), this.open = !0, this.follow = n || this.follow, this.__ready) || ((u.addClass(this.className).attr("role", this.modal ? "alertdialog" : "dialog").css("position", this.fixed ? "fixed" : "absolute"), r || t(window).on("resize", t.proxy(this.reset, this)), this.modal) && (o = {
                    position: "fixed",
                    left: 0,
                    top: 0,
                    width: "100%",
                    height: "100%",
                    overflow: "hidden",
                    userSelect: "none",
                    zIndex: this.zIndex || i.zIndex
                },
                u.addClass(this.className + "-modal"), f || t.extend(o, {
                    position: "absolute",
                    width: t(window).width() + "px",
                    height: t(document).height() + "px"
                }), e.css(o).attr({
                    tabindex: "0"
                }).on("focus", t.proxy(this.focus, this)), this.__mask = e.clone(!0).attr("style", "").insertAfter(u), e.addClass(this.className + "-backdrop").insertBefore(u), this.__ready = !0), u.html() || u.html(this.innerHTML)), u.addClass(this.className + "-show").show(), e.show(), this.reset().focus(), this.__dispatchEvent("show"), this)
            },
            showModal: function () {
                return this.modal = !0,
                this.show.apply(this, arguments)
            },
            close: function (n) {
                return !this.destroyed && this.open && (void 0 !== n && (this.returnValue = n), this.__popup.hide().removeClass(this.className + "-show"), this.__backdrop.hide(), this.open = !1, this.blur(), this.__dispatchEvent("close")),
                this
            },
            remove: function () {
                if (this.destroyed) return this;
                this.__dispatchEvent("beforeremove");
                i.current === this && (i.current = null);
                this.__popup.remove();
                this.__backdrop.remove();
                this.__mask.remove();
                r || t(window).off("resize", this.reset);
                this.__dispatchEvent("remove");
                for (var n in this) delete this[n];
                return this
            },
            reset: function () {
                var n = this.follow;
                return n ? this.__follow(n) : this.__center(),
                this.__dispatchEvent("reset"),
                this
            },
            focus: function () {
                var f = this.node,
                r = this.__popup,
                u = i.current,
                e = this.zIndex = i.zIndex++,
                n;
                return (u && u !== this && u.blur(!1), t.contains(f, this.__getActive())) || (n = r.find("[autofocus]")[0], !this._autofocus && n ? this._autofocus = !0 : n = f, this.__focus(n)),
                r.css("zIndex", e),
                i.current = this,
                r.addClass(this.className + "-focus"),
                this.__dispatchEvent("focus"),
                this
            },
            blur: function () {
                var n = this.__activeElement,
                t = arguments[0];
                return t !== !1 && this.__focus(n),
                this._autofocus = !1,
                this.__popup.removeClass(this.className + "-focus"),
                this.__dispatchEvent("blur"),
                this
            },
            addEventListener: function (n, t) {
                return this.__getEventListener(n).push(t),
                this
            },
            removeEventListener: function (n, t) {
                for (var r = this.__getEventListener(n), i = 0; i < r.length; i++) t === r[i] && r.splice(i--, 1);
                return this
            },
            __getEventListener: function (n) {
                var t = this.__listener;
                return t || (t = this.__listener = {}),
                t[n] || (t[n] = []),
                t[n]
            },
            __dispatchEvent: function (n) {
                var i = this.__getEventListener(n),
                t;
                for (this["on" + n] && this["on" + n](), t = 0; t < i.length; t++) i[t].call(this)
            },
            __focus: function (n) {
                try {
                    this.autofocus && !/^iframe$/i.test(n.nodeName) && n.focus()
                } catch (t) { }
            },
            __getActive: function () {
                try {
                    var n = document.activeElement,
                    t = n.contentDocument;
                    return t && t.activeElement || n
                } catch (r) { }
            },
            __center: function () {
                var n = this.__popup,
                i = t(window),
                r = t(document),
                u = this.fixed,
                f = u ? 0 : r.scrollLeft(),
                e = u ? 0 : r.scrollTop(),
                s = i.width(),
                h = i.height(),
                c = n.width(),
                l = n.height(),
                a = (s - c) / 2 + f,
                v = 382 * (h - l) / 1e3 + e,
                o = n[0].style;
                o.left = Math.max(parseInt(a), f) + "px";
                o.top = Math.max(parseInt(v), e) + "px"
            },
            __follow: function (n) {
                var r = n.parentNode && t(n),
                f = this.__popup,
                a;
                if ((this.__followSkin && f.removeClass(this.__followSkin), r) && (a = r.offset(), a.left * a.top < 0)) return this.__center();
                var ot = this,
                h = this.fixed,
                b = t(window),
                k = t(document),
                st = b.width(),
                ht = b.height(),
                d = k.scrollLeft(),
                g = k.scrollTop(),
                c = f.width(),
                l = f.height(),
                v = r ? r.outerWidth() : 0,
                y = r ? r.outerHeight() : 0,
                nt = this.__offset(n),
                tt = nt.left,
                it = nt.top,
                e = h ? tt - d : tt,
                o = h ? it - g : it,
                rt = h ? 0 : d,
                ut = h ? 0 : g,
                ct = rt + st - c,
                lt = ut + ht - l,
                p = {},
                i = this.align.split(" "),
                w = this.className + "-",
                ft = {
                    top: "bottom",
                    bottom: "top",
                    left: "right",
                    right: "left"
                },
                u = {
                    top: "top",
                    bottom: "top",
                    left: "left",
                    right: "left"
                },
                s = [{
                    top: o - l,
                    bottom: o + y,
                    left: e - c,
                    right: e + v
                },
                {
                    top: o,
                    bottom: o - l + y,
                    left: e,
                    right: e - c + v
                }],
                at = {
                    left: e + v / 2 - c / 2,
                    top: o + y / 2 - l / 2
                },
                et = {
                    left: [rt, ct],
                    top: [ut, lt]
                };
                t.each(i,
                function (n, t) {
                    s[n][t] > et[u[t]][1] && (t = i[n] = ft[t]);
                    s[n][t] < et[u[t]][0] && (i[n] = ft[t])
                });
                i[1] || (u[i[1]] = "left" === u[i[0]] ? "top" : "left", s[1][i[1]] = at[u[i[1]]]);
                w += i.join("-") + " " + this.className + "-follow";
                ot.__followSkin = w;
                r && f.addClass(w);
                p[u[i[0]]] = parseInt(s[0][i[0]]);
                p[u[i[1]]] = parseInt(s[1][i[1]]);
                f.css(p)
            },
            __offset: function (n) {
                var f = n.parentNode,
                r = f ? t(n).offset() : {
                    left: n.pageX,
                    top: n.pageY
                },
                i,
                u;
                if (n = f ? n : n.target, i = n.ownerDocument, u = i.defaultView || i.parentWindow, u == window) return r;
                var s = u.frameElement,
                e = t(i),
                h = e.scrollLeft(),
                c = e.scrollTop(),
                o = t(s).offset(),
                l = o.left,
                a = o.top;
                return {
                    left: r.left + l - h,
                    top: r.top + a - c
                }
            }
        }),
        i.zIndex = 1024,
        i.current = null,
        i
    });
    n("dialog-config", {
        backdropBackground: "#000",
        backdropOpacity: .5,
        content: '<span class="ui-dialog-loading">Loading..<\/span>',
        title: "",
        statusbar: "",
        button: null,
        ok: null,
        cancel: null,
        okValue: "ok",
        cancelValue: "cancel",
        cancelDisplay: !0,
        width: "",
        height: "",
        padding: "",
        skin: "",
        quickClose: !1,
        cssUri: "../css/ui-dialog.css",
        innerHTML: '<div i="dialog" class="ui-dialog"><div class="ui-dialog-arrow-a"><\/div><div class="ui-dialog-arrow-b"><\/div><div class="morph-shape"><svg width="100%" height="100%" viewBox="0 0 560 280" preserveAspectRatio="none"><rect x="3" y="3" fill="none" width="556" height="276"></rect></svg></div><table class="ui-dialog-grid"><tr><td i="header" class="ui-dialog-header"><button i="close" class="ui-dialog-close">&#215;<\/button><div i="title" class="ui-dialog-title"><\/div><\/td><\/tr><tr><td i="body" class="ui-dialog-body"><div i="content" class="ui-dialog-content"><\/div><\/td><\/tr><tr><td i="footer" class="ui-dialog-footer"><div i="statusbar" class="ui-dialog-statusbar"><\/div><div i="button" class="ui-dialog-button"><\/div><\/td><\/tr><\/table><\/div>'
    });
    n("dialog",
    function (n) {
        var t = n("jquery"),
        u = n("popup"),
        o = n("dialog-config"),
        r = o.cssUri,
        f,
        e;
        r && (f = n[n.toUrl ? "toUrl" : "resolve"], f && (r = f(r), r = '<link rel="stylesheet" href="' + r + '" />', t("base")[0] ? t("base").before(r) : t("head").append(r)));
        var s = 0,
        l = new Date - 0,
        a = !("minWidth" in t("html")[0].style),
        h = "createTouch" in document && !("onmousemove" in document) || /(iPhone|iPad|iPod)/i.test(navigator.userAgent),
        v = !a && !h,
        i = function (n, r, u) {
            var o = n = n || {},
            f, e;
            return ("string" == typeof n || 1 === n.nodeType) && (n = {
                content: n,
                fixed: !h
            }),
            n = t.extend(!0, {},
            i.defaults, n),
            n.original = o,
            f = n.id = n.id || l + s,
            e = i.get(f),
            e ? e.focus() : (v || (n.fixed = !1), n.quickClose && (n.modal = !0, n.backdropOpacity = 0), t.isArray(n.button) || (n.button = []), void 0 !== u && (n.cancel = u), n.cancel && n.button.push({
                id: "cancel",
                value: n.cancelValue,
                callback: n.cancel,
                display: n.cancelDisplay
            }), void 0 !== r && (n.ok = r), n.ok && n.button.push({
                id: "ok",
                value: n.okValue,
                callback: n.ok,
                autofocus: !0
            }), i.list[f] = new i.create(n))
        },
        c = function () { };
        return c.prototype = u.prototype,
        e = i.prototype = new c,
        i.create = function (n) {
            var r = this,
            f, e;
            return t.extend(this, new u),
            f = (n.original, t(this.node).html(n.innerHTML)),
            e = t(this.backdrop),
            this.options = n,
            this._popup = f,
            t.each(n,
            function (n, t) {
                "function" == typeof r[n] ? r[n](t) : r[n] = t
            }),
            n.zIndex && (u.zIndex = n.zIndex),
            f.attr({
                "aria-labelledby": this._$("title").attr("id", "title:" + this.id).attr("id"),
                "aria-describedby": this._$("content").attr("id", "content:" + this.id).attr("id")
            }),
            this._$("close").css("display", this.cancel === !1 ? "none" : "").attr("title", this.cancelValue).on("click",
            function (n) {
                r._trigger("cancel");
                n.preventDefault()
            }),
            this._$("dialog").addClass(this.skin),
            this._$("body").css("padding", this.padding),
            n.quickClose && e.on("onmousedown" in document ? "mousedown" : "click",
            function () {
                return r._trigger("cancel"),
                !1
            }),
            this.addEventListener("show",
            function () {
                e.css({
                    opacity: 0,
                    background: n.backdropBackground
                }).animate({
                    opacity: n.backdropOpacity
                },
                150)
            }),
            this._esc = function (n) {
                var t = n.target,
                i = t.nodeName,
                f = u.current === r,
                e = n.keyCode; !f || /^input|textarea$/i.test(i) && "button" !== t.type || 27 === e && r._trigger("cancel")
            },
            t(document).on("keydown", this._esc),
            this.addEventListener("remove",
            function () {
                t(document).off("keydown", this._esc);
                delete i.list[this.id]
            }),
            s++,
            i.oncreate(this),
            this
        },
        i.create.prototype = e,
        t.extend(e, {
            content: function (n) {
                var i = this._$("content");
                return "object" == typeof n ? (n = t(n), i.empty("").append(n.show()), this.addEventListener("beforeremove",
                function () {
                    t("body").append(n.hide())
                })) : i.html(n),
                this.reset()
            },
            title: function (n) {
                return this._$("title").text(n),
                this._$("header")[n ? "show" : "hide"](),
                this
            },
            width: function (n) {
                return this._$("content").css("width", n),
                this.reset()
            },
            height: function (n) {
                return this._$("content").css("height", n),
                this.reset()
            },
            button: function (n) {
                n = n || [];
                var i = this,
                r = "",
                u = 0;
                return this.callbacks = {},
                "string" == typeof n ? (r = n, u++) : t.each(n,
                function (n, f) {
                    var e = f.id = f.id || f.value,
                    o = "";
                    i.callbacks[e] = f.callback;
                    f.display === !1 ? o = ' style="display:none"' : u++;
                    r += '<button type="button" i-id="' + e + '"' + o + (f.disabled ? " disabled" : "") + (f.autofocus ? ' autofocus class="ui-dialog-autofocus"' : "") + ">" + f.value + "<\/button>";
                    i._$("button").on("click", "[i-id=" + e + "]",
                    function (n) {
                        var r = t(this);
                        r.attr("disabled") || i._trigger(e);
                        n.preventDefault()
                    })
                }),
                this._$("button").html(r),
                this._$("footer")[u ? "show" : "hide"](),
                this
            },
            statusbar: function (n) {
                return this._$("statusbar").html(n)[n ? "show" : "hide"](),
                this
            },
            _$: function (n) {
                return this._popup.find("[i=" + n + "]")
            },
            _trigger: function (n) {
                var t = this.callbacks[n];
                return "function" != typeof t || t.call(this) !== !1 ? this.close().remove() : this
            }
        }),
        i.oncreate = t.noop,
        i.getCurrent = function () {
            return u.current
        },
        i.get = function (n) {
            return void 0 === n ? i.list : i.list[n]
        },
        i.list = {},
        i.defaults = o,
        i
    });
    n("drag",
    function (n) {
        var t = n("jquery"),
        f = t(window),
        i = t(document),
        e = "createTouch" in document,
        o = document.documentElement,
        l = !("minWidth" in o.style),
        h = !l && "onlosecapture" in o,
        c = "setCapture" in o,
        u = {
            start: e ? "touchstart" : "mousedown",
            over: e ? "touchmove" : "mousemove",
            end: e ? "touchend" : "mouseup"
        },
        s = e ?
        function (n) {
            return n.touches || (n = n.originalEvent.touches.item(0)),
            n
        } : function (n) {
            return n
        },
        r = function () {
            this.start = t.proxy(this.start, this);
            this.over = t.proxy(this.over, this);
            this.end = t.proxy(this.end, this);
            this.onstart = this.onover = this.onend = t.noop
        };
        return r.types = u,
        r.prototype = {
            start: function (n) {
                return n = this.startFix(n),
                i.on(u.over, this.over).on(u.end, this.end),
                this.onstart(n),
                !1
            },
            over: function (n) {
                return n = this.overFix(n),
                this.onover(n),
                !1
            },
            end: function (n) {
                return n = this.endFix(n),
                i.off(u.over, this.over).off(u.end, this.end),
                this.onend(n),
                !1
            },
            startFix: function (n) {
                return n = s(n),
                this.target = t(n.target),
                this.selectstart = function () {
                    return !1
                },
                i.on("selectstart", this.selectstart).on("dblclick", this.end),
                h ? this.target.on("losecapture", this.end) : f.on("blur", this.end),
                c && this.target[0].setCapture(),
                n
            },
            overFix: function (n) {
                return s(n)
            },
            endFix: function (n) {
                return n = s(n),
                i.off("selectstart", this.selectstart).off("dblclick", this.end),
                h ? this.target.off("losecapture", this.end) : f.off("blur", this.end),
                c && this.target[0].releaseCapture(),
                n
            }
        },
        r.create = function (n, u) {
            var h, c, v, y, e = t(n),
            o = new r,
            l = r.types.start,
            a = function () { },
            p = n.className.replace(/^\s|\s.*/g, "") + "-drag-start",
            s = {
                onstart: a,
                onover: a,
                onend: a,
                off: function () {
                    e.off(l, o.start)
                }
            };
            return o.onstart = function (t) {
                var r = "fixed" === e.css("position"),
                a = i.scrollLeft(),
                w = i.scrollTop(),
                o = e.width(),
                l = e.height();
                h = 0;
                c = 0;
                v = r ? f.width() - o + h : i.width() - o;
                y = r ? f.height() - l + c : i.height() - l;
                var u = e.offset(),
                b = this.startLeft = r ? u.left - a : u.left,
                k = this.startTop = r ? u.top - w : u.top;
                this.clientX = t.clientX;
                this.clientY = t.clientY;
                e.addClass(p);
                s.onstart.call(n, t, b, k)
            },
            o.onover = function (t) {
                var i = t.clientX - this.clientX + this.startLeft,
                r = t.clientY - this.clientY + this.startTop,
                u = e[0].style;
                i = Math.max(h, Math.min(v, i));
                r = Math.max(c, Math.min(y, r));
                u.left = i + "px";
                u.top = r + "px";
                s.onover.call(n, t, i, r)
            },
            o.onend = function (t) {
                var i = e.position(),
                r = i.left,
                u = i.top;
                e.removeClass(p);
                s.onend.call(n, t, r, u)
            },
            o.off = function () {
                e.off(l, o.start)
            },
            u ? o.start(u) : e.on(l, o.start),
            s
        },
        r
    });
    n("dialog-plus",
    function (n) {
        var i = n("jquery"),
        t = n("dialog"),
        r = n("drag");
        return t.oncreate = function (n) {
            var t, u = n.options,
            e = u.original,
            o = u.url,
            s = u.oniframeload,
            h, f;
            if (o && (this.padding = u.padding = 0, t = i("<iframe />"), t.attr({
                src: o,
                name: n.id,
                width: "100%",
                height: "100%",
                allowtransparency: "yes",
                frameborder: "no",
                scrolling: "yes"
            }).on("load",
            function () {
                var i;
                try {
                    i = t[0].contentWindow.frameElement
            } catch (r) { }
                i && (u.width || n.width(t.contents().width()), u.height || n.height(t.contents().height()));
                s && s.call(n)
            }), n.addEventListener("beforeremove",
            function () {
                t.attr("src", "about:blank").remove()
            },
            !1), n.content(t[0]), n.iframeNode = t[0]), !(e instanceof Object)) for (h = function () {
                n.close().remove()
            },
            f = 0; f < frames.length; f++) try {
                if (e instanceof frames[f].Object) {
                    i(frames[f]).one("unload", h);
                    break
                }
            } catch (c) { }
            i(n.node).on(r.types.start, "[i=title]",
            function (t) {
                n.follow || (n.focus(), r.create(n.node, t))
            })
        },
        t.get = function (n) {
            var i, u, r, f;
            if (n && n.frameElement) {
                u = n.frameElement;
                r = t.list;
                for (f in r) if (i = r[f], i.node.getElementsByTagName("iframe")[0] === u) return i
            } else if (n) return t.list[n]
        },
        t
    });
    window.dialog = t("dialog-plus")
}(),
function (n) {
    n.gritter = {};
    n.gritter.options = {
        position: "",
        class_name: "",
        fade_in_speed: "medium",
        fade_out_speed: 1e3,
        time: 6e3
    };
    n.gritter.add = function (n) {
        try {
            return t.add(n || {})
        } catch (r) {
            var i = "Gritter Error: " + r;
            typeof console != "undefined" && console.error ? console.error(i, n) : alert(i)
        }
    };
    n.gritter.remove = function (n, i) {
        t.removeSpecific(n, i || {})
    };
    n.gritter.removeAll = function (n) {
        t.stop(n || {})
    };
    var t = {
        position: "",
        fade_in_speed: "",
        fade_out_speed: "",
        time: "",
        _custom_timer: 0,
        _item_count: 0,
        _is_setup: 0,
        _tpl_close: '<a class="gritter-close" href="#" tabindex="1">Close Notification<\/a>',
        _tpl_title: '<span class="gritter-title">[[title]]<\/span>',
        _tpl_item: '<div id="gritter-item-[[number]]" class="gritter-item-wrapper [[item_class]]" style="display:none" role="alert"><div class="gritter-top"><\/div><div class="gritter-item">[[close]][[image]]<div class="[[class_name]]">[[title]]<p>[[text]]<\/p><\/div><div style="clear:both"><\/div><\/div><div class="gritter-bottom"><\/div><\/div>',
        _tpl_wrap: '<div id="gritter-notice-wrapper"><\/div>',
        add: function (i) {
            var r, e, c, l, u;
            if (typeof i == "string" && (i = {
                text: i
            }), i.text === null) throw 'You must supply "text" parameter.';
            this._is_setup || this._runSetup();
            var f = i.title,
            a = i.text,
            o = i.image || "",
            s = i.sticky || !1,
            v = i.class_name || n.gritter.options.class_name,
            y = n.gritter.options.position,
            h = i.time || "";
            return (this._verifyWrapper(), this._item_count++, r = this._item_count, e = this._tpl_item, n(["before_open", "after_open", "before_close", "after_close"]).each(function (u, f) {
                t["_" + f + "_" + r] = n.isFunction(i[f]) ? i[f] : function () { }
            }), this._custom_timer = 0, h && (this._custom_timer = h), c = o != "" ? '<img alt="image" src="' + o + '" class="gritter-image" />' : "", l = o != "" ? "gritter-with-image" : "gritter-without-image", f = f ? this._str_replace("[[title]]", f, this._tpl_title) : "", e = this._str_replace(["[[title]]", "[[text]]", "[[close]]", "[[image]]", "[[number]]", "[[class_name]]", "[[item_class]]"], [f, a, this._tpl_close, c, this._item_count, l, v], e), this["_before_open_" + r]() === !1) ? !1 : (n("#gritter-notice-wrapper").addClass(y).append(e), u = n("#gritter-item-" + this._item_count), u.fadeIn(this.fade_in_speed,
            function () {
                t["_after_open_" + r](n(this))
            }), s || this._setFadeTimer(u, r), n(u).bind("mouseenter mouseleave",
            function (i) {
                i.type == "mouseenter" ? s || t._restoreItemIfFading(n(this), r) : s || t._setFadeTimer(n(this), r);
                t._hoverState(n(this), i.type)
            }), n(u).find(".gritter-close").click(function () {
                return t.removeSpecific(r, {},
                null, !0),
                !1
            }), r)
        },
        _countRemoveWrapper: function (t, i, r) {
            i.remove();
            this["_after_close_" + t](i, r);
            n(".gritter-item-wrapper").length == 0 && n("#gritter-notice-wrapper").remove()
        },
        _fade: function (n, i, r, u) {
            var r = r || {},
            e = typeof r.fade != "undefined" ? r.fade : !0,
            o = r.speed || this.fade_out_speed,
            f = u;
            this["_before_close_" + i](n, f);
            u && n.unbind("mouseenter mouseleave");
            e ? n.animate({
                opacity: 0
            },
            o,
            function () {
                n.animate({
                    height: 0
                },
                300,
                function () {
                    t._countRemoveWrapper(i, n, f)
                })
            }) : this._countRemoveWrapper(i, n)
        },
        _hoverState: function (n, t) {
            t == "mouseenter" ? (n.addClass("hover"), n.find(".gritter-close").show()) : (n.removeClass("hover"), n.find(".gritter-close").hide())
        },
        removeSpecific: function (t, i, r, u) {
            if (!r) var r = n("#gritter-item-" + t);
            this._fade(r, t, i || {},
            u)
        },
        _restoreItemIfFading: function (n, t) {
            clearTimeout(this["_int_id_" + t]);
            n.stop().css({
                opacity: "",
                height: ""
            })
        },
        _runSetup: function () {
            for (opt in n.gritter.options) this[opt] = n.gritter.options[opt];
            this._is_setup = 1
        },
        _setFadeTimer: function (n, i) {
            var r = this._custom_timer ? this._custom_timer : this.time;
            this["_int_id_" + i] = setTimeout(function () {
                t._fade(n, i)
            },
            r)
        },
        stop: function (t) {
            var r = n.isFunction(t.before_close) ? t.before_close : function () { },
            u = n.isFunction(t.after_close) ? t.after_close : function () { },
            i = n("#gritter-notice-wrapper");
            r(i);
            i.fadeOut(function () {
                n(this).remove();
                u()
            })
        },
        _str_replace: function (n, t, i, r) {
            var f = 0,
            e = 0,
            o = "",
            c = "",
            l = 0,
            a = 0,
            h = [].concat(n),
            s = [].concat(t),
            u = i,
            v = s instanceof Array,
            y = u instanceof Array;
            for (u = [].concat(u), r && (this.window[r] = 0), f = 0, l = u.length; f < l; f++) if (u[f] !== "") for (e = 0, a = h.length; e < a; e++) o = u[f] + "",
            c = v ? s[e] !== undefined ? s[e] : "" : s[0],
            u[f] = o.split(h[e]).join(c),
            r && u[f] !== o && (this.window[r] += (o.length - u[f].length) / h[e].length);
            return y ? u : u[0]
        },
        _verifyWrapper: function () {
            n("#gritter-notice-wrapper").length == 0 && n("body").append(this._tpl_wrap)
        }
    }
}(jQuery);

dig = {
    addModel: function (n, t, i, r, u) {
        top.dialog({
            title: n,
            url: t,
            width: i,
            height: r,
            onremove: function () {
            },
            onclose: u
        }).showModal();
        return false;
    },
    /**
       * show: 打开弹窗并传值
       * @example
       * data,title,url,width,height,function
       */
    show: function (d,n, t, i, r, u) {
        top.dialog({
            title: n,
            url: t,
            data: d,
            width: i,
            height: r,
            onremove: function () {
            },
            onclose: u
        }).showModal();
        return false;
    },
    remove: function () {
        var n = top.dialog.get(window);
        n.close().remove();
    },
    close: function () {
        var n = top.dialog.get(window);
        n.close();
    },
    alertSuccess: function (n, t, i) {
        top.dialog({
            title: n,
            content: '<p style="padding:50px 100px; max-width:600px; margin-bottom:0px;font-size:18px;font-family:Microsoft YaHei,Helvetica,arial,sans-serif;color:#18a689;"><img src="/Content/themes/images/icon/success-32-32.png" style="margin-right:15px;" />' + t + "<\/p>",
            okValue: "确定",
            ok: function () {
                i && i()
            }
        }).showModal()
    },
    alertError: function (n, t, i) {
        top.dialog({
            title: n,
            content: '<p style="padding:50px 100px; max-width:600px; margin-bottom:0px;font-size:18px;font-family:Microsoft YaHei,Helvetica,arial,sans-serif;color:#c94e50;"><img src="/Content/themes/images/icon/error32-32.png" style="margin-right:15px;" />' + t + "<\/p>",
            okValue: " 关闭 ",
            ok: function () {
                i && i()
            }
        }).showModal()
    },
    confirm: function (n, t, i, r) {
        var u = top.dialog({
            title: n,
            content: '<p style="padding:50px 100px; max-width:600px; margin-bottom:0px;font-size:18px;font-family:Microsoft YaHei,Helvetica,arial,sans-serif;"><img src="/Content/themes/images/icon/warning32-32.png" style="margin-right:15px;" />' + t + "<\/p>",
            okValue: " 确定 ",
            ok: function () {
                i && i()
            },
            cancelValue: " 取消 ",
            cancel: function () {
                r && r()
            }
        }).showModal()
    },
    msg: function (n) {
        var t = top.dialog({
            content: '<p style="padding:50px 100px; max-width:600px; margin-bottom:0px;font-size:18px;font-family:Microsoft YaHei,Helvetica,arial,sans-serif;"><img src="/Content/themes/images/icon/success-32-32.png" style="margin-right:15px;" />' + n + "<\/p>"
        }).show();
        setTimeout(function () {
            t.close().remove()
        },
        1500)
    },
    upload: function (o, t) {
        top.dialog({
            title: '选择文件',
            url: '/Com/Upload/FileMain',
            width: 800,
            height: 480,
            data: o, // 给 iframe 的数据
            onclose: t,
            oniframeload: function () {
            }
        }).showModal();
        return false;
    }
},
function (n) {
    n.fn.checkall = function (t) {
        var f = {
            chname: "checkname[]",
            callback: function () { }
        },
        t = n.extend(f, t),
        u = n(this),
        i = n("input[name='" + t.chname + "']"),
        r = 0;
        return i.click(function () {
            i.filter(":checked").length === i.length ? u.attr("checked", !0) : u.removeAttr("checked");
            r = i.filter(":checked").length;
            typeof t.callback == "function" && t.callback(r)
        }),
        u.each(function () {
            n(this).click(function () {
                n(this).prop("checked") ? (i.prop("checked", !0), i.parent().parent().addClass("selected")) : (i.removeAttr("checked"), i.parent().parent().removeClass("selected"));
                r = i.filter(":checked").length;
                typeof t.callback == "function" && t.callback(r)
            })
        })
    }
}(jQuery);
dateFormat = function () {
    var t = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
    i = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
    r = /[^-+\dA-Z]/g,
    n = function (n, t) {
        for (n = String(n), t = t || 2; n.length < t;) n = "0" + n;
        return n
    };
    return function (u, f, e) {
        var h = dateFormat;
        if (arguments.length != 1 || Object.prototype.toString.call(u) != "[object String]" || /\d/.test(u) || (f = u, u = undefined), u = u ? new Date(u) : new Date, isNaN(u)) throw SyntaxError("invalid date");
        f = String(h.masks[f] || f || h.masks["default"]);
        f.slice(0, 4) == "UTC:" && (f = f.slice(4), e = !0);
        var o = e ? "getUTC" : "get",
        c = u[o + "Date"](),
        y = u[o + "Day"](),
        l = u[o + "Month"](),
        p = u[o + "FullYear"](),
        s = u[o + "Hours"](),
        w = u[o + "Minutes"](),
        b = u[o + "Seconds"](),
        a = u[o + "Milliseconds"](),
        v = e ? 0 : u.getTimezoneOffset(),
        k = {
            d: c,
            dd: n(c),
            ddd: h.i18n.dayNames[y],
            dddd: h.i18n.dayNames[y + 7],
            m: l + 1,
            mm: n(l + 1),
            mmm: h.i18n.monthNames[l],
            mmmm: h.i18n.monthNames[l + 12],
            yy: String(p).slice(2),
            yyyy: p,
            h: s % 12 || 12,
            hh: n(s % 12 || 12),
            H: s,
            HH: n(s),
            M: w,
            MM: n(w),
            s: b,
            ss: n(b),
            l: n(a, 3),
            L: n(a > 99 ? Math.round(a / 10) : a),
            t: s < 12 ? "a" : "p",
            tt: s < 12 ? "am" : "pm",
            T: s < 12 ? "A" : "P",
            TT: s < 12 ? "AM" : "PM",
            Z: e ? "UTC" : (String(u).match(i) || [""]).pop().replace(r, ""),
            o: (v > 0 ? "-" : "+") + n(Math.floor(Math.abs(v) / 60) * 100 + Math.abs(v) % 60, 4),
            S: ["th", "st", "nd", "rd"][c % 10 > 3 ? 0 : (c % 100 - c % 10 != 10) * c % 10]
        };
        return f.replace(t,
        function (n) {
            return n in k ? k[n] : n.slice(1, n.length - 1)
        })
    }
}();
dateFormat.masks = {
    "default": "ddd mmm dd yyyy HH:MM:ss",
    shortDate: "m/d/yy",
    mediumDate: "mmm d, yyyy",
    longDate: "mmmm d, yyyy",
    fullDate: "dddd, mmmm d, yyyy",
    shortTime: "h:MM TT",
    mediumTime: "h:MM:ss TT",
    longTime: "h:MM:ss TT Z",
    isoDate: "yyyy-mm-dd",
    isoTime: "HH:MM:ss",
    isoDateTime: "yyyy-mm-dd' 'HH:MM:ss",
    isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
};
dateFormat.i18n = {
    dayNames: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
    monthNames: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"]
};
Date.prototype.format = function (n, t) {
    return dateFormat(this, n, t)
};
$.fn.initValidform = function () {
    var n = function (n) {
        $(n).Validform({
            tiptype: function (n, t, i) {
                if (!t.obj.is("form")) {
                    t.obj.parent("div").find(".Validform_checktip").length == 0 && (t.obj.parent("div").append("<label class='Validform_checktip' />"), t.obj.parent("div").next().find(".Validform_checktip").remove());
                    var r = t.obj.parent("div").find(".Validform_checktip");
                    i(r, t.type);
                    r.text(n)
                }
            },
            showAllError: !0
        })
    };
    return $(this).each(function () {
        n($(this))
    })
}
