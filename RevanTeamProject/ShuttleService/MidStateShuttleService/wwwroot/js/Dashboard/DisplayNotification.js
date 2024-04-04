$(document).ready(function () {

    // The server will set this data attribute to the updated count
    var registrationCount = parseInt($('.notification-bell').data('registration-count')) || 0;

    var checkInCount = parseInt($('.notification-bell').data('check-in-count')) || 0;

    // Update the badge with the new counts from the server
    updateNotificationBadge(registrationCount, checkInCount);

    if (registrationCount > 0) {
        addRegistrationNotification(registrationCount);
    }
    
    if (checkInCount > 0) {
        addCheckInNotification(checkInCount);
    }
});

function updateNotificationBadge(registrationCount, checkInCount) {
    // Sum the registration and check-in counts for the badge
    let totalNotifications = registrationCount + checkInCount;
    let badge = $('.notification-bell .badge');
    badge.text(totalNotifications); // Set the badge text to the sum of both counts
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
