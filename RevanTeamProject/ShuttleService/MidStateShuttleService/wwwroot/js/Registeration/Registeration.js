
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

//Other Special Request Selection
document.addEventListener('DOMContentLoaded', function () {
    var pickUpLocationSelect = document.getElementById('PickUpLocation');
    var dropOffLocationSelect = document.getElementById('DropOffLocation');
    var otherSpecialRequestSection = document.querySelector('.other-special-request');

    function toggleSpecialRequestDisplay() {
        // Check if either of the dropdowns has "Other" selected
        if (pickUpLocationSelect.value === 'Other' || dropOffLocationSelect.value === 'Other') {
            otherSpecialRequestSection.style.display = 'block';
        } else {
            otherSpecialRequestSection.style.display = 'none';
        }
    }

    // Add change event listeners to both dropdowns
    pickUpLocationSelect.addEventListener('change', toggleSpecialRequestDisplay);
    dropOffLocationSelect.addEventListener('change', toggleSpecialRequestDisplay);

    // Initial check in case the page is loaded with "Other" already selected
    toggleSpecialRequestDisplay();
});

//Other Special Request Selection
document.addEventListener('DOMContentLoaded', function () {
    // Select the radio buttons for "Other Special Request"
    const otherSpecialYes = document.getElementById('otherSpecialYes');
    const otherSpecialNo = document.getElementById('otherSpecialNo');

    // Function to toggle visibility based on the "Other Special Request" selection
    function toggleVisibility(show) {
        // Select the elements to show/hide
        const otherSpecialRequestTimePreferences = document.querySelector('.other-special-request-time-preferences');

        if (show) {
            // Show elements
            otherSpecialRequestTimePreferences.style.display = '';
        } else {
            // Hide elements
            otherSpecialRequestTimePreferences.style.display = 'none';
        }
    }

    // Initialize with current selection
    toggleVisibility(otherSpecialYes.checked);

    // Add event listeners to the radio buttons
    otherSpecialYes.addEventListener('change', function () {
        toggleVisibility(this.checked);
    });

    otherSpecialNo.addEventListener('change', function () {
        // When "No" is selected, ensure the elements are hidden
        toggleVisibility(false);
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
    // Function to toggle visibility of sections based on the Special Request selection
    function toggleSectionsBasedOnSpecialRequest() {
        var specialRequestYes = document.getElementById('specialYes').checked;
        var specialRequestTimePreferences = document.querySelector('.special-request-time-preferences');

        // Toggle Special Request Time Preferences section
        if (specialRequestTimePreferences) {
            specialRequestTimePreferences.style.display = specialRequestYes ? 'block' : 'none';
        }
    }

    // Event listener for changes in Special Request radio buttons
    document.querySelectorAll('input[name="SpecialRequest"]').forEach(function (radio) {
        radio.addEventListener('change', toggleSectionsBasedOnSpecialRequest);
    });

    // Optionally, trigger the change event on page load if "Yes" is already checked
    // This ensures the correct sections are shown or hidden based on the preselected value
    toggleSectionsBasedOnSpecialRequest();
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

//Show and hide contact preference section based on the selection
//document.addEventListener('DOMContentLoaded', function () {
//    // Function to check the state of special request options and toggle contact preference visibility
//    function checkSpecialRequestsAndToggleContactPreference() {
//        var specialYesChecked = document.getElementById('specialYes').checked;
//        var otherSpecialYesChecked = document.getElementById('otherSpecialYes').checked;
//        var contactPreferenceSection = document.querySelector('.contact-preference');

//        // Show contact preference if either special request is checked, otherwise hide
//        if (specialYesChecked || otherSpecialYesChecked) {
//            contactPreferenceSection.style.display = 'block';
//        } else {
//            contactPreferenceSection.style.display = 'none';
//        }
//    }

//    // Listen for changes on the 'specialYes' and 'otherSpecialYes' radio buttons
//    document.getElementById('specialYes').addEventListener('change', checkSpecialRequestsAndToggleContactPreference);
//    document.getElementById('otherSpecialYes').addEventListener('change', checkSpecialRequestsAndToggleContactPreference);

//    // Also, listen for changes on the 'no' options to ensure the contact preference section is hidden when neither 'yes' option is selected
//    document.getElementById('specialNo').addEventListener('change', checkSpecialRequestsAndToggleContactPreference);
//    document.getElementById('otherSpecialNo').addEventListener('change', checkSpecialRequestsAndToggleContactPreference);

//    // Initial check to set the correct display state when the page loads
//    checkSpecialRequestsAndToggleContactPreference();
//});

document.addEventListener('DOMContentLoaded', function () {
    // Define the function to control the display of contact preferences
    function updateContactPreferencesDisplay() {
        var specialYesChecked = document.getElementById('specialYes').checked;
        var otherSpecialYesChecked = document.getElementById('otherSpecialYes').checked;
        var contactPreferenceSection = document.querySelector('.contact-preference');

        // Hide contact preferences by default
        contactPreferenceSection.style.display = 'none';

        // Logic to determine if contact preferences should be shown
        if (specialYesChecked && !otherSpecialYesChecked) {
            contactPreferenceSection.style.display = 'block';
        } else if (otherSpecialYesChecked && !specialYesChecked) {
            contactPreferenceSection.style.display = 'block';
        }
    }

    // Get all the relevant radio buttons by ID
    var specialYes = document.getElementById('specialYes');
    var specialNo = document.getElementById('specialNo');
    var otherSpecialYes = document.getElementById('otherSpecialYes');
    var otherSpecialNo = document.getElementById('otherSpecialNo');

    // Add event listeners to the special request radio buttons
    [specialYes, specialNo, otherSpecialYes, otherSpecialNo].forEach(function (radioButton) {
        radioButton.addEventListener('change', updateContactPreferencesDisplay);
    });

    // Initial update on page load
    updateContactPreferencesDisplay();
});




