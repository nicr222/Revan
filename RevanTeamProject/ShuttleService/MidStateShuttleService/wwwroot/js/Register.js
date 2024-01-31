
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
});
