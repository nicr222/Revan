$(document).ready(function () {
    // Custom validation logic
    function validateForm() {
        let isValid = true;
        let tripType = $('input[name="TripType"]:checked').val();
        let specialRequest = $('#otherSpecialYes').is(':checked')

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

            // Special Request validation
            let specialRequestSelected = $('#otherSpecialYes').is(':checked') || $('#otherSpecialNo').is(':checked');
            if (!specialRequestSelected) {
                $('#OtherSpecialRequest-validation-message').text('Please select a special request option.').show();
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

            // Directly integrating the new validation requirements
            if (fieldId === 'otherMustArriveBy' || fieldId === 'otherCanLeaveAfter') {
                let arriveTime = $('#otherMustArriveBy').val();
                let leaveTime = $('#otherCanLeaveAfter').val();

                if (arriveTime && leaveTime && arriveTime === leaveTime) {
                    $('#otherMustArriveBy-validation-message, #otherCanLeaveAfter-validation-message').text('Times cannot be the same.').show();
                    isValid = false;
                }
            }

            // Special validation for 'other-pickup-location' and 'other-dropoff-location'
            if (fieldId === 'other-pickup-location' || fieldId === 'other-dropoff-location') {
                let wordsCount = $field.val().trim().split(/\s+/).length;

                if (wordsCount > 25) {
                    $(validationMessageId).text('Cannot exceed 25 words.').show();
                    isValid = false;
                }
            }

            // Conditional validation for Special Request related fields
            if (specialRequest) {
                // Validate 'otherMustArriveBy' and 'otherCanLeaveAfter' fields
                let arriveTime = $('#otherMustArriveBy').val();
                let leaveTime = $('#otherCanLeaveAfter').val();

                // Check if fields are filled out
                if (!arriveTime) {
                    $('#otherMustArriveBy-validation-message').text('This field is required.').show();
                    isValid = false;
                }

                // For 'otherCanLeaveAfter', check if empty and tripType is not 'OneWay'
                if (tripType !== 'OneWay' && !leaveTime) {
                    $('#otherCanLeaveAfter-validation-message').text('This field is required.').show();
                    isValid = false;
                }

                // Check if times are the same
                if (arriveTime && leaveTime && arriveTime === leaveTime) {
                    $('#otherMustArriveBy-validation-message, #otherCanLeaveAfter-validation-message').text('Arrival and departure times cannot be the same.').show();
                    isValid = false;
                }

                // Validate 'other-pickup-location' and 'other-dropoff-location' fields
                if (fieldId === 'other-pickup-location' || fieldId === 'other-dropoff-location') {
                    let fieldValue = $field.val().trim();
                    let charCount = fieldValue.length;

                    // Check if character count exceeds limit
                    if (charCount > 25) {
                        $(validationMessageId).text('Cannot exceed 25 characters.').show();
                        isValid = false;
                    }
                }

                // Check if pickup and dropoff locations are the same
                if ($('#other-pickup-location').val().trim() === $('#other-dropoff-location').val().trim()) {
                    $('#SpecialPickUpLocation-validation-message, #SpecialDropOffLocation-validation-message').text('Pickup and drop off locations cannot be the same.').show();
                    isValid = false;
                }
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

        // Days of the Week validation
        if ($('.daysofweek-checkbox-group :checkbox:checked').length === 0) {
            $('#daysofweek-validation-message').show();
            isValid = false;
        }

        // First Day Expecting to Ride validation
        if (!$('#FirstDayExpectingToRide').val()) {
            $('#FirstDayExpectingToRide-validation-message').show();
            isValid = false;
        }

        // Special Request validation
        if (!$('input[name="SpecialRequest"]:checked').val()) {
            $('#OtherSpecialRequest-validation-message').show();
            isValid = false;
        }

        return isValid;
    }
    function validateLocation(pickUpId, dropOffId) {
        let pickUpLocation = $(pickUpId).find(":selected").text(); // Get the selected text
        let dropOffLocation = $(dropOffId).find(":selected").text(); // Get the selected text
        let isValid = true;

        // Check for empty selections
        if (!pickUpLocation) {
            $(pickUpId + '-validation-message').text('Please select pickup location').show();
            isValid = false;
        }

        if (!dropOffLocation) {
            $(dropOffId + '-validation-message').text('Please select dropoff location').show();
            isValid = false;
        }

        // Allow both to be 'Other', but not the same any other case
        if (pickUpLocation !== 'Other' && dropOffLocation !== 'Other' && pickUpLocation === dropOffLocation) {
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
