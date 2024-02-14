$(document).ready(function () {
    // Trigger the confirmation modal on form submit attempt
    $('.btn--submit').click(function (e) {
        e.preventDefault(); // Prevent the default form submit action

        // Populate modal with form data
        var confirmationContent = '<p>User ID: ' + $('#UserId').val() + '</p>' +
            '<p>First Name: ' + $('#FirstName').val() + '</p>' +
            '<p>Last Name: ' + $('#LastName').val() + '</p>' +
            '<p>Email: ' + $('#Email').val() + '</p>' +
            '<p>Phone Number: ' + $('#PhoneNumber').val() + '</p>' +
            '<p>Trip Type: ' + $('[name="TripType"]:checked').val() + '</p>' +
            '<p>Pick-Up Location: ' + $('#PickUpLocation option:selected').text() + '</p>' +
            '<p>Drop-Off Location: ' + $('#DropOffLocation option:selected').text() + '</p>' +
            '<p>Date: ' + $('#Date').val() + '</p>' +
            '<p>Time: ' + $('#Time').val() + '</p>' +
            '<p>Contact Preference: ' + $('[name="ContactPreference"]:checked').val() + '</p>';

            // ... Continue for other fields

            $('.modal-body').html(confirmationContent);

        // Show the modal
        $('#confirmationModal').modal('show');
    });

    // Handle the final confirmation button click
    $('#confirmSubmit').click(function () {
        // When the confirmation button is clicked, submit the form
        $('form').submit();
    });
});
