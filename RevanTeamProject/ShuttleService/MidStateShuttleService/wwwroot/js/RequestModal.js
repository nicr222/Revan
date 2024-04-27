$(document).ready(function () {
    $('.viewButton').click(function () {
        var name = $(this).closest('tr').find('td:eq(0)').text();
        var phoneNumber = $(this).closest('tr').find('td:eq(1)').text();
        var email = $(this).closest('tr').find('td:eq(2)').text();
        var pickUpTime = $(this).closest('tr').find('td:eq(3)').text();
        var dropOffTime = $(this).closest('tr').find('td:eq(4)').text();
        var fullDetails = $(this).data('full-message');

        // Update modal content with the data
        $('#specialRequestDetailsContent').html(
            '<div>' +
            '<p><strong>Name:</strong> ' + name + '</p>' +
            '<p><strong>Phone Number:</strong> ' + phoneNumber + '</p>' +
            '<p><strong>Email:</strong> ' + email + '</p>' +
            '<p><strong>Pick up Time:</strong> ' + pickUpTime + '</p>' +
            '<p><strong>Drop off Time:</strong> ' + dropOffTime + '</p>' +
            '<p><strong>Full Details:</strong> ' + fullDetails + '</p>' + // Displaying the full details
            '</div>'
        );

        // Open the modal
        $('#specialRequestDetailsModal').modal('show');
    });

    // Handle modal close event
    $('#specialRequestDetailsModal').on('hidden.bs.modal', function () {
        // Here you can add code to reset any page state as needed
        // For example, you could reload the page using:
        window.location.reload();
        // or reset specific form fields, etc.
    });
});
