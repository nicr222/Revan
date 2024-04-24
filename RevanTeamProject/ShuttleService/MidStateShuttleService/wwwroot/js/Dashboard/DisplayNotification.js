$(document).ready(function () {

    // The server will set this data attribute to the updated count
    var registrationCount = parseInt($('.notification-bell').data('registration-count')) || 0;

    var checkInCount = parseInt($('.notification-bell').data('check-in-count')) || 0;

    var messageCount = parseInt($('.notification-message').data('message-count')) || 0;
    var lastMessage = $('.notification-message').data('last-message') || 'You have a new message!';

    var feedbackCount = parseInt($('.notification-message').data('feedback-count')) || 0;
    var lastFeedback = $('.notification-message').data('last-feedback') || 'You have new feedback!';

    // Update the message icon badge with the sum of messages and feedback
    updateMessageNotificationBadge(messageCount, feedbackCount);

    // Update the badge with the new counts from the server
    updateNotificationBadge(registrationCount, checkInCount);

    if (registrationCount > 0) {
        addRegistrationNotification(registrationCount);
    }
    
    if (checkInCount > 0) {
        addCheckInNotification(checkInCount);
    }

    if (messageCount > 0) {
        addMessageNotification(messageCount, lastMessage);
    }

    if (feedbackCount > 0) {
        addMessageNotification(feedbackCount, lastFeedback);
    }
});

function updateNotificationBadge(registrationCount, checkInCount) {
    // Sum the registration and check-in counts for the badge
    let totalNotifications = registrationCount + checkInCount;
    let badge = $('.notification-bell .badge');
    badge.text(totalNotifications); // Set the badge text to the sum of both counts
}

function updateMessageNotificationBadge(messageCount, feedbackCount) {
    let totalNotifications = messageCount + feedbackCount;
    let badge = $('.notification-message .badge');
    // Set the text of the badge to the total count
    badge.text(totalNotifications);
}

function addRegistrationNotification(count) {
    console.log('Adding notification with count:', count);
    let notificationDropdown = $('#notificationBellDropdown');
    let newNotificationHtml = `
        <div class="notification-item">
            <i class="bi bi-exclamation-circle text-warning"></i>
            <div>
                <h4>New Registration (${count})</h4>
                <p>New registration received!</p>
            </div>
        </div>`;
    notificationDropdown.prepend(newNotificationHtml);
    // Make sure the dropdown is visible if it was hidden
    notificationDropdown.show();
}

function addCheckInNotification(count) {
    console.log('Adding check-in notification with count:', count);
    let notificationDropdown = $('#notificationBellDropdown');
    let newNotificationHtml = `
        <div class="notification-item">
            <i class="bi bi-exclamation-circle text-warning"></i>
            <div>
                <h4>New Check-In (${count})</h4>
                <p>New check-in processed!</p>
            </div>
        </div>`;
    notificationDropdown.prepend(newNotificationHtml);
}

function addMessageNotification(count, message = 'You have a new message!') {
    console.log('Adding message notification with count:', count);
    let notificationDropdown = $('#notificationMessageDropdown');
    let newNotificationHtml = `
        <div class="notification-item">
            <i class="bi bi-exclamation-circle text-warning"></i>
            <div>
                <h4>New Message (${count})</h4>
                <p>${message}</p>
            </div>
        </div>`;
    notificationDropdown.prepend(newNotificationHtml);
    // Make sure the dropdown is visible if it was hidden
    notificationDropdown.show();
}

function addFeedbackNotification(count, message = 'You have a new message!') {
    console.log('Adding message notification with count:', count);
    let notificationDropdown = $('#notificationMessageDropdown');
    let newNotificationHtml = `
        <div class="notification-item">
            <i class="bi bi-exclamation-circle text-warning"></i>
            <div>
                <h4>New Message (${count})</h4>
                <p>${message}</p>
            </div>
        </div>`;
    notificationDropdown.prepend(newNotificationHtml);
    // Make sure the dropdown is visible if it was hidden
    notificationDropdown.show();
}


