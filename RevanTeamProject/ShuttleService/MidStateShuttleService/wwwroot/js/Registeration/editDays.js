window.addEventListener("load", function () {
    var days = document.getElementById('daysSelected').value;

    var mondayToggle = document.getElementById('MondayToggle');
    var tuesdayToggle = document.getElementById('TuesdayToggle');
    var wednesdayToggle = document.getElementById('WednesdayToggle');
    var thursdayToggle = document.getElementById('ThursdayToggle');

    if (days.includes("Monday")) {
        mondayToggle.checked = true;
    }

    if (days.includes("Tuesday")) {
        tuesdayToggle.checked = true;
    }

    if (days.includes("Wednesday")) {
        wednesdayToggle.checked = true;
    }

    if (days.includes("Thursday")) {
        thursdayToggle.checked = true;
    }
});