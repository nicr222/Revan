
function toggleRiders() {
    const button = document.getElementById('toggleButton');
    const tableRows = document.querySelectorAll('#passengerTable tbody tr');

    if (button.textContent.trim() === 'Show Today\'s Riders') {
        button.textContent = 'Show All Riders';
        tableRows.forEach(row => {
            const firstDayExpected = new Date(row.children[3].textContent);
            if (firstDayExpected.toDateString() !== new Date().toDateString()) {
                row.style.display = 'none';
            }
        });
    } else {
        button.textContent = 'Show Today\'s Riders';
        tableRows.forEach(row => {
            row.style.display = '';
        });
    }
}

// Add event listener when the DOM content is loaded
document.addEventListener('DOMContentLoaded', function () {
    const button = document.getElementById('toggleButton');
    if (button) {
        button.addEventListener('click', toggleRiders);
    }
});
