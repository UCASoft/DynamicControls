(function ($) {

    if (!$.dynamic) {
        throw new Error("This file should be loaded after 'dynamic.js'!");
    }

    $.dynamic.validation = $.dynamic.validation || {

        extensions: [],

        _checkInput: function(control, element) {
            var type = element.attr("type");
            if (!type || type === "text") {
                if (element.attr("required") && element.val() === "") {
                    this._setControlInvalid(control);
                    return false;
                }
            }
            return true;
        },

        _checkDiv: function(control, element) {
            var childElement = element.find("input").eq(0);
            var type = childElement.attr("type");
            if (type === "radio") {
                if (childElement.attr("required") && element.find("input[name = " + childElement.attr("name") + "]:checked").length !== 1) {
                    this._setControlInvalid(control);
                    return false;
                }
            }
            return true;
        },

        _checkSelect: function(control, element) {
            if (element.attr("required") && element.val() === "nullRow") {
                this._setControlInvalid(control);
                return false;
            }
            return true;
        },

        _setControlInvalid: function(control) {
            control.addClass(this.options.errorClass);
        },

        valid: function (parent) {
            var self = this;
            var $parent = $(parent);
            var result = true;
            $parent.find(".dynamic-control").removeClass(self.options.errorClass).each(function() {
                var control = $(this);
                var $element = control.find(".work-element").eq(0);
                var element = $element[0];
                if (element.tagName === "INPUT") {
                    result = self._checkInput(control, $element) && result;
                } else if (element.tagName === "DIV") {
                    result = self._checkDiv(control, $element) && result;
                } else if (element.tagName === "SELECT") {
                    result = self._checkSelect(control, $element) && result;
                }
            });
            for (var i = 0; i < this.extensions.length; i++) {
                result = this.extensions[i](parent) && result;
            }
            return result;
        },

        options: {
            errorClass: "error"
        }
    };

})(jQuery);