
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
        <div class="notification-item message-notification">
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
        <div class="notification-item checkin-notification">
            <i class="bi bi-exclamation-circle text-warning"></i>
            <div>
                <h4>New Check-In (${count})</h4>
                <p>New check-in processed!</p>
            </div>
        </div>`;
    notificationDropdown.prepend(newNotificationHtml);
}

//function addMessageNotification(count, message = 'You have a new message!') {
//    console.log('Adding message notification with count:', count);
//    let notificationDropdown = $('#notificationMessageDropdown');
//    let newNotificationHtml = `
//        <div class="notification-item message-notification">
//            <i class="bi bi-exclamation-circle text-warning"></i>
//            <div>
//                <h4>New Message (${count})</h4>
//                <p>${message}</p>
//            </div>
//        </div>`;
//    notificationDropdown.prepend(newNotificationHtml);
//    // Make sure the dropdown is visible if it was hidden
//    notificationDropdown.show();
//}
function addMessageNotification(count, message = 'You have a new message!') {
    console.log('Adding message notification with count:', count);
    let notificationDropdown = $('#notificationMessageDropdown');
    let newNotificationHtml = `
        <div class="notification-item message-notification" data-count="${count}">
            <i class="bi bi-exclamation-circle text-warning"></i>
            <div>
                <h4>New Message (${count})</h4>
                <p>${message}</p>
            </div>
        </div>`;
    notificationDropdown.prepend(newNotificationHtml);
}

function addFeedbackNotification(count, message = 'You have new feedback!') {
    console.log('Adding feedback notification with count:', count);
    let notificationDropdown = $('#notificationMessageDropdown');
    let newNotificationHtml = `
        <div class="notification-item feedback-notification" data-count="${count}">
            <i class="bi bi-exclamation-circle text-warning"></i>
            <div>
                <h4>New Feedback (${count})</h4>
                <p>${message}</p>
            </div>
        </div>`;
    notificationDropdown.prepend(newNotificationHtml);
}

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
        addFeedbackNotification(feedbackCount, lastFeedback);
    }
});

// Event delegation for dynamic content
//$(document).on('click', '.feedback-notification', function () {
//    var feedbackUrl = $(this).data('url'); // Assuming data-url attribute is set with the feedback URL
//    console.log('Feedback notification clicked', feedbackUrl);
//    if (feedbackUrl) {
//        window.location.href = feedbackUrl;
//    }
//});


// Event delegation for dynamic content Working
$(document).on('click', '.feedback-notification', function () {
    var feedbackUrl = $(this).data('url'); // Assuming data-url attribute is set with the feedback URL
    console.log('Feedback notification clicked', feedbackUrl);

    // Clear feedback count
    $('.notification-message').data('feedback-count', 0);
    updateMessageNotificationBadge(0, 0); // Assuming message count should also be reset

    if (feedbackUrl) {
        window.location.href = feedbackUrl;
    }

    location.reload(true);
});

$(document).on('click', '.message-notification', function () {
    var messageUrl = $(this).data('url'); // Fetch the URL from data attribute
    console.log('Message notification clicked', messageUrl);

    // Clear feedback count
    $('.notification-message').data('message-count', 0);
    updateMessageNotificationBadge(0, 0); // Assuming message count should also be reset

    if (messageUrl) {
        window.location.href = messageUrl; // Redirect to the message page
    }
    location.reload(true);

});

// Event delegation for dynamic content working by using AJAX
//$(document).on('click', '.feedback-notification', function () {
//    // Assuming data-url attribute is set with the feedback URL
//    var feedbackUrl = $(this).data('url');
//    console.log('Feedback notification clicked', feedbackUrl);

//    // Trigger server-side update for feedback count
//    $.ajax({
//        url: '/Dashboard/FeedbackClicked', // Assuming you have a route set for this
//        success: function () {
//            window.location.href = feedbackUrl; // Redirect to the dashboard or specific URL
//            location.reload(true); // Force reload to ensure all data is up to date
//        }
//    });
//});



// Define the function to set up the event handlers
function setupEventHandlers() {
    // Use event delegation for dynamically loaded content
    $(document).on('click', '.feedback-notification', function () {
        console.log('Feedback notification clicked');

        console.log(feedbackUrl);
        window.location.href = feedbackUrl; // Use the variable here
    });

    $(document).on('click', '.message-notification', function () {
        console.log('Message notification clicked');

        console.log(messageUrl);
        window.location.href = messageUrl; // Use the variable here
    });

    // ...set up other event handlers here as needed...
}

// Call the function on initial load
setupEventHandlers();








