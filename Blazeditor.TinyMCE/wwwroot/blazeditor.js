﻿// Proxy function that serves as middlemen
window.blazeditorCallbackProxy = function (dotNetInstance, callMethod, id, option, callbackMethod) {
    // Execute function that will do the actual job
    window[callMethod](id, option, function (result) {
        // Invoke the C# callback method passing the result as parameter
        return dotNetInstance.invokeMethodAsync(callbackMethod, result);
    });
    return true;
};

window.blazeditorInit = function (id, option, callback) {
    var html = document.getElementById(id).innerHTML;

    function setup(ed) {
        ed.on("init",
            function () {
                tinyMCE.get(id).setContent(html);
                tinyMCE.execCommand('mceRepaint');
            }
        );

        ed.on("change", function () {
            var content = ed.getContent();
            callback(content);
        });
    }

    var config = {
        selector: 'textarea#' + id,
        inline: false,
        setup: setup,
        default_link_target: '_blank',
        smart_paste: false,
    };
    
    for (var key in option) {
        if (option.hasOwnProperty(key)) {
            config[key] = option[key];
        }
    }

    if (option.inlineMode) {
        config.selector = '#' + id;
        config.inline = true;        
    } 

    tinymce.init(config);
}

window.blazeditorSetContent = function (id, data) {
    tinymce.get(id).setContent(data);
    tinyMCE.execCommand('mceRepaint');
}

window.blazeditorGetContent = function (id, format) {
    let getContentRequest = {
        format: format ? format : 'html'
    };

    let rawContent = tinymce.get(id).getContent(getContentRequest);

    // restore html format
    getContentRequest.format = 'html';
    tinymce.get(id).getContent(getContentRequest)
    return rawContent;
}
