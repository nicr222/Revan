
document.addEventListener("DOMContentLoaded", function () {
    $("#studentID").on("blur", function () {
        // Regular Expresion for only 8 didgets
        var regex = /^\d{8}$/;

        if (regex.test($(this).val())) {
            $(this).removeClass("is-invalid").addClass("is-valid");
        } else {
            $(this).removeClass("is-valid").addClass("is-invalid");
        }
    });

    $("#firstName").on("blur", function () {
        // Regular Expresion for only 8 didgets
        var regex = /^\d{8}$/;

        if (regex.test($(this).val())) {
            $(this).removeClass("is-invalid").addClass("is-valid");
        } else {
            $(this).removeClass("is-valid").addClass("is-invalid");
        }
    });

    $("#lastName").on("blur", function () {
        // Regular Expresion for only 8 didgets
        var regex = /^\d{8}$/;

        if (regex.test($(this).val())) {
            $(this).removeClass("is-invalid").addClass("is-valid");
        } else {
            $(this).removeClass("is-valid").addClass("is-invalid");
        }
    });

    $("#startLocation").on("blur", function () {
        // Regular Expresion for only 8 didgets
        var regex = /^\d{8}$/;

        if (regex.test($(this).val())) {
            $(this).removeClass("is-invalid").addClass("is-valid");
        } else {
            $(this).removeClass("is-valid").addClass("is-invalid");
        }
    });

    $("#endLocation").on("blur", function () {
        // Regular Expresion for only 8 didgets
        var regex = /^\d{8}$/;

        if (regex.test($(this).val())) {
            $(this).removeClass("is-invalid").addClass("is-valid");
        } else {
            $(this).removeClass("is-valid").addClass("is-invalid");
        }
    });
});