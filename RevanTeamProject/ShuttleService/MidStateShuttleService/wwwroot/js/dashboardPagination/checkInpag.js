document.addEventListener("DOMContentLoaded", function () {
    const checkInsTableBody = document.getElementById('checkInsTableBody');
    const checkInsRows = checkInsTableBody.querySelectorAll('.checkRow');
    const paginationControls = document.getElementById('pagination-controls-checkIns');
    const checkInsPerPage = 1; // Adjust this value as needed
    const maxButtonsToShow = 4;

    // Calculate the number of pages needed
    let pageCount = Math.ceil(checkInsRows.length / checkInsPerPage);

    // Limit the number of buttons to maxButtonsToShow (4 in this case)
    pageCount = pageCount > maxButtonsToShow ? maxButtonsToShow : pageCount;

    // Clear existing pagination buttons
    paginationControls.innerHTML = '';

    // Create pagination buttons
    for (let i = 1; i <= pageCount; i++) {
        let button = document.createElement('button');
        button.className = 'page-btn';
        button.textContent = i;
        paginationControls.appendChild(button);
    }

    // Function to show the correct check-ins for the page
    function showPage(page) {
        checkInsRows.forEach((row, index) => {
            row.style.display = 'none'; // Hide all check-ins initially
            if (index >= (page - 1) * checkInsPerPage && index < page * checkInsPerPage) {
                row.style.display = 'table-row'; // Show check-ins for this page
            }
        });
    }

    // Add click event to each button
    const buttons = paginationControls.querySelectorAll('.page-btn');
    buttons.forEach(button => {
        button.addEventListener('click', function () {
            showPage(this.textContent); // Show page based on button text
        });
    });

    // Initialize the view with the first page
    if (buttons.length > 0) {
        showPage(1);
    }
});
