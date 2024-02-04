
//date and time picker
//document.addEventListener("DOMContentLoaded", function () {
//    var dateInput = document.getElementById('dateInput');
//    var timeInput = document.getElementById('timeInput');

//    dateInput.addEventListener('focus', function () {
//        this.type = 'date';
//    });
//    dateInput.addEventListener('blur', function () {
//        this.type = 'text';
//    });

//    timeInput.addEventListener('focus', function () {
//        this.type = 'time';
//    });
//    timeInput.addEventListener('blur', function () {
//        this.type = 'text';
//    });

//    window.openDatePicker = function () {
//        dateInput.focus();
//    };

//    window.openTimePicker = function () {
//        timeInput.focus();
//    };
//});

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

