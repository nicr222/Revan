function toggleNotifications(dropdownId) {
    var $dropdown = $('#' + dropdownId);

    // Hide any already open dropdowns
    $('.dropdown-menu.notifications').not($dropdown).hide();

    // Toggle the display of the dropdown
    $dropdown.toggle();
}

