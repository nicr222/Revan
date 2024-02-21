document.addEventListener("DOMContentLoaded", function () {
    // Checks Location Name
    $("#Name").on("blur", function () {
        // Regular Expression for only 255 characters
        var regex = /^[A-Za-z0-9\s]{1,255}$/;

        if (regex.test($(this).val())) {
            $(this).removeClass("is-invalid").addClass("is-valid");
        } else {
            $(this).removeClass("is-valid").addClass("is-invalid");
        }
    });

    // Checks Location Address
    $("#Address").on("blur", function () {
        // Regular Expression for only 255 characters
        var regex = /^[A-Za-z0-9\s]{1,255}$/;

        if (regex.test($(this).val())) {
            $(this).removeClass("is-invalid").addClass("is-valid");
        } else {
            $(this).removeClass("is-valid").addClass("is-invalid");
        }
    });

    // Checks Location Abbreviation
    $("#Abbreviation").on("blur", function () {
        // Regular Expression for only 10 characters
        var regex = /^[A-Za-z0-9\s]{1,10}$/;

        if (regex.test($(this).val())) {
            $(this).removeClass("is-invalid").addClass("is-valid");
        } else {
            $(this).removeClass("is-valid").addClass("is-invalid");
        }
    });

    $("button[type='submit']").on("click", function () {
        // Trigger blur events for all relevant elements
        $("#Name, #Address, #Abbreviation").trigger("blur");

        // Check if any element has the is-invalid class
        if ($(".is-invalid").length === 0) {
            // No element has the is-invalid class, all fields are valid
            alert("Form submitted successfully!");
        } else {
            // At least one element has the is-invalid class, the form is not valid
            alert("Form contains invalid fields. Please check and try again.");
            $("form").on("submit", function (e) {
                e.preventDefault();
            });
        }
    });
});
