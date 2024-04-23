document.addEventListener("DOMContentLoaded", function () {
    // Checks student ID
    $("#studentID").on("blur", function () {
        // Regular Expression for only 8 digits
        var regex = /^\d{8}$/;

        if (regex.test($(this).val())) {
            $(this).removeClass("is-invalid").addClass("is-valid");
        } else {
            $(this).removeClass("is-valid").addClass("is-invalid");
        }
    });

    // Checks First Name
    $("#firstName").on("blur", function () {
        // Regular Expression for only 20 Letters
        var regex = /^[A-Za-z]{1,20}$/;

        if (regex.test($(this).val())) {
            $(this).removeClass("is-invalid").addClass("is-valid");
        } else {
            $(this).removeClass("is-valid").addClass("is-invalid");
        }
    });

    // Checks Last Name
    $("#lastName").on("blur", function () {
        // Regular Expression for only 20 Letters
        var regex = /^[A-Za-z]{1,20}$/;

        if (regex.test($(this).val())) {
            $(this).removeClass("is-invalid").addClass("is-valid");
        } else {
            $(this).removeClass("is-valid").addClass("is-invalid");
        }
    });

    // Checks start and end locations are not the same
    $("#startLocation, #endLocation").on("blur", function () {
        // Check if both start and end locations have the same value
        var startValue = $("#startLocation").val();
        var endValue = $("#endLocation").val();

        if (startValue == "" || endValue == "" || startValue === endValue) {
            // Either one is invalid or they have different values
            $("#startLocation, #endLocation").removeClass("is-valid").addClass("is-invalid");
        } else {
            // Both are valid and have the same value
            $("#startLocation, #endLocation").removeClass("is-invalid").addClass("is-valid");
        }
    });

    // Checks Feedback
    $("#feedback").on("blur", function () {
        // Max character length check
        var inputValue = $(this).val();
        var maxLength = 255;

        if (inputValue.length <= maxLength) {
            $(this).removeClass("is-invalid").addClass("is-valid");
        } else {
            $(this).removeClass("is-valid").addClass("is-invalid");
        }
    });

    $("button[type='submit']").on("click", function () {
        // Trigger blur events for all relevant elements
        $("#studentID, #firstName, #lastName, #startLocation, #endLocation, #feedback").trigger("blur");

        if ($('#newRiderYes').is(':checked') || $('#newRiderNo').is(':checked')) {
            $("#newRiderYes").removeClass("is-invalid").addClass("is-valid");

            // Check if any element has the is-invalid class
            if ($(".is-invalid").length === 0) {
                // No element has the is-invalid class, all fields are valid
                //alert("Form submitted successfully!");
            } else {
                // At least one element has the is-invalid class, the form is not valid
                alert("Form contains invalid fields. Please check and try again.");
                $("form").on("submit", function (e) {
                    e.preventDefault();
                });
            }
        } else {
            // neither radio is checked
            $("#newRiderYes").removeClass("is-valid").addClass("is-invalid");
            alert("Form contains invalid fields. Please check and try again.");
            $("form").on("submit", function (e) {
                e.preventDefault();
            });
        }
    });
});
