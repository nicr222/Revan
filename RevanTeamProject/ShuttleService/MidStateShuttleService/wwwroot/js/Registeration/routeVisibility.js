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
