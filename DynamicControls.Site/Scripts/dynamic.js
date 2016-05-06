(function ($) {

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
        control.attr("text", $this.next("span").text());
        loadChilds(control);
    }).filter(":checked").each(function() {
        var $this = $(this);
        var control = $this.closest(".dynamic-control");
        control.attr("text", $this.next("span").text());
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
    var data = getChildData(area);
    if (data) {
        var result = {};
        result[areaName] = data;
        return result;
    }
    return null;
}

function getChildData(parent, inner) {
    var data = {};
    if ($.dynamic.validation) {
        if (!$.dynamic.validation.valid(parent)) {
            return null;
        }
    }
    var controls = parent.find(".dynamic-control[value]");
    if (!inner) {
        controls = controls.not(function() {
            return $(this).closest(".child-panel").hasClass("inner-child");
        });
    }
    controls.each(function() {
        var $this = $(this);
        var dataFunction = $this.data("getDataFunction");
        if (jQuery.isFunction(dataFunction)) {
            data[$this.attr("id")] = dataFunction();
        } else {
            data[$this.attr("id")] = { 'value': $this.attr("value"), 'text': $this.attr("text") };
        }
    });
    return data;
}
