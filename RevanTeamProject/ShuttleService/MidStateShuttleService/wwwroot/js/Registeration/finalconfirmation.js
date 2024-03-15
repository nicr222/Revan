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
        e.preventDefault(); // Prevent the default form submit action

        // Gather form data
        var tripType = $('[name="TripType"]:checked').val();
        var specialRequest = $('input[name="SpecialRequest"]:checked').val() === 'true';
        var initialRoute = $('input[name="SelectedRouteDetail"]:checked').val() || 'Not specified';
        var returnRoute = $('input[name="ReturnSelectedRouteDetail"]:checked').val() || 'Not specified';
        var pickUpLocationID = $('#PickUpLocationID').val() || 'Not specified';
        var dropOffLocationID = $('#DropOffLocationID').val() || 'Not specified';
        var specialPickUpLocation = $('#SpecialPickUpLocation').val() || 'Not specified';
        var specialDropOffLocation = $('#SpecialDropOffLocation').val() || 'Not specified';
        var mustArriveTime = $('#MustArriveTime').val() || 'Not specified';
        var canLeaveTime = $('#CanLeaveTime').val() || 'Not specified';

        var confirmationContent = `
            <p>First Name: ${$('#FirstName').val()}</p>
            <p>Last Name: ${$('#LastName').val()}</p>
            <p>Email: ${$('#Email').val()}</p>
            <p>Phone Number: ${$('#PhoneNumber').val()}</p>
            <p>Trip Type: ${tripType}</p>
            <p>Special Request: ${specialRequest ? 'Yes' : 'No'}</p>
        `;

        // Conditions based on Trip Type and Special Requests
        if (tripType === 'RoundTrip') {
            confirmationContent += `<p>Initial Route: ${initialRoute}</p>`;
            confirmationContent += `<p>Return Route: ${returnRoute}</p>`;
        } else if (tripType === 'OneWay') {
            confirmationContent += `<p>Route Detail: ${initialRoute}</p>`;
        }

        // Additional conditions for Special Pickup or Dropoff locations
        if (specialRequest) {
            confirmationContent += `
                <p>Special Pickup Location: ${specialPickUpLocation}</p>
                <p>Special Dropoff Location: ${specialDropOffLocation}</p>
                <p>Must Arrive Time: ${mustArriveTime}</p>
                <p>Can Leave Time: ${canLeaveTime}</p>
            `;
        }

        // Handling the location 'Other' option based on IDs (simplified logic)
        if (pickUpLocationID === 'Other' || dropOffLocationID === 'Other') {
            confirmationContent += `<p>Pick Up Location ID: ${pickUpLocationID}</p>`;
            confirmationContent += `<p>Drop Off Location ID: ${dropOffLocationID}</p>`;
        }

        // Display the constructed confirmation content
        $('.modal-body').html(confirmationContent);
        $('#confirmationModal').modal('show');
    });

    $('#confirmSubmit').click(function () {
        $('#registrationForm').submit();
    });
});
