$(document).ready(function() {
    addRadioChange($("[aria-dynamic = 'true']"));
});

function addRadioChange(parent) {
    $(parent).find(".radio-list").closest(".dynamic-control:has(.child-panel)").find("input[type=radio]").change(function () {
        var $this = $(this);
        loadChilds($this.attr("name"), $this.val(), $this.closest(".dynamic-control").children(".child-panel"));
    });
}

function loadChilds(name, value, childPanel) {
    $.post("GetChilds", { areaName: childPanel.closest("[aria-dynamic = 'true']").attr("id"), parentName: name, parentValue: value }, function(data) {
        childPanel.html(data);
        addRadioChange(childPanel);
        if (window.prepareDynamicControls)
            window.prepareDynamicControls(childPanel);
    });
}

function checkBoxChange(checkBox) {
    var control = $(checkBox).closest(".dynamic-control");
    loadChilds(control.attr("id"), checkBox.checked ? 1 : 0, control.children(".child-panel"));
}

function selectChange(select) {
    var $select = $(select);
    var control = $select.closest(".dynamic-control");
    loadChilds(control.attr("id"), $select.val(), control.children(".child-panel"));
}

function dateChange(date) {
    
}