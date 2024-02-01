
//date and time picker
document.addEventListener("DOMContentLoaded", function () {
    var dateInput = document.getElementById('dateInput');
    var timeInput = document.getElementById('timeInput');

    dateInput.addEventListener('focus', function () {
        this.type = 'date';
    });
    dateInput.addEventListener('blur', function () {
        this.type = 'text';
    });

    timeInput.addEventListener('focus', function () {
        this.type = 'time';
    });
    timeInput.addEventListener('blur', function () {
        this.type = 'text';
    });

    window.openDatePicker = function () {
        dateInput.focus();
    };

    window.openTimePicker = function () {
        timeInput.focus();
    };
});

//document.addEventListener("DOMContentLoaded", function () {
//    var dateInput = document.getElementById('dateInput');
//    var timeInput = document.getElementById('timeInput');

//    // Function to open the date picker
//    window.openDatePicker = function () {
//        dateInput.type = 'date';
//        dateInput.focus();
//    };

//    dateInput.addEventListener('change', function () {
//        if (this.value) {
//            this.type = 'text';
//        }
//    });

//    dateInput.addEventListener('blur', function () {
//        if (!this.value) {
//            this.type = 'text';
//        }
//    });

//    timeInput.addEventListener('focus', function () {
//        this.type = 'time';
//    });

//    timeInput.addEventListener('blur', function () {
//        this.type = 'text';
//    });
//});


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

