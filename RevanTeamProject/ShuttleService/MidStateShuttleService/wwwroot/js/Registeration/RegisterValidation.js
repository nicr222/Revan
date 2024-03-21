$(document).ready(function () {
    // Custom validation logic
    function validateForm() {
        let isValid = true;
        let tripType = $('input[name="TripType"]:checked').val();
        var otherSpecialRequestDisplay = $('.other-special-request').css('display');
        var isOtherSpecialRequestYes = $('#otherSpecialYes').is(':checked');
        var isOtherSpecialRequestNo = $('#otherSpecialNo').is(':checked');
        var otherMustArriveBy = $('#otherMustArriveBy').val();
        var otherCanLeaveAfter = $('#otherCanLeaveAfter').val();
        var otherCanLeaveAfterDisplay = $('.other-leave-after').css('display');
        var isOtherSpecialRequestVisible = $('.other-special-request').css('display') !== 'none';
        var returnRouteOptionsDisplay = $('.return-route-location-schedule').css('display');


        // Determine if either or both pick-up and drop-off location are not 'Other'
        var pickUpLocationText = $('#PickUpLocation option:selected').text().trim();
        var dropOffLocationText = $('#DropOffLocation option:selected').text().trim();
        var isAtLeastOneLocationNotOther = pickUpLocationText !== 'Other' || dropOffLocationText !== 'Other';

        // Determine if either or both return pick-up and return drop-off location are not 'Other' (for RoundTrip)
        var returnPickUpLocation = $('#PickUpLocation').val();
        var returnDropOffLocation = $('#DropOffLocation').val();
        var returnPickUpLocationText = $('#ReturnPickUpLocation option:selected').text().trim();
        var returnDropOffLocationText = $('#ReturnDropOffLocation option:selected').text().trim();
        var isReturnPickUporDropOffOther = returnPickUpLocationText === 'Other' || returnDropOffLocationText === 'Other';


        // Check for visibility of Days of the Week and First Day Expecting to Ride
        var daysOfWeekDisplay = $('.daysofweek-checkbox-group').css('display');
        var firstDayExpectingToRideDisplay = $('#FirstDayExpectingToRide').parent().css('display');

        // Check for selected PickUpLocation and DropOffLocation
        var pickUpLocation = $('#PickUpLocation').val();
        var dropOffLocation = $('#DropOffLocation').val();
        var pickUpLocationText = $('#PickUpLocation option:selected').text().trim();
        var dropOffLocationText = $('#DropOffLocation option:selected').text().trim();

        // Determine if either or both pick-up and drop-off location are 'Other'
        var isPickUpOrDropOffOther = pickUpLocationText === 'Other' || dropOffLocationText === 'Other';

        // Add a new condition to check if both pick-up and drop-off location are 'Other'
        var areBothLocationsOther = pickUpLocationText === 'Other' && dropOffLocationText === 'Other';

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

            // Validate SpecialPickUpLocation and SpecialDropOffLocation only if PickUpLocation or DropOffLocation is not 'Other'
            if (isPickUpOrDropOffOther || isReturnPickUporDropOffOther) {
                // Existing validation for visibility and non-empty values
                if (specialPickUpLocationDisplay !== 'none' && !specialPickUpLocation) {
                    $('#SpecialPickUpLocation-validation-message').text('Please add a specific pick up location').show();
                    isValid = false;
                }
                if (specialDropOffLocationDisplay !== 'none' && !specialDropOffLocation) {
                    $('#SpecialDropOffLocation-validation-message').text('Please add a specific drop off location').show();
                    isValid = false;
                }

                // Here's where you should add or fix the condition to ensure it handles the case where both locations are the same
                // If both fields are visible and required, ensure they are not the same
                if (specialPickUpLocationDisplay !== 'none' && specialDropOffLocationDisplay !== 'none' && specialPickUpLocation === specialDropOffLocation && specialPickUpLocation && specialDropOffLocation) {
                    $('#SpecialPickUpLocation-validation-message').text('Pick up and drop off locations cannot be the same').show();
                    $('#SpecialDropOffLocation-validation-message').text('Pick up and drop off locations cannot be the same').show();
                    isValid = false;
                }
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

        // Determine if either or both pick-up and drop-off location are 'Other'
        var isPickUpOrDropOffOther = pickUpLocationText === 'Other' || dropOffLocationText === 'Other';

        // Validate Pick-Up and Drop-Off Locations only if both are not 'Other'
        if (!isPickUpOrDropOffOther) {
            isValid = validateLocation('#PickUpLocation', '#DropOffLocation') && isValid;
        }
        //// Determine if validation for Return Locations should be skipped
        //let skipReturnLocationsValidation = shouldSkipReturnLocationsValidation();

        //// If trip type is "RoundTrip" or "Friday", validate Return Locations as well
        //if (tripType !== 'OneWay' && !skipReturnLocationsValidation) {
        //    isValid = validateLocation('#ReturnPickUpLocation', '#ReturnDropOffLocation') && isValid;
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

    function shouldSkipReturnLocationsValidation() {
        var pickUpLocationText = $('#PickUpLocation option:selected').text().trim();
        var dropOffLocationText = $('#DropOffLocation option:selected').text().trim();
        var returnPickUpLocationText = $('#ReturnPickUpLocation option:selected').text().trim();
        var returnDropOffLocationText = $('#ReturnDropOffLocation option:selected').text().trim();

        // Check if any location is 'Other'
        return pickUpLocationText === 'Other' || dropOffLocationText === 'Other' || returnPickUpLocationText === 'Other' || returnDropOffLocationText === 'Other';
    }

    // Attach validation to form submission event
    $('#registrationForm').on('submit', function (e) {
        if (!validateForm()) {
            e.preventDefault(); // Prevent form submission
            alert('Please correct the errors in the form.');
        }
    });
});
