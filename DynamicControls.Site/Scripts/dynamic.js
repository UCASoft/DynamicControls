(function ($) {

    $.dynamic = $.dynamic || {};

    if (!$.dynamic.prepareDynamicControls) {
        $.dynamic.prepareDynamicControls = [];
    }

    if (!$.dynamic.validateParent) {
        $.dynamic.validateParent = [];
    }

    $.dynamic.validationAvailable = function() {
        return $.validator || false;
    }

    if ($.dynamic.validationAvailable()) {
        $.validator.setDefaults({
            highlight: function(element, errorClass) {
                $(element).addClass(errorClass);
            },
            errorPlacement: function () {}
        });
    }

})(jQuery);

$(document).ready(function () {
    addRadioChange($("[aria-dynamic = 'true']"));
});

function addRadioChange(parent) {
    $(parent).find(".radio-list").find("input[type=radio]").change(function () {
        var $this = $(this);
        var control = $this.closest(".dynamic-control");
        control.attr("value", $this.val());
        control.attr("text", $this.parent().text());
        loadChilds(control);
    });
}

function loadChilds(control) {
    var name = control.attr("id");
    var value = control.attr("value");
    var childPanel = control.children(".child-panel");
    if (childPanel.length === 1) {
        $.post("GetChilds", { areaName: control.closest("[aria-dynamic = 'true']").attr("id"), parentName: name, parentValue: value }, function(data) {
            childPanel.html(data);
            addRadioChange(childPanel);
            for (var i = 0; i < $.dynamic.prepareDynamicControls.length; i++) {
                $.dynamic.prepareDynamicControls[i](childPanel);
            }
        });
    }
}

function checkBoxChange(checkBox) {
    var control = $(checkBox).closest(".dynamic-control");
    control.attr("value", checkBox.checked ? 1 : 0);
    loadChilds(control);
}

function selectChange(select) {
    var $select = $(select);
    var control = $select.closest(".dynamic-control");
    control.attr("value", $select.val());
    control.attr("text", $select.find("option[value = '" + $select.val() + "']").text());
    loadChilds(control);
}

function dateChange(date) {
    inputChange(date);
}

function inputChange(input) {
    var $input = $(input);
    var control = $input.closest(".dynamic-control");
    control.attr("value", $input.val());
    control.attr("text", $input.val());
    loadChilds(control);
}

function getAreaData(areaName) {
    var area = $("#" + areaName + "[aria-dynamic = 'true']");
    return getChildData(area);
}

function getChildData(parent) {
    var data = {};
    if ($.dynamic.validationAvailable()) {
        parent.wrap("<form id=\"temp-dynamic-form\"/>");
        var form = parent.parent();
        var valid = form.valid();
        if (valid) {
            for (var i = 0; i < $.dynamic.validateParent.length; i++) {
                if (!$.dynamic.validateParent[i](form)) {
                    valid = false;
                    break;
                }
            }
        }
        parent.unwrap();
        if (!valid)
            return null;
    }
    parent.find(".dynamic-control[value]").each(function() {
        var $this = $(this);
        data[$this.attr("id")] = { 'value': $this.attr("value"), 'text': $this.attr("text") };
    });
    return data;
}