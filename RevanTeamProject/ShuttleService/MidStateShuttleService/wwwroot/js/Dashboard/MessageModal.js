$(document).ready(function () {
    // Event listener for the "View" link
    $('.viewButton').click(function () {
        // Retrieve message details from data attributes
        var messageId = $(this).attr('asp-route-id');
        var fullMessage = $(this).data('full-message');

        // Populate modal with message details
        $('#messageDetailsContent').html('<strong>ID:</strong> ' + messageId + '<br><strong>Message:</strong> ' + fullMessage);

        // Show the modal
        $('#messageDetailsModal').modal('show');
    });
});