
document.addEventListener("DOMContentLoaded", function () {
    $("input").on( "blur", function () {
        // Regular Expresion for only 8 didgets
        var regex = /^\d{8}$/;

        if (regex.test($(this).val())) {
            $(this).removeClass("is-invalid").addClass("is-valid");
        } else {
            $(this).removeClass("is-valid").addClass("is-invalid");
        }
    });
});