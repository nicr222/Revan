$(document).ready(function () {
    // Trigger the confirmation modal on form submit attempt
    $('.btn--submit').click(function (e) {
        e.preventDefault(); // Prevent the default form submit action

        // For Initial Route
        var initialRoute = $('input[name="SelectedRouteDetail"]:checked').val();

        // For Return Route
        var returnRoute = $('input[name="ReturnSelectedRouteDetail"]:checked').val();

        // Determine other Special Request selection
        var specialRequestSelection = $('input[name="SpecialRequest"]:checked').val();
        var specialRequestText = specialRequestSelection === 'true' ? 'Yes' : 'No';


        // Adjust the confirmationContent string to include these variables
        var confirmationContent =
            '<p>First Name: ' + $('#FirstName').val() + '</p>' +
            '<p>Last Name: ' + $('#LastName').val() + '</p>' +
            '<p>Email: ' + $('#Email').val() + '</p>' +
            '<p>Phone Number: ' + $('#PhoneNumber').val() + '</p>' +
            '<p>Trip Type: ' + $('[name="TripType"]:checked').val() + '</p>' +
            '<p>Initial Route: ' + initialRoute + '</p>' +
            '<p>Return Route: ' + returnRoute + '</p>' +
            '<p>Special Request: ' + specialRequestText + '</p>' +
            '<p>Contact Preference: ' + $('[name="ContactPreference"]:checked').val() + '</p>';
            // ... Continue for other fields

            $('.modal-body').html(confirmationContent);

        // Show the modal
        $('#confirmationModal').modal('show');
    });

    // Handle the final confirmation button click
    $('#confirmSubmit').click(function () {
        // When the confirmation button is clicked, submit the form
        $('#registrationForm').submit();
    });
});
