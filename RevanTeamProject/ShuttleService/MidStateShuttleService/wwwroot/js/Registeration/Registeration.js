
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




