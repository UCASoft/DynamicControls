(function($) {
    $.dynamic = $.dynamic || {};

    if (!$.dynamic.prepareDynamicControls) {
        $.dynamic.prepareDynamicControls = [];
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
    inputChange(select);
}

function dateChange(date) {
    inputChange(date);
}

function inputChange(input) {
    var $input = $(input);
    var control = $input.closest(".dynamic-control");
    control.attr("value", $input.val());
    loadChilds(control);
}

function getAreaData(areaName) {
    var data = {};
    var area = $("#" + areaName + "[aria-dynamic = 'true']");
    area.find(".dynamic-control[value]").each(function() {
        var $this = $(this);
        data[$this.attr("id")] = $this.attr("value");
    });
    return data;
}