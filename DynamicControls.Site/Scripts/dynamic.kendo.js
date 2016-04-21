﻿(function($) {
    $.dynamic.prepareDynamicControls.push(prepareDynamicKendoControls);

    if ($.dynamic.validation) {
        $.dynamic.validation.extensions.push(kendoValidate);
    }   
})(jQuery);

$(document).ready(function () {
    prepareDynamicKendoControls($("[aria-dynamic = 'true']"));
});

function prepareDynamicKendoControls(parentPanel) {
    $(parentPanel).find("input[type = 'date']").removeAttr("onchange").kendoDatePicker({
        change: onDateChange
    });
    $(parentPanel).find("select").removeAttr("onchange").width("270px").each(function() {
        var $this = $(this);
        var nullOptions = $this.children("[value = 'nullRow']");
        var hasNullOption = nullOptions.length === 1;
        nullOptions.remove();
        if (hasNullOption) {
            $this.kendoDropDownList({
                change: onDropDownChange,
                width: "270px",
                optionLabel: " ",
                value: " "
            });
        } else {
            $this.kendoDropDownList({
                change: onDropDownChange,
                width: "270px"
            });
        }
    });
}

function onDateChange() {
    var control = this.element.closest(".dynamic-control");
    control.attr("value", this.value());
    control.attr("text", kendo.toString(this.value(), 'd'));
    loadChilds(control);
}

function onDropDownChange() {
    var control = this.element.closest(".dynamic-control");
    control.attr("value", this.value());
    control.attr("text", this.text());
    loadChilds(control);
}

function kendoValidate(parent) {
    var result = true;
    parent.find(".k-widget").each(function() {
        var $this = $(this);
        var control = kendo.widgetInstance($this.find(".work-element"));
        if ($this.hasClass("k-datepicker")) {
            var input = $this.find("input");
            if (input.attr("required") && !control.value()) {
                $this.closest(".dynamic-control").addClass($.dynamic.validation.options.errorClass);
                result = false;
            }
        } else if ($this.hasClass("k-dropdown")) {
            var select = $this.find("select");
            if (select.attr("required") && !control.value()) {
                $this.closest(".dynamic-control").addClass($.dynamic.validation.options.errorClass);
                result = false;
            }
        }
    });
    return result;
}