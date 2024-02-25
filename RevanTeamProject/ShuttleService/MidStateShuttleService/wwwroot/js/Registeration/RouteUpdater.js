//function to fetch and display route options based on selected pick-up and drop-off locations .
$(document).ready(function () {
    function updateRoutes() {
        // Get the value of the selected pick-up and drop-off location from the dropdown.
        var pickUpLocationId = $('#PickUpLocation').val();
        var dropOffLocationId = $('#DropOffLocation').val();

        // Check if both pick-up and drop-off locations are selected.
        if (pickUpLocationId && dropOffLocationId) {
            // Perform a fetch request to get routes based on selected locations
            fetch('/Register/GetRoutes?pickUpLocationId=' + pickUpLocationId + '&dropOffLocationId=' + dropOffLocationId)
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
                    var routeInfoHtml = '<p class="route-info">Friday routes are by reservation only and can be customized for your schedule.</p>';

                    // Iterate over each route in the routes array.
                    routes.forEach(function (route) {
                        console.log("Individual route object:", route); // Log the entire route object
                        // Append an HTML string for each route option to the routeOptionsHtml string.
                        routeOptionsHtml +=
                            '<label><input type="radio" name="RouteID" value="' + route.routeID + '">' +
                            '<span>' + route.detail + '</span></label><br>';
                    });

                    console.log("Generated HTML:", routeOptionsHtml); // Log the generated HTML
                    // Insert the generated HTML into the page.
                    $('#routeOptions').html(routeSelectHtml + routeOptionsHtml + routeInfoHtml);
                })
                .catch(error => {
                    console.error('There has been a problem with your fetch operation:', error);
                    $('#routeOptions').html('<p>Error loading routes. Please try again.</p>');
                });
        } else {
            // If either pick-up or drop-off location is not selected, clear the route options.
            $('#routeOptions').empty();
        }
    }

    // Attach the updateRoutes function as an event handler for the change event on both location dropdowns.
    $('#PickUpLocation, #DropOffLocation').change(updateRoutes);
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

