//$(document).ready(function () {
//    // Trigger the confirmation modal on form submit attempt
//    $('.btn--submit').click(function (e) {
//        e.preventDefault(); // Prevent the default form submit action

//        // For Initial Route
//        var initialRoute = $('input[name="SelectedRouteDetail"]:checked').val();

//        // For Return Route
//        var returnRoute = $('input[name="ReturnSelectedRouteDetail"]:checked').val();

//        // Determine other Special Request selection
//        var specialRequestSelection = $('input[name="SpecialRequest"]:checked').val();
//        var specialRequestText = specialRequestSelection === 'true' ? 'Yes' : 'No';


//        // Adjust the confirmationContent string to include these variables
//        var confirmationContent =
//            '<p>First Name: ' + $('#FirstName').val() + '</p>' +
//            '<p>Last Name: ' + $('#LastName').val() + '</p>' +
//            '<p>Email: ' + $('#Email').val() + '</p>' +
//            '<p>Phone Number: ' + $('#PhoneNumber').val() + '</p>' +
//            '<p>Trip Type: ' + $('[name="TripType"]:checked').val() + '</p>' +
//            '<p>Initial Route: ' + initialRoute + '</p>' +
//            '<p>Return Route: ' + returnRoute + '</p>' +
//            '<p>Special Request: ' + specialRequestText + '</p>' +
//            '<p>Contact Preference: ' + $('[name="ContactPreference"]:checked').val() + '</p>';
//            // ... Continue for other fields

//            $('.modal-body').html(confirmationContent);

//        // Show the modal
//        $('#confirmationModal').modal('show');
//    });

//    // Handle the final confirmation button click
//    $('#confirmSubmit').click(function () {
//        // When the confirmation button is clicked, submit the form
//        $('#registrationForm').submit();
//    });
//});

$(document).ready(function () {
    $('.btn--submit').click(function (e) {
        e.preventDefault(); // Prevent form submit

        // Gather form data
        var tripType = $('[name="TripType"]:checked').val();
        var specialRequest = $('input[name="SpecialRequest"]:checked').val() === 'true';
        var pickUpLocationID = $('#PickUpLocationID').val() || 'Not specified';
        var dropOffLocationID = $('#DropOffLocationID').val() || 'Not specified';
        var firstName = $('#FirstName').val();
        var lastName = $('#LastName').val();
        var email = $('#Email').val();
        var phoneNumber = $('#PhoneNumber').val();
        var specialPickUpLocation = $('#SpecialPickUpLocation').val() || 'Not specified';
        var specialDropOffLocation = $('#SpecialDropOffLocation').val() || 'Not specified';
        var mustArriveTime = $('#MustArriveTime').val() || 'Not specified';
        var canLeaveTime = $('#CanLeaveTime').val() || 'Not specified';
        var contactPreference = $('[name="ContactPreference"]:checked').val();

        var initialRoute = $('input[name="SelectedRouteDetail"]:checked').val();
        var returnRoute = $('input[name="ReturnSelectedRouteDetail"]:checked').val();

        var selectedDaysOfWeek = [];
        $('input[name="SelectedDaysOfWeek"]:checked').each(function () {
            selectedDaysOfWeek.push($(this).val());
        });
        var daysOfWeekFormatted = selectedDaysOfWeek.join(', ');

        var firstDayExpectingToRide = $('#FirstDayExpectingToRide').val() || 'Not specified';

        // Start building the confirmationContent
        var confirmationContent = `
            <p>First Name: ${firstName}</p>
            <p>Last Name: ${lastName}</p>
            <p>Email: ${email}</p>
            <p>Phone Number: ${phoneNumber}</p>
            <p>Trip Type: ${tripType}</p>
            <p>Special Request: ${specialRequest ? 'Yes' : 'No'}</p>
        `;

        //Round trip No Speical Request
        if (tripType === 'RoundTrip') {
            confirmationContent += `
                <p>Pick Up Location ID: ${pickUpLocationID}</p>
                <p>Drop Off Location ID: ${dropOffLocationID}</p>
            `;
            if (!specialRequest) {
                confirmationContent += `
                    <p>Initial Route: ${initialRoute}</p>
                    <p>Return Route: ${returnRoute}</p>
                    <p>Days of the Week Needed: ${daysOfWeekFormatted}</p>
                    <p>First Day Expecting to Ride: ${firstDayExpectingToRide}</p>
                `;
            }
        }

        //One Way No Speical Request
        if (tripType === 'OneWay') {
            confirmationContent += `
                <p>Pick Up Location ID: ${pickUpLocationID}</p>
                <p>Drop Off Location ID: ${dropOffLocationID}</p>
            `;
            if (!specialRequest) {
                confirmationContent += `
                    <p>Initial Route: ${initialRoute}</p>
                    <p>Days of the Week Needed: ${daysOfWeekFormatted}</p>
                    <p>First Day Expecting to Ride: ${firstDayExpectingToRide}</p>
                `;
            }
        }

        if (specialRequest) {
            confirmationContent += `
                <p>Special Pick Up Location: ${specialPickUpLocation}</p>
                <p>Special Drop Off Location: ${specialDropOffLocation}</p>
                <p>Must Arrive Time: ${mustArriveTime}</p>
                <p>Can Leave Time: ${canLeaveTime}</p>
            `;
        }

        confirmationContent += `<p>Contact Preference: ${contactPreference}</p>`;

        // Display the constructed confirmation content in the modal body
        $('.modal-body').html(confirmationContent);

        // Show the modal
        $('#confirmationModal').modal('show');
    });

    // Handle the final confirmation button click
    $('#confirmSubmit').click(function () {
        // Actual form submission
        $('#registrationForm').submit();
    });
});
