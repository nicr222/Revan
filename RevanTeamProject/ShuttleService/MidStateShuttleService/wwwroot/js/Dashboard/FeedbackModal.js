$(document).ready(function () {
    $('.viewButton').click(function () {
        // Retrieve the feedback information from the row
        var customerName = $(this).closest('tr').find('td:eq(0)').text();
        var comment = $(this).closest('tr').find('td:eq(1)').text();
        var rating = $(this).closest('tr').find('td:eq(2)').text();
        var dateSubmitted = $(this).closest('tr').find('td:eq(3)').text();

        // Update modal content with the data
        $('#feedbackDetailsContent').html(
            '<div>' +
            '<p><strong>Customer Name:</strong> ' + customerName + '</p>' +
            '<p><strong>Comment:</strong> ' + comment + '</p>' + // Displaying the full comment
            '<p><strong>Rating:</strong> ' + rating + '</p>' +
            '<p><strong>Date Submitted:</strong> ' + dateSubmitted + '</p>' +
            '</div>'
        );

        // Open the modal
        $('#feedbackDetailsModal').modal('show');
    });
});
