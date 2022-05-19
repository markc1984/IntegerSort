// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function GetDynamicTextBox(value) {
    var div = $("<div />");
    var textBox = $("<input />").attr("type", "text").attr("name", "Integer").addClass("form-control");
    textBox.val(value);
    div.append("</br>");
    div.append(textBox);
    div.append("</br>");

    var button = $("<input />").attr("type", "button").attr("value", "Remove").addClass("btn btn-primary");
    button.attr("onclick", "RemoveTextBox(this)");
    div.append(button);

    return div;
}
function AddTextBox() {
    var div = GetDynamicTextBox("");
    $("#TextBoxContainer").append(div);
}

function RemoveTextBox(button) {
    $(button).parent().remove();
}

$(function () {
    var values = eval('@Html.Raw(ViewBag.Values)');
    if (values != null) {
        $("#TextBoxContainer").html("");
        $(values).each(function () {
            $("#TextBoxContainer").append(GetDynamicTextBox(this));
        });
    }
});