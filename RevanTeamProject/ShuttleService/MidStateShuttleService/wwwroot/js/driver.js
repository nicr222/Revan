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

        // Validate Name
        isValidForm &= validateInput($('#Name'), /^[A-Za-z\s]{2,100}$/, 'Name-validation-message');

        // Validate Phone Number
        isValidForm &= validateInput($('#PhoneNumb'), /^\d{10,20}$/, 'PhoneNumb-validation-message');

        // Validate Email
        isValidForm &= validateInput($('#Email'), /^[^\s@]+@[^\s@]+\.[^\s@]+$/, 'Email-validation-message');

        return isValidForm;
    }

    // Input field keyup events for live validation
    $('#Name').on('input', function () {
        validateInput($(this), /^[A-Za-z\s]{2,100}$/, 'Name-validation-message');
    });

    $('#PhoneNumb').on('input', function () {
        validateInput($(this), /^\d{10,20}$/, 'PhoneNumb-validation-message');
    });

    $('#Email').on('input', function () {
        validateInput($(this), /^[^\s@]+@[^\s@]+\.[^\s@]+$/, 'Email-validation-message');
    });

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
});
