$(document).ready(function () {
    // Custom validation logic
    function validateForm() {
        let isValid = true;
        let tripType = $('input[name="TripType"]:checked').val();
        var otherSpecialRequestDisplay = $('.other-special-request').css('display');
        var isOtherSpecialRequestYes = $('#otherSpecialYes').is(':checked');
        var otherMustArriveBy = $('#otherMustArriveBy').val();
        var otherCanLeaveAfter = $('#otherCanLeaveAfter').val();
        var otherCanLeaveAfterDisplay = $('.other-leave-after').css('display');
        var isOtherSpecialRequestVisible = $('.other-special-request').css('display') !== 'none';

        // Check for visibility of Days of the Week and First Day Expecting to Ride
        var daysOfWeekDisplay = $('.daysofweek-checkbox-group').css('display');
        var firstDayExpectingToRideDisplay = $('#FirstDayExpectingToRide').parent().css('display');

        // Check for visibility of SpecialPickUpLocation and SpecialDropOffLocation
        var specialPickUpLocationDisplay = $('.other-pickup').css('display');
        var specialDropOffLocationDisplay = $('.other-dropoff').css('display');
        var specialPickUpLocation = $('#other-pickup-location').val().trim();
        var specialDropOffLocation = $('#other-dropoff-location').val().trim();



        // Hide all previous validation messages
        $('.validation-message').hide();

        // Validate all visible fields except the return fields when TripType is "One Way"
        $('#registrationForm :input:visible').each(function () {
            let $field = $(this);
            let fieldId = $field.attr('id');
            let validationMessageId = '#' + fieldId + '-validation-message';

            //// Skip validation for return fields if trip type is "One Way"
            //if (tripType === 'OneWay' && (fieldId === 'ReturnPickUpLocation' || fieldId === 'ReturnDropOffLocation' || $field.attr('name') === 'ReturnSelectedRouteDetail')) {
            //    return true; // Skip to next field
            //}

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

            // Special Request validation based on trip types
            if (tripType === 'RoundTrip' && otherSpecialRequestDisplay !== 'none') {
                if (!($('#otherSpecialYes').is(':checked') || $('#otherSpecialNo').is(':checked'))) {
                    $('#OtherSpecialRequest-validation-message').show();
                    isValid = false;
                }
            }

            if (tripType === 'OneWay' && otherSpecialRequestDisplay !== 'none') {
                if (!($('#otherSpecialYes').is(':checked') || $('#otherSpecialNo').is(':checked'))) {
                    $('#OtherSpecialRequest-validation-message').show();
                    isValid = false;
                }
            }

            //// Validation for routeOptions - Check if any radio within the group is checked
            //if ($field.attr('name') === 'SelectedRouteDetail' && !$('input[name="SelectedRouteDetail"]:checked').val()) {
            //    $('#routeOptions-validation-message').text('Please select a route option').show();
            //    isValid = false;
            //}

            //// Check if either initial or return route details are required based on trip type
            //var isSelectedRouteDetailRequired = tripType === 'RoundTrip' || tripType === 'OneWay';
            //var isReturnSelectedRouteDetailRequired = tripType === 'RoundTrip';

            //// Initial route selection validation (applies for both OneWay and RoundTrip)
            //if (isSelectedRouteDetailRequired && !$('input[name="SelectedRouteDetail"]:checked').val() && isOtherSpecialRequestVisible) {
            //    $('#routeOptions-validation-message').text('Please select a route option').show();
            //    isValid = false;
            //}

            //// Return route selection validation (only applies for RoundTrip)
            //if (isReturnSelectedRouteDetailRequired && !$('input[name="ReturnSelectedRouteDetail"]:checked').val() && isOtherSpecialRequestVisible) {
            //    $('#returnRouteOptions-validation-message').text('Please select a return route option').show();
            //    isValid = false;
            //}

            // Validate otherCanLeaveAfter if Other Special Request is Yes and trip type is RoundTrip
            if (isOtherSpecialRequestYes && tripType === 'RoundTrip') {

                // Validate otherCanLeaveAfter only if it's visible
                if (otherCanLeaveAfterDisplay !== 'none' && !otherCanLeaveAfter) {
                    $('#otherMustArriveBy-validation-message').text('Please select must arrive time').show();
                    isValid = false;
                }
                // Validate otherCanLeaveAfter only if it's visible
                if (otherCanLeaveAfterDisplay !== 'none' && !otherCanLeaveAfter) {
                    $('#otherCanLeaveAfter-validation-message').text('Please select can leave after time').show();
                    isValid = false;
                }

                // Check if times are not the same (only if both times are provided and valid)
                if (otherMustArriveBy && otherCanLeaveAfter && otherMustArriveBy === otherCanLeaveAfter && otherCanLeaveAfterDisplay !== 'none') {
                    $('#otherMustArriveBy-validation-message, #otherCanLeaveAfter-validation-message').text('Times cannot be the same').show();
                    isValid = false;
                }
            }

            // Validate SpecialPickUpLocation if visible
            if (specialPickUpLocationDisplay !== 'none' && !specialPickUpLocation) {
                $('#SpecialPickUpLocation-validation-message').text('Please add a specific pick up location').show();
                isValid = false;
            }

            // Validate SpecialDropOffLocation if visible
            if (specialDropOffLocationDisplay !== 'none' && !specialDropOffLocation) {
                $('#SpecialDropOffLocation-validation-message').text('Please add a specific drop off location').show();
                isValid = false;
            }

            // If both fields are visible and required, ensure they are not the same
            if (specialPickUpLocationDisplay !== 'none' && specialDropOffLocationDisplay !== 'none' && specialPickUpLocation === specialDropOffLocation && specialPickUpLocation && specialDropOffLocation) {
                $('#SpecialPickUpLocation-validation-message').text('Pick up and drop off locations cannot be the same').show();
                $('#SpecialDropOffLocation-validation-message').text('Pick up and drop off locations cannot be the same').show();
                isValid = false;
            }

            if (!isOtherSpecialRequestVisible) {

                // Days of the Week validation if visible
                if (daysOfWeekDisplay !== 'none' && $('.daysofweek-checkbox-group :checkbox:checked').length === 0) {
                    $('#daysofweek-validation-message').text('Please select at least one day').show();
                    isValid = false;
                }

                // First Day Expecting to Ride validation if visible
                if (firstDayExpectingToRideDisplay !== 'none' && !$('#FirstDayExpectingToRide').val()) {
                    $('#FirstDayExpectingToRide-validation-message').text('Please select the first day you are expecting to ride').show();
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

        //// If trip type is "RoundTrip" or "Friday", validate Return Locations as well
        //if (tripType !== 'OneWay') {
        //    isValid = validateLocation('#ReturnPickUpLocation', '#ReturnDropOffLocation') && isValid;
        //}


        //// Days of the Week validation
        //if ($('.daysofweek-checkbox-group :checkbox:checked').length === 0) {
        //    $('#daysofweek-validation-message').show();
        //    isValid = false;
        //}

        //// First Day Expecting to Ride validation
        //if (!$('#FirstDayExpectingToRide').val()) {
        //    $('#FirstDayExpectingToRide-validation-message').show();
        //    isValid = false;
        //}

        //// Special Request validation
        //if (!$('input[name="SpecialRequest"]:checked').val()) {
        //    $('#OtherSpecialRequest-validation-message').show();
        //    isValid = false;
        //}

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
