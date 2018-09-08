; (function (define) {
    define(['jquery'], function ($) {
        return (function () {
            var lib = {
                //消息处理
                messager: top.toastr,
              
            }
            return lib;
        })();
    });
}(typeof define === 'function' && define.amd ? define : function (deps, factory) {
    if (typeof module !== 'undefined' && module.exports) { //Node
        module.exports = factory(require('jquery'));
    } else {
        window['lib'] = factory(window['jQuery']);
    }
}));
