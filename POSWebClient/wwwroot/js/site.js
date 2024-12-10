// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showMainAlert(type,message) {

    var addClass = "alert-success";
    var removeClass = "alert-danger";
    if (type == "Error") {
        addClass = "alert-danger";
        removeClass = "alert-success";
    }

    $("#divMainAlert").removeClass(removeClass).addClass(addClass).addClass("show");
    $("#spanMainAlert").text(message);
    setTimeout(function () {
        $("#divMainAlert").removeClass("show");
    }, 3000);




}