//$(document).ready(function () {
//    // Custom validation logic
//    function validateForm() {
//        let isValid = true;
//        // Determine the selected Trip Type
//        let tripType = $('input[name="TripType"]:checked').val();

//        $('.validation-message').hide(); // Hide all previous validation messages

//        // Validate only visible fields
//        $('#registrationForm :input').each(function () {
//            let $field = $(this);
//            if ($field.is(':visible')) {
//                let validationMessageId = '#' + $field.attr('id') + '-validation-message';

//                // Skip validation for return fields if trip type is "One Way"
//                if (tripType === 'OneWay' && ($field.attr('id') === 'ReturnPickUpLocation' || $field.attr('id') === 'ReturnDropOffLocation' || $field.attr('name') === 'ReturnSelectedRouteDetail')) {
//                    // Skip the current iteration for these fields
//                    return true; // Equivalent to 'continue' in a traditional loop
//                }

//                // Required field validation
//                if ($field.attr('required') && !$field.val()) {
//                    $(validationMessageId).text('This field is required').show();
//                    isValid = false;
//                }

//                // Email validation
//                //if ($field.attr('id') === 'Email' && !/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test($field.val())) {
//                //    $(validationMessageId).text('Invalid email format').show();
//                //    isValid = false;
//                //}

//                // Phone Number validation (digits only)
//                if ($field.attr('id') === 'PhoneNumber' && !/^\d{10}$/.test($field.val())) {
//                    $(validationMessageId).text('Must be 10 digits').show();
//                    isValid = false;
//                }


//                // Trip Type selection validation
//                if (!$('input[name="TripType"]:checked').val()) {
//                    $('#TripType-validation-message').text('Please select trip type').show();
//                    isValid = false;
//                }

//                // Validation for routeOptions - Check if any radio within the group is checked
//                if ($field.attr('name') === 'SelectedRouteDetail' && !$('input[name="SelectedRouteDetail"]:checked').val()) {
//                    $('#routeOptions-validation-message').text('Please select a route option').show();
//                    isValid = false;
//                }

//                // Validation for returnRouteOptions - Check if any radio within the group is checked
//                if ($field.attr('name') === 'ReturnSelectedRouteDetail' && !$('input[name="ReturnSelectedRouteDetail"]:checked').val()) {
//                    $('#returnRouteOptions-validation-message').text('Please select a return route option').show();
//                    isValid = false;
//                }
//            }
//        });
//        // Pick-Up and Drop-Off Locations validation (should not be the same and both should be selected)
//        let pickUpLocation = $('#PickUpLocation').val();
//        let dropOffLocation = $('#DropOffLocation').val();
//        if (pickUpLocation && dropOffLocation && pickUpLocation === dropOffLocation) {
//            $('#DropOffLocation-validation-message').text('Locations cannot be the same').show();
//            isValid = false;
//        } else if (!pickUpLocation || !dropOffLocation) {
//            if (!pickUpLocation) $('#PickUpLocation-validation-message').text('Please select pickup location').show();
//            if (!dropOffLocation) $('#DropOffLocation-validation-message').text('Please select dropoff location').show();
//            isValid = false;
//        }

//        let returnPickUpLocation = $('#ReturnPickUpLocation').val();
//        let returnDropOffLocation = $('#ReturnDropOffLocation').val();
//        if (returnPickUpLocation && returnDropOffLocation && returnPickUpLocation === returnDropOffLocation) {
//            $('#ReturnDropOffLocation-validation-message').text('Locations cannot be the same').show();
//            isValid = false;
//        } else if (!returnPickUpLocation || !returnDropOffLocation) {
//            if (!returnPickUpLocation) $('#ReturnPickUpLocation-validation-message').text('Please select return pickup location').show();
//            if (!returnDropOffLocation) $('#ReturnDropOffLocation-validation-message').text('Please select return dropoff location').show();
//            isValid = false;
//        }

