// Function to show modal with filtered riders
function showFilteredModal(filteredRiders) {
    var modalBody = document.getElementById('modalPassengerList');
    modalBody.innerHTML = ''; // Clear previous data

    filteredRiders.forEach(function (rider) {
        var row = document.createElement('div');
        row.className = 'row table-left-border table-right-border';
        row.innerHTML = `
            <div class="col table-top-border center-col">${rider.name}</div>
            <div class="col table-left-border table-top-border center-col">${rider.email}</div>
            <div class="col table-top-border center-col">${rider.phoneNumber}</div>
        `;
        modalBody.appendChild(row);
    });

    // Show the modal
    $('#routeSchedual').modal('show');
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

// Event listener for Print button in the modal
// Update the event listener to target the correct button by its id
document.getElementById("printModalButton").onclick = function () {
    printElement(document.getElementById("printThis"));
};

function printElement(elem) {
    // Clone the modal content along with its styles
    var domClone = elem.cloneNode(true);

    // Create a new div to hold the cloned content
    var $printSection = document.createElement("div");
    $printSection.id = "printSection";

    // Append the cloned content to the new div
    $printSection.appendChild(domClone);

    // Append the new div to the body
    document.body.appendChild($printSection);

    // Print the contents
    window.print();

    // Remove the temporary div after printing
    setTimeout(function () {
        document.body.removeChild($printSection);
    }, 500); // Adjust the timeout as needed to ensure the content is removed after printing
}