function toggleNotifications(dropdownId) {
    var dropdown = document.getElementById(dropdownId);

    // Check if the dropdown menu is currently displayed
    if (dropdown.style.display === 'none' || dropdown.style.display === '') {
        // Hide any already open dropdowns
        var dropdowns = document.getElementsByClassName('notifications');
        for (var i = 0; i < dropdowns.length; i++) {
            dropdowns[i].style.display = 'none';
        }

        // Show the relevant dropdown
        dropdown.style.display = 'block';
    } else {
        // Hide the dropdown
        dropdown.style.display = 'none';
    }
}
