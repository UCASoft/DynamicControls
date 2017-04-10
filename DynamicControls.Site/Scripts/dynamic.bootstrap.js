(function ($) {

    $.dynamic.prepareDynamicControls.push(prepareDynamicBootstrapControls);

    $.dynamic.binds.push(function() {
        var area = $("[aria-dynamic = 'true']");
        area.addClass("row");
        var mainChildPanel = area.children(".child-panel");
        mainChildPanel.addClass("col-lg-10 col-lg-offset-1");
        prepareDynamicBootstrapControls(mainChildPanel);
    });

    if ($.dynamic.validation) {
        $.dynamic.validation.options.errorClass = "has-error";
    }

})(jQuery);

function prepareDynamicBootstrapControls(parentPanel) {
    var controls = $(parentPanel).children(".dynamic-control");
    controls.each(function () {
        prepareDynamicBootstrapControl(this);
    });
}

function prepareDynamicBootstrapControl(control) {
    var $control = $(control);
    var childs = $control.children().not(".child-panel");
    childs.wrapAll($("<div/>").addClass("form-group"));
    childs.each(function () {
        var $this = $(this);
        if ($this.hasClass("label-element")) {
            $this.addClass("control-label");
        } else if ($this.hasClass("work-element")) {
            switch ($this[0].tagName.toLowerCase()) {
            case "div":
                if ($this.hasClass("radio-list")) {
                    $this.children("input").each(function() {
                        var input = $(this);
                        var span = input.next("span");
                        input.wrap($("<label/>"));
                        var label = input.closest("label");
                        label.append(span.text());
                        span.remove();
                        label.wrap($("<div class='radio'/>"));
                        input.unbind("change").change(function() {
                            var $this = $(this);
                            var control = $this.closest(".dynamic-control");
                            control.attr("value", $this.val());
                            control.attr("text", $this.parent().text());
                            loadChilds(control);
                        });
                    });
                    $this.children("br").remove();
                }
                break;
            case "input":
                if ($this.attr("type") === "checkbox") {
                    var parent = $this.closest(".form-group");
                    parent.removeClass("form-group").addClass("checkbox");
		            var label = $("<label class='dynamic-checkbox'/>");
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
    prepareDynamicBootstrapControls($control.children(".child-panel"));
}
