$(document).ready(function () {
    function resetPageState() {
        // Reset page state here, e.g., reload the page
        window.location.reload();
    }

    

    // Your existing code for opening the modal and populating it with data
    $('.viewButton').click(function () {
        var name = $(this).closest('tr').find('td:eq(0)').text();
        var message = $(this).closest('tr').find('td:eq(1)').text();
        var responseRequired = $(this).closest('tr').find('td:eq(2)').text();
        var contactInfo = $(this).closest('tr').find('td:eq(3)').text();

        // Retrieve the full message content from the data attribute
        var fullMessage = $(this).data('full-message');

        console.log('Name: ' + name);
        console.log('Message: ' + message);
        console.log('Response Required: ' + responseRequired);
        console.log('Contact Info: ' + contactInfo);
        console.log('Full Message: ' + fullMessage);

        // Update modal content with the data
        $('#messageDetailsContent').html(
            '<div>' +
            '<p><strong>Name:</strong> ' + name + '</p>' +
            '<p><strong>Message:</strong> ' + fullMessage + '</p>' + // Displaying the full message
            '<p><strong>Response Required:</strong> ' + responseRequired + '</p>' +
            '<p><strong>Contact Info:</strong> ' + contactInfo + '</p>' +
            '</div>'
        );

        // Open the modal
        $('#messageDetailsModal').modal('show');
    });

    // Event handler for modal close event
    $('#messageDetailsModal').on('hidden.bs.modal', function () {
        // Reset page state when the modal is closed
        resetPageState();
    });
});
