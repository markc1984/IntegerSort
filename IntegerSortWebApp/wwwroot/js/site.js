// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function GetDynamicTextBox(value) {
    var div = $("<div />").addClass("container");
    var containerText = $("<div />").attr("style", "width: 15%").addClass("row pt-5")
    var textInput = $("<input />").attr("type", "number").attr("name", "Integer").addClass("form-control col-sm").attr("required", "");
    textInput.val(value);
    containerText.append(textInput);

    var button = $("<input />").attr("type", "button").attr("value", "Remove").addClass("btn btn-primary col-sm");
    button.attr("onclick", "RemoveInputBox(this)");
    containerText.append(button);
    div.append(containerText);

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

$(function () {
    $('#btnCancel').click(function (e) {
        $("form").validate().cancelSubmit = true;
    });
});