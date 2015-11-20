function loadChilds(name, value, childPanel) {
    $.post("GetChilds", { areaName: childPanel.closest("[aria-dynamic = 'true']").attr("id"), parentName: name, parentValue: value }, function(data) {
        childPanel.html(data);
        if (window.prepareDynamicControls)
            window.prepareDynamicControls(childPanel);
    });
}

function checkBoxChange(checkBox) {
    var control = $(checkBox).closest(".dynamic-control");
    loadChilds(control.attr("id"), checkBox.checked ? 1 : 0, control.children(".child-panel"));
}

function dateChange(date) {
    
}