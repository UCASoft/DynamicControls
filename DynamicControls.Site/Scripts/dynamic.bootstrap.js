$(document).ready(function() {
    var area = $("[aria-dynamic = 'true']");
    area.addClass("row");
    var mainChildPanel = area.children(".child-panel");
    mainChildPanel.addClass("col-lg-10 col-lg-offset-1");
    prepareDynamicControls(mainChildPanel);
});

function prepareDynamicControls(parentPanel) {
    var controls = $(parentPanel).children(".dynamic-control");
    controls.each(function () {
        prepareDynamicControl(this);
    });
}

function prepareDynamicControl(control) {
    var $control = $(control);
    var childs = $control.children().not(".child-panel");
    childs.wrapAll($("<div/>").addClass("form-group"));
    childs.each(function () {
        var $this = $(this);
        if ($this.hasClass("label-element")) {
            $this.addClass("control-label");
        } else if ($this.hasClass("work-element")) {
            switch ($this[0].tagName.toLowerCase()) {
            case "input":
                if ($this.attr("type") === "checkbox") {
                    var parent = $this.closest(".form-group");
                    parent.removeClass("form-group").addClass("checkbox");
                    var label = $("<label/>");
                    label.append($this);
                    var span = parent.children("span");
                    label.append(span.text());
                    span.remove();
                    parent.append(label);
                    break;
                }
            case "select":
            default:
                $this.addClass("form-control");
            }
        }
    });
    prepareDynamicControls($control.children(".child-panel"));
}