$(document).ready(function () {
    // Custom validation logic
    function validateForm() {
        let isValid = true;
        $('.validation-message').hide(); // Hide all previous validation messages

        // Validate only visible fields
        $('#registrationForm :input').each(function () {
            let $field = $(this);
            if ($field.is(':visible')) {
                let validationMessageId = '#' + $field.attr('id') + '-validation-message';

                // Required field validation
                if ($field.attr('required') && !$field.val()) {
                    $(validationMessageId).text('This field is required').show();
                    isValid = false;
                }

                // First and Last Name validation (letters only, at least 2 characters)
                if ($field.attr('id') === 'FirstName' || $field.attr('id') === 'LastName') {
                    if (!/^[A-Za-z]{2,}$/.test($field.val())) {
                        $(validationMessageId).text('Must be characters').show();
                        isValid = false;
                    }
                }

                // Email validation
                if ($field.attr('id') === 'Email' && !validateEmail($field.val())) {
                    $(validationMessageId).text('Please enter a valid email address').show();
                    isValid = false;
                }

                // Phone Number validation (digits only)
                if ($field.attr('id') === 'PhoneNumber' && !/^\d{10}$/.test($field.val())) {
                    $(validationMessageId).text('Must be 10 digits').show();
                    isValid = false;
                }
            }
        });

        // Trip Type selection validation
        if (!$('input[name="TripType"]:checked').val()) {
            $('#TripType-validation-message').text('Please select trip type').show();
            isValid = false;
        }

        // Pick-Up and Drop-Off Locations validation (should not be the same and both should be selected)
        let pickUpLocation = $('#PickUpLocation').val();
        let dropOffLocation = $('#DropOffLocation').val();
        if (pickUpLocation && dropOffLocation && pickUpLocation === dropOffLocation) {
            $('#DropOffLocation-validation-message').text('Locations cannot be the same').show();
            isValid = false;
        } else if (!pickUpLocation || !dropOffLocation) {
            if (!pickUpLocation) $('#PickUpLocation-validation-message').text('Please select pickup location').show();
            if (!dropOffLocation) $('#DropOffLocation-validation-message').text('Please select dropoff location').show();
            isValid = false;
        }

        return isValid;
    }

    // Email format validation utility function
    function validateEmail(email) {
        const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(email).toLowerCase());
    }

    // Attach validation to form submission event
    $('#registrationForm').on('submit', function (e) {
        if (!validateForm()) {
            e.preventDefault(); // Prevent form submission
            alert('Please correct the errors in the form.');
        }
    });
});