//        return isValid;
//    }

//    // Attach validation to form submission event
//    $('#registrationForm').on('submit', function (e) {
//        if (!validateForm()) {
//            e.preventDefault(); // Prevent form submission
//            alert('Please correct the errors in the form.');
//        }
//    });
//});

$(document).ready(function () {
    // Custom validation logic
    function validateForm() {
        let isValid = true;
        let tripType = $('input[name="TripType"]:checked').val();

        // Hide all previous validation messages
        $('.validation-message').hide();

        // Validate all visible fields except the return fields when TripType is "One Way"
        $('#registrationForm :input:visible').each(function () {
            let $field = $(this);
            let fieldId = $field.attr('id');
            let validationMessageId = '#' + fieldId + '-validation-message';

            // Skip validation for return fields if trip type is "One Way"
            if (tripType === 'OneWay' && (fieldId === 'ReturnPickUpLocation' || fieldId === 'ReturnDropOffLocation' || $field.attr('name') === 'ReturnSelectedRouteDetail')) {
                return true; // Skip to next field
            }

            // Required field validation
            if ($field.attr('required') && !$field.val()) {
                $(validationMessageId).text('This field is required').show();
                isValid = false;
            }

            // First and Last Name validation (letters only, at least 2 characters)
            if (fieldId === 'FirstName' || fieldId === 'LastName') {
                if (!/^[A-Za-z]{2,}$/.test($field.val())) {
                    $(validationMessageId).text('Must be characters').show();
                    isValid = false;
                }
            }

            // Phone Number validation (digits only)
            if (fieldId === 'PhoneNumber' && !/^\d{10}$/.test($field.val())) {
                $(validationMessageId).text('Must be 10 digits').show();
                isValid = false;
            }

            // Validation for routeOptions - Check if any radio within the group is checked
            if ($field.attr('name') === 'SelectedRouteDetail' && !$('input[name="SelectedRouteDetail"]:checked').val()) {
                $('#routeOptions-validation-message').text('Please select a route option').show();
                isValid = false;
            }

            // Validation for returnRouteOptions - Check if any radio within the group is checked
            if ($field.attr('name') === 'ReturnSelectedRouteDetail' && !$('input[name="ReturnSelectedRouteDetail"]:checked').val()) {
                $('#returnRouteOptions-validation-message').text('Please select a return route option').show();
                isValid = false;
            }

            // Add other specific field validations here as needed...
        });

        // Trip Type selection validation
        if (!$('input[name="TripType"]:checked').val()) {
            $('#TripType-validation-message').text('Please select trip type').show();
            isValid = false;
        }

        // Validate Pick-Up and Drop-Off Locations
        isValid = validateLocation('#PickUpLocation', '#DropOffLocation') && isValid;

        // If trip type is "RoundTrip" or "Friday", validate Return Locations as well
        if (tripType !== 'OneWay') {
            isValid = validateLocation('#ReturnPickUpLocation', '#ReturnDropOffLocation') && isValid;
        }

        return isValid;
    }

    function validateLocation(pickUpId, dropOffId) {
        let pickUpLocation = $(pickUpId).val();
        let dropOffLocation = $(dropOffId).val();
        let isValid = true;

        if (!pickUpLocation) {
            $(pickUpId + '-validation-message').text('Please select pickup location').show();
            isValid = false;
        }

        if (!dropOffLocation) {
            $(dropOffId + '-validation-message').text('Please select dropoff location').show();
            isValid = false;
        }

        if (pickUpLocation && dropOffLocation && pickUpLocation === dropOffLocation) {
            $(dropOffId + '-validation-message').text('Locations cannot be the same').show();
            isValid = false;
        }

        return isValid;
    }

    // Attach validation to form submission event
    $('#registrationForm').on('submit', function (e) {
        if (!validateForm()) {
            e.preventDefault(); // Prevent form submission
            alert('Please correct the errors in the form.');
        }
    });
});
