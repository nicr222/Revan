// Function to handle accepting feedback
function acceptFeedback(feedbackId) {
    updateFeedbackStatus(feedbackId, true);
}

// Function to handle rejecting feedback
function rejectFeedback(feedbackId) {
    updateFeedbackStatus(feedbackId, false);
}

// Generic function to update feedback status
function updateFeedbackStatus(feedbackId, isActive) {
    $.post('/Feedback/UpdateStatus', { id: feedbackId, isActive: isActive }, function (data) {
        if (data.success) {
            alert('Feedback status updated.');
            location.reload(); // Reload the page to reflect changes
        } else {
            alert('Error updating feedback.');
        }
    });
}
