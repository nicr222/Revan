window.addEventListener("load", function () {
    // Retrieve radio elements
    var roundTripSelect = document.getElementById('RoundTrip');
    var oneWaySelect = document.getElementById('OneWay');
    var fridaySelect = document.getElementById('Friday');

    if (roundTripSelect.checked) {
        ToggleRoundTrip();
    }
    else if (oneWaySelect.checked) {
        ToggleOneWay();
    }
    else if (fridaySelect.checked) {
        ToggleFriday();
    }
});

function ToggleRoundTrip() {
    // Retrieve the div containers
    var theBigOne = document.getElementById('TheBigOne');
    var returnSelect = document.getElementById('ReturnDiv');
    var fridayInfo = document.getElementById('FridayDiv');

    // hide Friday if applicable
    fridayInfo.style.display = "none";
    // Show the big one
    theBigOne.style.display = "block";
    // Make sure return select is showing as well.
    returnSelect.style.display = "block";
}

function ToggleOneWay() {
    // Retrieve the div containers
    var returnSelect = document.getElementById('ReturnDiv');

    // Hide friday and show big one
    ToggleRoundTrip();

    // Hide the return routes
    returnSelect.style.display = "none";
}

function ToggleFriday() {
    // Retrieve the div containers
    var theBigOne = document.getElementById('TheBigOne');
    var fridayInfo = document.getElementById('FridayDiv');

    // hide the big one
    theBigOne.style.display = "none";

    // Show Friday div
    fridayInfo.style.display = "block";
}