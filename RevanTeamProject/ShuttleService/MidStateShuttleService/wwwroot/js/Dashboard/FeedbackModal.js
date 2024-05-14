$(document).ready(function () {
    function resetPageState() {
        // Reset page state here, e.g., reload the page
        window.location.reload();
    }

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

        // Handle modal close event
        
    });
    $('#feedbackDetailsModal').on('hidden.bs.modal', function () {
        // Here you can add code to reset any page state as needed
        // For example, you could reload the page using:
        resetPageState();
        // or reset specific form fields, etc.
    });
});

