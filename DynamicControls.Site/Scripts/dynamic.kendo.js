(function($) {
    $.dynamic.prepareDynamicControls.push(prepareDynamicKendoControls);
    $.dynamic.binds.push(function() {
        prepareDynamicKendoControls($("[aria-dynamic = 'true']"));
    });

    if ($.dynamic.validation) {
        $.dynamic.validation.extensions.push(kendoValidate);
    }

})(jQuery);


function prepareDynamicKendoControls(parentPanel) {
    $(parentPanel).find("input[type = 'date']").not("[custom-creating=\"true\"]").removeAttr("onchange").kendoDatePicker({
        change: onDateChange
    });
    $(parentPanel).find("select").not("[custom-creating=\"true\"]").removeAttr("onchange").width("270px").each(function() {
        var $this = $(this);
        var nullOptions = $this.children("[value = 'nullRow']");
        var selectedOption = $this.children("[selected = 'selected']");
        var hasNullOption = nullOptions.length === 1;
        nullOptions.remove();
        var kendoDropDownList;
        if (hasNullOption) {
            kendoDropDownList = $this.kendoDropDownList({
                change: onDropDownChange,
                width: "270px",
                optionLabel: " ",
                value: " "
            }).data("kendoDropDownList");
        } else {
            kendoDropDownList = $this.kendoDropDownList({
                change: onDropDownChange,
                width: "270px"
            }).data("kendoDropDownList");
        }
        if (selectedOption) {
            kendoDropDownList.value(selectedOption.val());
        }
    });
}

function onDateChange() {
    var control = this.element.closest(".dynamic-control");
    control.attr("value", kendo.toString(this.value(), 'yyyy-MM-dd'));
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
        var innerParent = $this.closest(".child-panel");
        if (!innerParent.hasClass("inner-child") || innerParent.text() === parent.text()) {
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
        }
    });
    return result;
}
