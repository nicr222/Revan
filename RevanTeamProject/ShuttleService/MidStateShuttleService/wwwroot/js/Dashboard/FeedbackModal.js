$(document).ready(function () {
    $('.viewButton').click(function () {
        // Retrieve the feedback information from the data attributes
        var customerName = $(this).data('customer-name');
        var comment = $(this).data('comment'); // Get the full comment from data attribute
        var rating = $(this).data('rating');
        var shareTestimonial = $(this).data('share-testimonial');
        var dateSubmitted = $(this).data('date-submitted');

        // Update modal content with the data
        $('#feedbackDetailsContent').html(
            '<div>' +
            '<p><strong>Customer Name:</strong> ' + customerName + '</p>' +
            '<p><strong>Comment:</strong> ' + comment + '</p>' + // Use the full comment
            '<p><strong>Rating:</strong> ' + rating + '</p>' +
            '<p><strong>Share Testimonial:</strong> ' + shareTestimonial + '</p>' +
            '<p><strong>Date Submitted:</strong> ' + dateSubmitted + '</p>' +
            '</div>'
        );

        // Open the modal
        $('#feedbackDetailsModal').modal('show');
    });
});

