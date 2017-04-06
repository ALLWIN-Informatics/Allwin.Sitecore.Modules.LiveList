function fieldBase () {}

fieldBase.getParentIframe = function () {
    var iframes = window.parent.document.getElementsByTagName('iframe');
    var parent;
    Array.prototype.forEach.call(iframes, function (el, i) {
        if (fieldBase.getIframeDocument(el) === window.document) {
            parent = el;
            return;
        }
    });
    return parent;
};

fieldBase.getIframeDocument = function (iframe) {
    return iframe.contentDocument || iframe.contentWindow.document;
};

fieldBase.ajax = function (url, params, onSuccess) {
    var xmlhttp = new XMLHttpRequest();

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == XMLHttpRequest.DONE) {
            if (xmlhttp.status == 200) {
                onSuccess(JSON.parse(xmlhttp.responseText));
            }
            else {
                console.log("Error occured while get request");
            }
        }
    };

    var contextParams = window.location.search;
    xmlhttp.open("GET", url + contextParams + params);
    xmlhttp.send();
};

fieldBase.fillDropdown = function (selectEl, values) {
    if (values === undefined) {
        return;
    }

    var emptyOption = document.createElement("option");
    selectEl.appendChild(emptyOption);

    Array.prototype.forEach.call(values, function (el, i) {
        var option = document.createElement("option");
        var text = document.createTextNode(el.Text);
        option.setAttribute("value", el.Value);
        option.appendChild(text);

        selectEl.appendChild(option);
    });
};

fieldBase.removeChildren = function (node) {
    while (node.hasChildNodes()) {
        node.removeChild(node.lastChild);
    }
}

fieldBase.observe = function (targetId, callback) {
    var target = document.getElementById(targetId);
    var observer = new MutationObserver(function (mutations) {
        callback();
    });

    var config = { attributes: true, childList: true, subtree: true, characterData: true };
    observer.observe(target, config);
};