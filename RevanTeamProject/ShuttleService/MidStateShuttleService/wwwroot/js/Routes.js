$(document).ready(function () {
    // Function to validate each input field
    function validateInput(input, regex, errorMessageId) {
        var value = input.val().trim();
        var isValid = regex.test(value);

        if (isValid) {
            input.removeClass('is-invalid');
            $('#' + errorMessageId).hide();
        } else {
            input.addClass('is-invalid');
            $('#' + errorMessageId).show();
        }

        return isValid;
    }

    // Function to validate the entire form
    function validateForm() {
        var isValidForm = true;

        // Validate Pick Up Location
        isValidForm &= validateInput($('#PickUpLocationID'), /^\d+$/, 'PickUpLocationID-validation-message');

        // Validate Drop Off Location
        isValidForm &= validateInput($('#DropOffLocationID'), /^\d+$/, 'DropOffLocationID-validation-message');

        // Validate Pick Up Time
        isValidForm &= validateInput($('#PickUpTime'), /^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$/, 'PickUpTime-validation-message');

        // Validate Drop Off Time
        isValidForm &= validateInput($('#DropOffTime'), /^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$/, 'DropOffTime-validation-message');

        // Additional validation can be added for textarea if needed

        // Check if dropdowns have selected values
        if ($('#PickUpLocationID').val() === "") {
            $('#PickUpLocationID').addClass('is-invalid');
            $('#PickUpLocationID-validation-message').show();
            isValidForm = false;
        }

        if ($('#DropOffLocationID').val() === "") {
            $('#DropOffLocationID').addClass('is-invalid');
            $('#DropOffLocationID-validation-message').show();
            isValidForm = false;
        }

        return isValidForm;
    }

    // Submit form event
    $('form').submit(function () {
        if (validateForm()) {
            // Form is valid, continue with submission
            return true;
        } else {
            // Form is invalid, prevent submission
            return false;
        }
    });

    // Input field change events for live validation
    $('#PickUpLocationID').change(function () {
        validateInput($(this), /^\d+$/, 'PickUpLocationID-validation-message');
    });

    $('#DropOffLocationID').change(function () {
        validateInput($(this), /^\d+$/, 'DropOffLocationID-validation-message');
    });

    $('#PickUpTime').on('input', function () {
        validateInput($(this), /^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$/, 'PickUpTime-validation-message');
    });

    $('#DropOffTime').on('input', function () {
        validateInput($(this), /^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$/, 'DropOffTime-validation-message');
    });
});
