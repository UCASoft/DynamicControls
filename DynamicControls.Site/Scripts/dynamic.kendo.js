﻿(function($) {
    $.dynamic.prepareDynamicControls.push(prepareDynamicKendoControls);
})(jQuery);

$(document).ready(function () {
    prepareDynamicKendoControls($("[aria-dynamic = 'true']"));
});

function prepareDynamicKendoControls(parentPanel) {
    $(parentPanel).find("input[type = 'date']").removeAttr("onchange").kendoDatePicker({
        change: onDateChange
    });
    $(parentPanel).find("select").removeAttr("onchange").width("270px").kendoComboBox({
        change: onComboBoxChange,
        width: "270px"
    });
}

function onDateChange() {
    var control = this.element.closest(".dynamic-control");
    control.attr("value", this.value());
    control.attr("text", kendo.toString(this.value(), 'd'));
    loadChilds(control);
}

function onComboBoxChange() {
    var control = this.element.closest(".dynamic-control");
    control.attr("value", this.value());
    control.attr("text", this.text());
    loadChilds(control);
}