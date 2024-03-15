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

        // Validate Bus Number (Only Numbers Allowed)
        isValidForm &= validateInput($('#BusNo'), /^\d+$/, 'BusNo-validation-message');

        // Validate Passenger Capacity (Between 0 and 30)
        var passengerCapacity = parseInt($('#PassengerCapacity').val().trim());
        isValidForm &= (passengerCapacity >= 0 && passengerCapacity <= 30);
        if (isValidForm) {
            $('#PassengerCapacity').removeClass('is-invalid');
            $('#PassengerCapacity-validation-message').hide();
        } else {
            $('#PassengerCapacity').addClass('is-invalid');
            $('#PassengerCapacity-validation-message').show();
        }

        return isValidForm;
    }

    // Input field keyup events for live validation
    $('#BusNo').on('input', function () {
        validateInput($(this), /^\d+$/, 'BusNo-validation-message');
    });

    $('#PassengerCapacity').on('input', function () {
        var passengerCapacity = parseInt($(this).val().trim());
        var isValid = (passengerCapacity >= 0 && passengerCapacity <= 30);
        if (isValid) {
            $(this).removeClass('is-invalid');
            $('#PassengerCapacity-validation-message').hide();
        } else {
            $(this).addClass('is-invalid');
            $('#PassengerCapacity-validation-message').show();
        }
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
