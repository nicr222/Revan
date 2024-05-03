// Function to show modal with filtered riders
function showFilteredModal(filteredRiders) {
    var modalBody = document.getElementById('filteredData');
    modalBody.innerHTML = ''; // Clear previous data

    filteredRiders.forEach(function (rider) {
        var row = document.createElement('tr');
        row.innerHTML = `
            <td>${rider.name}</td>
            <td>${rider.phoneNumber}</td>
            <td><a href="mailto:${rider.email}">${rider.email}</a></td>
        `;
        modalBody.appendChild(row);
    });

    // Show the modal
    $('#filteredModal').modal('show');
}

// Function to filter table rows based on selected day
function filterTableRows(selectedDay) {
    var passengerRows = document.querySelectorAll('.passengerRow');

    passengerRows.forEach(function (row) {
        var days = row.getAttribute('data-days').split(',');
        if (selectedDay === "" || days.includes(selectedDay)) {
            row.style.display = 'table-row';
        } else {
            row.style.display = 'none';
        }
    });
}

// Event listener for day selector
document.getElementById('daySelector').addEventListener('change', function () {
    var selectedDay = this.value;

    // Filter table rows based on selected day
    filterTableRows(selectedDay);
});

// Event listener for Print button
document.getElementById('printButton').addEventListener('click', function () {
    var selectedDay = document.getElementById('daySelector').value;
    var passengerRows = document.querySelectorAll('.passengerRow');
    var filteredRiders = [];

    passengerRows.forEach(function (row) {
        var days = row.getAttribute('data-days').split(',');
        if (selectedDay === "" || days.includes(selectedDay)) {
            filteredRiders.push({
                name: row.cells[0].innerText,
                phoneNumber: row.cells[1].innerText,
                email: row.cells[2].innerText
            });
        }
    });

    // Filter table rows based on selected day
    filterTableRows(selectedDay);

    // Show the modal with filtered riders
    showFilteredModal(filteredRiders);
});

// Add event listener to the "Print" button
document.getElementById('printModalButton').addEventListener('click', function () {
    printModalContent();
});

// Function to print modal content
function printModalContent() {
    var modalContent = document.getElementById('filteredModal').innerHTML;
    var originalBody = document.body.innerHTML;
    document.body.innerHTML = modalContent;
    window.print();
    document.body.innerHTML = originalBody;
}