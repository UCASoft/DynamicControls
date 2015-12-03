$(document).ready(function() {
    addRadioChange($("[aria-dynamic = 'true']"));
});

function addRadioChange(parent) {
    $(parent).find(".radio-list").closest(".dynamic-control:has(.child-panel)").find("input[type=radio]").change(function () {
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
    $.post("GetChilds", { areaName: childPanel.closest("[aria-dynamic = 'true']").attr("id"), parentName: name, parentValue: value }, function(data) {
        childPanel.html(data);
        addRadioChange(childPanel);
        if (window.prepareDynamicControls)
            window.prepareDynamicControls(childPanel);
    });
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
    loadChilds(control);
}

function dateChange(date) {
    
}