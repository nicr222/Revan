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
        isValidForm &= validateInput($('#Name'), /^[A-Za-z\s]{2,25}$/, 'Name-validation-message');

        // Validate Address
        isValidForm &= validateInput($('#Address'), /^[A-Za-z0-9\s]{2,50}$/, 'Address-validation-message');

        // Validate City
        isValidForm &= validateInput($('#City'), /^[A-Za-z\s]{2,50}$/, 'City-validation-message');

        // Validate State
        isValidForm &= validateInput($('#State'), /^[A-Za-z]{2}$/, 'State-validation-message');

        // Validate ZipCode
        isValidForm &= validateInput($('#ZipCode'), /^[0-9]{5,10}$/, 'ZipCode-validation-message');

        // Validate Abbreviation
        isValidForm &= validateInput($('#Abbreviation'), /^[A-Za-z]{3}$/, 'Abbreviation-validation-message');

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
    $('#Name').change(function () {
        validateInput($(this), /^[A-Za-z\s]{2,25}$/, 'Name-validation-message');
    });

    $('#Address').change(function () {
        validateInput($(this), /^[A-Za-z0-9\s]{2,50}$/, 'Address-validation-message');
    });

    $('#City').change(function () {
        validateInput($(this), /^[A-Za-z\s]{2,50}$/, 'City-validation-message');
    });

    $('#State').change(function () {
        validateInput($(this), /^[A-Za-z]{2}$/, 'State-validation-message');
    });

    $('#ZipCode').change(function () {
        validateInput($(this), /^[0-9]{5,10}$/, 'ZipCode-validation-message');
    });

    $('#Abbreviation').change(function () {
        validateInput($(this), /^[A-Za-z]{3}$/, 'Abbreviation-validation-message');
    });
});
