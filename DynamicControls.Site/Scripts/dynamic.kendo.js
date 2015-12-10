(function($) {
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
    /*var controls = $(parentPanel).children(".dynamic-control");
    controls.each(function () {
        prepareDynamicKendoControl(this);
    });*/
}

/*function prepareDynamicKendoControl(control) {
    
}*/

function onDateChange() {
    var control = this.element.closest(".dynamic-control");
    control.attr("value", this.value());
    loadChilds(control);
}

function onComboBoxChange() {
    var control = this.element.closest(".dynamic-control");
    control.attr("value", this.value());
    loadChilds(control);
}