$(document).ready(function () {
    // Function to toggle the visibility of the .return-select-route based on radio button selection
    function toggleReturnSelectRouteVisibility() {
        // Check if any of the radio buttons in #routeOptions is checked
        var isAnyRouteSelected = $('#routeOptions input[type="radio"]:checked').length > 0;

        // Show .return-select-route if any radio button is selected, hide otherwise
        if (isAnyRouteSelected) {
            $('.return-select-route').show();
        } else {
            $('.return-select-route').hide();
        }
    }

    // Attach the toggle function to the change event of radio buttons in #routeOptions
    $('#routeOptions').on('change', 'input[type="radio"]', toggleReturnSelectRouteVisibility);

    // Call the function on page load in case a radio button is already checked
    toggleReturnSelectRouteVisibility();
});

$(document).ready(function () {
    // When a route option is selected, update the hidden input's value
    $(document).on('change', 'input[type=radio][name="SelectedRouteDetail"]', function () {
        $('#hiddenSelectedRouteDetail').val(this.value);
    });

    $(document).on('change', 'input[type=radio][name="ReturnSelectedRouteDetail"]', function () {
        $('#hiddenReturnSelectedRouteDetail').val(this.value);
    });
});

$(document).ready(function () {
    // Function to check the selection status of route options and return route options
    function updateVisibilityBasedOnRouteSelection() {
        // Check if both routeOptions and returnRouteOptions have a selected radio button
        var isRouteSelected = $('#routeOptions input[type="radio"]:checked').length > 0;
        var isReturnRouteSelected = $('#returnRouteOptions input[type="radio"]:checked').length > 0;

        // Only if both are selected, hide other-special-request and show schedule-date
        if (isRouteSelected && isReturnRouteSelected) {
            $('.other-special-request').hide();
            $('.schedule-date').show();
        } else {
            // If not both are selected, ensure the schedule-date is hidden and other-special-request is shown/hidden appropriately
            $('.schedule-date').hide();
            // Optional: Decide on the visibility of .other-special-request here based on additional conditions
        }
    }

    // Attach the update function to the change event of radio buttons in both #routeOptions and #returnRouteOptions
    $('#routeOptions, #returnRouteOptions').on('change', 'input[type="radio"]', updateVisibilityBasedOnRouteSelection);

    // Call the function on page load to ensure correct initial state
    updateVisibilityBasedOnRouteSelection();
});
