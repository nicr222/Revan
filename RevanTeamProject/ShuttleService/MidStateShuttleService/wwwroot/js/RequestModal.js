$(document).ready(function () {
    // Function to reset the page state
    function resetPageState() {
        // Here you can add code to reset any page state as needed
        // For example, you could reload the page using:
        window.location.reload();
        // or reset specific form fields, etc.
    }

    $('.viewButton').click(function () {
        var name = $(this).closest('tr').find('td:eq(0)').text();
        var phoneNumber = $(this).closest('tr').find('td:eq(1)').text();
        var email = $(this).closest('tr').find('td:eq(2)').text();
        var pickUpTime = $(this).closest('tr').find('td:eq(3)').text();
        var dropOffTime = $(this).closest('tr').find('td:eq(4)').text();
        var details = $(this).closest('tr').find('td:eq(5)').text();

        // Retrieve the full message content from the data attribute
        var fullDetails = $(this).data('full-message');

        console.log('Name: ' + name);
        console.log('Phone Number: ' + phoneNumber);
        console.log('Email: ' + email);
        console.log('Pick up Time: ' + pickUpTime);
        console.log('Drop off Time: ' + dropOffTime);
        console.log('Details: ' + details);
        console.log('Full Details: ' + fullDetails);

        // Update modal content with the data
        $('#specialRequestDetailsContent').html(
            '<div>' +
            '<p><strong>Name:</strong> ' + name + '</p>' +
            '<p><strong>Phone Number:</strong> ' + phoneNumber + '</p>' +
            '<p><strong>Email:</strong> ' + email + '</p>' +
            '<p><strong>Pick up Time:</strong> ' + pickUpTime + '</p>' +
            '<p><strong>Drop off Time:</strong> ' + dropOffTime + '</p>' +
            '<p><strong>Details:</strong> ' + fullDetails + '</p>' + // Displaying the full details
            '</div>'
        );

        // Open the modal
        $('#specialRequestDetailsModal').modal('show');
    });

    // Handle modal close event
    $('#specialRequestDetailsModal').on('hidden.bs.modal', function () {
        // Reset page state when the modal is closed
        resetPageState();
    });
});
