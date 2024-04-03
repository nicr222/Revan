$(document).ready(function () {
    var registrationSuccess = $('.notification-bell').data('registration-success') === 'true';
    console.log("Registration Success:", registrationSuccess);

    // The server will set this data attribute to the updated count
    var registrationCount = parseInt($('.notification-bell').data('registration-count')) || 0;


    if (registrationCount > 0) {
        // Update the badge with the new count from the server
        updateNotificationBadge(registrationCount);
        addRegistrationNotification(registrationCount);
    }


});

function updateNotificationBadge(count) {
    let badge = $('.notification-bell .badge');
    badge.text(count);
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

