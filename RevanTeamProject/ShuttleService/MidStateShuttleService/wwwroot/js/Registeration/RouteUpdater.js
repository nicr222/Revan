////function to fetch and display route options based on selected pick-up and drop-off locations .
//$(document).ready(function () {
//    function updateRoutes() {
//        // Get the value of the selected pick-up and drop-off location from the dropdown.
//        var pickUpLocationId = $('#PickUpLocation').val();
//        var dropOffLocationId = $('#DropOffLocation').val();

//        // Check if both pick-up and drop-off locations are selected.
//        if (pickUpLocationId && dropOffLocationId) {
//            // Perform a fetch request to get routes based on selected locations
//            fetch('/Register/GetRoutes?pickUpLocationId=' + pickUpLocationId + '&dropOffLocationId=' + dropOffLocationId)
//                .then(response => {
//                    // Check if the response is ok (status in the range 200-299).
//                    if (!response.ok) {
//                        throw new Error('Network response was not ok');
//                    }
//                    // Parse the response body as JSON.
//                    return response.json();
//                })
//                .then(routes => {
//                    console.log("Routes:", routes); // Debugging line to inspect the routes data

//                    // Create Select Route and route-info HTML
//                    var routeSelectHtml = '<p class="route-type">Select Route </p>';
//                    var routeOptionsHtml = '';
//                    var routeformlineHtml = '<div class="form-line"></div>';
//                    var routeInfoHtml = '<p class="route-info">If these routes do not meet your needs, please submit a special request.</p>';

//                    // Iterate over each route in the routes array.
//                    routes.forEach(function (route) {
//                        console.log("Individual route object:", route); // Log the entire route object
//                        // Append an HTML string for each route option to the routeOptionsHtml string.
//                        routeOptionsHtml +=
//                            //'<label><input type="radio" name="SelectedRouteDetail" value="' + route.routeID + '">' +
//                        //'<span>' + route.detail + '</span></label><br>';
//                            '<label><input type="radio" name="SelectedRouteDetail" value="' + route.detail + '">' +
//                            '<span>' + route.detail + '</span></label><br>';

//                    });

//                    console.log("Generated HTML:", routeOptionsHtml); // Log the generated HTML
//                    // Insert the generated HTML into the page.
//                    $('#routeOptions').html(routeSelectHtml + routeOptionsHtml + routeformlineHtml + routeInfoHtml);
//                })
//                .catch(error => {
//                    console.error('There has been a problem with your fetch operation:', error);
//                    $('#routeOptions').html('<p>Error loading routes. Please try again.</p>');
//                });

//            // Show the special request section when both locations are selected.
//            $('.other-special-request').show();
//        } else {
//            // If either pick-up or drop-off location is not selected, clear the route options.
//            $('#routeOptions').empty();

//            $('.other-special-request').hide();
//        }
//    }

//    // Attach the updateRoutes function as an event handler for the change event on both location dropdowns.
//    $('#PickUpLocation, #DropOffLocation').change(updateRoutes);
//});

$(document).ready(function () {
    function updateRoutesAndSpecialRequests() {
        var pickUpLocationSelect = $('#PickUpLocation');
        var dropOffLocationSelect = $('#DropOffLocation');
        var pickUpLocationId = pickUpLocationSelect.val();
        var dropOffLocationId = dropOffLocationSelect.val();
        var otherSpecialRequestSection = $('.other-special-request');

        // Function to toggle the visibility of special request based on locations
        function toggleSpecialRequestDisplay() {
            var pickUpSelectedOptionText = pickUpLocationSelect.find('option:selected').text();
            var dropOffSelectedOptionText = dropOffLocationSelect.find('option:selected').text();
            var isPickUpOther = pickUpSelectedOptionText.toLowerCase() === 'other';
            var isDropOffOther = dropOffSelectedOptionText.toLowerCase() === 'other';

            // Determine if either location is 'Other' or both locations are selected
            var showSpecialRequest = isPickUpOther || isDropOffOther || (pickUpLocationId && dropOffLocationId);

            if (showSpecialRequest) {
                otherSpecialRequestSection.show();
            } else {
                otherSpecialRequestSection.hide();
            }
        }

        toggleSpecialRequestDisplay(); // Call this function to handle initial state or changes.

        // Fetch and display routes if both pick-up and drop-off locations are selected
        if (pickUpLocationId && dropOffLocationId) {
            fetch('/Register/GetRoutes?pickUpLocationId=' + pickUpLocationId + '&dropOffLocationId=' + dropOffLocationId)
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    return response.json();
                })
                .then(routes => {
                    console.log("Routes:", routes);

                    var routeSelectHtml = '<p class="route-type">Select Route </p>';
                    var routeOptionsHtml = '';
                    var routeformlineHtml = '<div class="form-line"></div>';
                    var routeInfoHtml = '<p class="route-info">If these routes do not meet your needs, please submit a special request.</p>';

                    routes.forEach(function (route) {
                        routeOptionsHtml += '<label><input asp-for="RouteID" type="radio" name="SelectedRouteDetail" value="' + route.routeID + '">' +
                            '<span>' + route.detail + '</span></label><br>';
                    });

                    $('#routeOptions').html(routeSelectHtml + routeOptionsHtml + routeformlineHtml + routeInfoHtml);
                })
                .catch(error => {
                    console.error('There has been a problem with your fetch operation:', error);
                    $('#routeOptions').html('<p>Error loading routes. Please try again.</p>');
                });
        } else {
            $('#routeOptions').empty();
            // Hide only if none of the locations are selected
            if (!pickUpLocationId && !dropOffLocationId) {
                otherSpecialRequestSection.hide();
            }
        }
    }

    // Attach the updateRoutesAndSpecialRequests function as an event handler
    $('#PickUpLocation, #DropOffLocation').change(updateRoutesAndSpecialRequests);

    // Initial call to set the correct state based on the current selection
    updateRoutesAndSpecialRequests();
});


// Hide route location schedule when 'Other' is selected as pick-up or drop-off location
$(document).ready(function () {
    function checkLocations() {
        var pickUpLocation = $('#PickUpLocation').find(":selected").text();
        var dropOffLocation = $('#DropOffLocation').find(":selected").text();

        // Check if either selected location is 'Other' (case insensitive)
        if (pickUpLocation.toLowerCase() === 'other' || dropOffLocation.toLowerCase() === 'other') {
            $('.route-location-schedule').hide();
        } else {
            $('.route-location-schedule').show();
        }
    }

    // Attach the checkLocations function as an event handler for the change event on both location dropdowns.
    $('#PickUpLocation, #DropOffLocation').change(checkLocations);

    // Initial check in case the 'Other' option is selected by default when the page loads
    checkLocations();
});

