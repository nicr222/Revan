
//hiding One way or round trip fields based on the selection
document.addEventListener("DOMContentLoaded", function () {

    // Function to toggle visibility of additional fields
    function toggleAdditionalFields(display) {
        document.querySelectorAll('.additional-fields').forEach(function (element) {
            element.style.display = display;
        });
    }

    // Initially hide the additional fields
    toggleAdditionalFields('none');

    // Event listener for changes in trip type
    document.querySelectorAll('input[name="TripType"]').forEach(function (radio) {
        radio.addEventListener('change', function () {
            if (this.value === 'RoundTrip' || this.value === 'OneWay') {
                toggleAdditionalFields('');
            } else {
                toggleAdditionalFields('none');
            }
        });
    });
});

//Friday Special Request Selection
document.addEventListener("DOMContentLoaded", function () {
    // Existing function to toggle additional fields
    function toggleAdditionalFields(display) {
        document.querySelectorAll('.additional-fields').forEach(function (element) {
            element.style.display = display;
        });
    }

    // New function to toggle Special Request section
    function toggleSpecialRequest(display) {
        document.querySelector('.special-request').style.display = display;
    }

    // Event listener for changes in trip type
    document.querySelectorAll('input[name="TripType"]').forEach(function (radio) {
        radio.addEventListener('change', function () {
            if (this.value === 'Friday') {
                toggleAdditionalFields('none');
                toggleSpecialRequest('');
            } else {
                toggleAdditionalFields('');
                toggleSpecialRequest('none');
            }
        });
    });
});

//Show and hide speical request time preferences section based on the selection
document.addEventListener("DOMContentLoaded", function () {
    // Function to toggle Special Request Time Preferences section
    function toggleSpecialRequestTimePreferences(display) {
        var specialRequestTimePreferences = document.querySelector('.special-request-time-preferences');
        if (specialRequestTimePreferences) {
            specialRequestTimePreferences.style.display = display;
        }
    }

    // Event listener for changes in Special Request radio buttons
    document.querySelectorAll('input[name="SpecialRequest"]').forEach(function (radio) {
        radio.addEventListener('change', function () {
            // Check if "Yes" is selected for Special Request
            if (document.getElementById('specialYes').checked) {
                toggleSpecialRequestTimePreferences('block'); // Show time preferences section
            } else {
                toggleSpecialRequestTimePreferences('none'); // Hide time preferences section
            }
        });
    });

    // Optionally, trigger the change event on page load if "Yes" is already checked
    // This is useful in case the form is reloaded with the "Yes" option preselected
    if (document.getElementById('specialYes').checked) {
        toggleSpecialRequestTimePreferences('block');
    } else {
        toggleSpecialRequestTimePreferences('none');
    }
});

//Hide can leave after field based on Friday Special Request time preferences selection
document.addEventListener('DOMContentLoaded', function () {
    // Function to toggle the visibility of the Can Leave After field
    function toggleCanLeaveAfterVisibility() {
        var specialRequestYes = document.getElementById('specialYes').checked;
        var fridayOneWay = document.getElementById('FridayOneWay').checked;

        // Logic to determine if the Can Leave After field should be hidden
        if (specialRequestYes && fridayOneWay) {
            document.querySelector('.leave-after').style.display = 'none'; // Hides the Can Leave After field
        } else {
            document.querySelector('.leave-after').style.display = 'block'; // Shows the Can Leave After field
        }
    }

    // Attach event listeners to the Special Request and FridayTripType radio buttons
    document.getElementById('specialYes').addEventListener('change', toggleCanLeaveAfterVisibility);
    document.getElementById('specialNo').addEventListener('change', toggleCanLeaveAfterVisibility);
    document.getElementById('FridayRoundTrip').addEventListener('change', toggleCanLeaveAfterVisibility);
    document.getElementById('FridayOneWay').addEventListener('change', toggleCanLeaveAfterVisibility);

    // Initial call to set the correct state when the page loads
    toggleCanLeaveAfterVisibility();
});

