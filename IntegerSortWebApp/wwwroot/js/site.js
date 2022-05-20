// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function GetDynamicTextBox(value) {
    var div = $("<div />");
    var textInput = $("<input />").attr("type", "number").attr("name", "Integer").addClass("form-control");
    textInput.val(value);
    div.append("</br>");
    div.append(textInput);
    div.append("</br>");

    var button = $("<input />").attr("type", "button").attr("value", "Remove").addClass("btn btn-primary");
    button.attr("onclick", "RemoveInputBox(this)");
    div.append(button);

    return div;
}
function AddInputBox() {
    var div = GetDynamicTextBox("");
    $("#InputContainer").append(div);
}

function RemoveInputBox(button) {
    $(button).parent().remove();
}

$(function () {
    var values = eval('@Html.Raw(ViewBag.Values)');
    if (values != null) {
        $("#InputContainer").html("");
        $(values).each(function () {
            $("#InputContainer").append(GetDynamicTextBox(this));
        });
    }
});