
$(document).ready(function () {
    function updateRoutes() {
        // Get the value of the selected pick-up and drop-off location from the dropdown.
        var returnPickUpLocationId = $('#ReturnPickUpLocation').val();
        var returnDropOffLocationId = $('#ReturnDropOffLocation').val();

        // Check if both pick-up and drop-off locations are selected.
        if (returnPickUpLocationId && returnDropOffLocationId) {
            // Perform a fetch request to get routes based on selected locations
            fetch('/Register/ReturnGetRoutes?returnpickUpLocationId=' + returnPickUpLocationId + '&returndropOffLocationId=' + returnDropOffLocationId)
                .then(response => {
                    // Check if the response is ok (status in the range 200-299).
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    // Parse the response body as JSON.
                    return response.json();
                })
                .then(routes => {
                    console.log("Routes:", routes); // Debugging line to inspect the routes data

                    // Create Select Route and route-info HTML
                    var routeSelectHtml = '<p class="route-type">Select Route </p>';
                    var routeOptionsHtml = '';
                    var routeformlineHtml = '<div class="form-line"></div>';
                    var routeInfoHtml = '<p class="route-info">If these routes do not meet your needs, please submit a special request.</p>';

                    // Iterate over each route in the routes array.
                    routes.forEach(function (route) {
                        console.log("Individual route object:", route); // Log the entire route object
                        // Append an HTML string for each route option to the routeOptionsHtml string.
                        routeOptionsHtml +=
                            '<label><input type="radio" name="ReturnRouteID" value="' + route.routeID + '">' +
                            '<span>' + route.detail + '</span></label><br>';
                    });

                    console.log("Generated HTML:", routeOptionsHtml); // Log the generated HTML
                    // Insert the generated HTML into the page.
                    $('#returnRouteOptions').html(routeSelectHtml + routeOptionsHtml + routeformlineHtml + routeInfoHtml);
                })
                .catch(error => {
                    console.error('There has been a problem with your fetch operation:', error);
                    $('#returnRouteOptions').html('<p>Error loading routes. Please try again.</p>');
                });

            // Show the special request section when both locations are selected.
            /*            $('.other-special-request').show();*/
        } else {
            // If either pick-up or drop-off location is not selected, clear the route options.
            $('#returnRouteOptions').empty();

            /*            $('.other-special-request').hide();*/
        }
    }

    // Attach the updateRoutes function as an event handler for the change event on both location dropdowns.
    $('#ReturnPickUpLocation, #ReturnDropOffLocation').change(updateRoutes);
});