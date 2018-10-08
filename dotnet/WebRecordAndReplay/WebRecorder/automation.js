console.log('loading automation');

/*var style = document.createElement('style');
style.type = 'text/css';
style.innerHTML = '.highlight { background: yellow; }';
document.getElementsByTagName('head')[0].appendChild(style);

$("body *").live('mouseover mouseout', function (event) {
        if (event.type == 'mouseover') {
            $(this).addClass('highlight');
        } else {
            $(this).removeClass('highlight');;
        }
        return false;
    });*/

function getFrames() {
    var i = new Array();
    function getSubframes(a, n) {
        var t = new Array();
        for (i[n] = 0; i[n] < a.length; i[n]++) {
            t.push(a[i[n]]);
            if (a[i[n]].frames.length) {
                t.push(getSubframes(a[i[n]].frames, n + 1));
            }
        }
        return t;
    }
    return getSubframes(top.frames, 0);
}

function getElementPath(element) {
    return "/" + $(element).parents().andSelf().map(function () {
        var $this = $(this);
        var tagName = this.nodeName;
        if ($this.siblings(tagName).length > 0) {
            tagName += "[" + $this.prevAll(tagName).length + "]";
        }
        return tagName;
    }).get().join("/").toLowerCase();
}

function getElementSelector(element) {
    return "" + $(element).parents().andSelf().map(function () {
        var $this = $(this);
        var tagName = this.nodeName;
        if ($this.siblings(tagName).length > 0) {
            tagName += ":nth-of-type(" + ($this.prevAll(tagName).length + 1) + ")";
        }
        return tagName;
    }).get().join(" ").toLowerCase();
}

function getActionParameters(element) {
    var actionParameters = {};
    actionParameters['checked'] = element.attr('checked');
    actionParameters['class'] = element.attr('class');
    actionParameters['className'] = element.attr('className');
    actionParameters['form'] = element.attr('form');
    actionParameters['href'] = element.attr('href');
    actionParameters['id'] = element.attr('id');
    actionParameters['name'] = element.attr('name');
    actionParameters['path'] = getElementPath(element);
    actionParameters['selector'] = getElementSelector(element);
    actionParameters['target'] = element.attr('target');
    actionParameters['type'] = element.attr('type');
    actionParameters['value'] = element.attr('value');
    return actionParameters;
}

function overrideWindowOpen() {
    overrideWindowOpenInAllFrames();
}

function overrideWindowOpenInAllFrames() {
    var frameobjects = getFrames();
    for (var i = 0; i < frameobjects.length; i++) {
        overrideWindowOpenInContext(frameobjects[i].window);
        console.log("overrode window.open in frame " + frameobjects[i].name);
    }
}

function overrideWindowOpenInFrame(frameName) {
    var frameobjects = getFrames();
    for (var i = 0; i < frameobjects.length; i++) {
        if (frameobjects[i].name == frameName) {
            overrideWindowOpenInContext(frameobjects[i].window);
            console.log("overrode window.open in frame " + frameobjects[i].name);
        }
    }
}

function overrideWindowOpenInContext(context) {
    if (context === undefined)
        context = window;
    context.open = function (open) {
        return function(url, name, features) {
            console.log("opening new window on top: " + url);
            return open.call(context, url, "_top", features);
        };
    }(context.open);
}

function subscribeToEvents() {
    subscribeToEventsInAllFrames();
}

function subscribeToEventsInAllFrames() {
    var frameobjects = getFrames();
    var frameSetExists = document.getElementsByTagName('frameset').length > 0;
    if (frameSetExists && frameobjects.length > 0) {
        for (var i = 0; i < frameobjects.length; i++) {
            subscribeToEventsInContext(frameobjects[i].document);
            console.log("subscribed to events in frame " + frameobjects[i].name);
        }
    }
    else
        subscribeToEventsInContext();
}

function subscribeToEventsInFrame(frameName) {
    var frameobjects = getFrames();
    for (var i = 0; i < frameobjects.length; i++) {
        if (frameobjects[i].name == frameName) {
            subscribeToEventsInContext(frameobjects[i].document);
            console.log("subscribed to events in frame " + frameobjects[i].name);
        }
    }
}

function subscribeToEventsInContext(context) {

    if (context === undefined)
        context = document;

    $("a", context).on('click', function () {
        var o = $(this);
        var actionParameters = getActionParameters(o);
        console.log("clicked on link to " + actionParameters['href']);
        automation.SetAction("linkclick", JSON.stringify(actionParameters));
    });

    $("input[type=submit]", context).on('click', function () {
        var o = $(this);
        var actionParameters = getActionParameters(o);
        console.log("clicked on submit labeled " + actionParameters['value']);
        automation.SetAction("submit", JSON.stringify(actionParameters));
    });

    $("input[type=image]", context).on('click', function () {
        var o = $(this);
        var actionParameters = getActionParameters(o);
        console.log("clicked on submit image " + actionParameters['value']);
        automation.SetAction("imageclick", JSON.stringify(actionParameters));
    });

    $(":button", context).on('click', function () {
        var o = $(this);
        var actionParameters = getActionParameters(o);
        console.log("clicked on button labeled " + actionParameters['value']);
        automation.SetAction("buttonclick", JSON.stringify(actionParameters));
    });

    $("input[type=text], input[type=password], textarea", context).on('change', function () {
        var o = $(this);
        var actionParameters = getActionParameters(o);
        console.log("changed value of text to " + actionParameters['value']);
        automation.SetAction("textchange", JSON.stringify(actionParameters));
    });

    $("select", context).on('change', function () {
        var o = $(this);
        var actionParameters = getActionParameters(o);
        console.log("changed value of select box to " + actionParameters['value']);
        automation.SetAction("selectchange", JSON.stringify(actionParameters));
    });

    $("input[type=radio]", context).on('change', function () {
        var o = $(this);
        var actionParameters = getActionParameters(o);
        console.log("changed value of radio button to " + actionParameters['value']);
        automation.SetAction("radiochange", JSON.stringify(actionParameters));
    });

    $("input[type=checkbox]", context).on('change', function () {
        var o = $(this);
        var actionParameters = getActionParameters(o);
        console.log("changed value of checkbox to " + actionParameters['value']);
        automation.SetAction("checkboxchange", JSON.stringify(actionParameters));
    });
}
