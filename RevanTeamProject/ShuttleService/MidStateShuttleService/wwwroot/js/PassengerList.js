document.getElementById('daySelector').addEventListener('change', function () {
    var selectedDay = this.value;
    var passengerRows = document.querySelectorAll('.passengerRow');

    passengerRows.forEach(function (row) {
        var days = row.getAttribute('data-days').split(',');
        if (selectedDay === "" || days.includes(selectedDay)) {
            row.style.display = 'table-row';
        } else {
            row.style.display = 'none';
        }
    });
});