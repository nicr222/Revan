// Search function
function searchRows(inputId, rowClassName) {
    var input, searchValue, rows, i, rowData, j, cellData;
    input = document.getElementById(inputId);
    searchValue = input.value.trim().toLowerCase();
    rows = document.getElementsByClassName(rowClassName);

    for (i = 0; i < rows.length; i++) {
        rowData = rows[i].getElementsByTagName('td');
        var matchFound = false; // Flag to track if a match is found for any cell in the row
        for (j = 0; j < rowData.length; j++) { // Loop through all cells in the row
            cellData = rowData[j].innerText.toLowerCase();
            if (cellData.includes(searchValue)) {
                matchFound = true;
                break; // Break the inner loop if a match is found
            }
        }
        // Set display style based on matchFound flag
        rows[i].style.display = matchFound ? '' : 'none';
    }
}

// Call the search function for each input
document.getElementById('messageSearch').addEventListener('input', function () {
    searchRows('messageSearch', 'messageRow');
});

document.getElementById('locationSearch').addEventListener('input', function () {
    searchRows('locationSearch', 'locationRow');
});

document.getElementById('shuttleSearch').addEventListener('input', function () {
    searchRows('shuttleSearch', 'shuttleRow');
});

document.getElementById('routeSearch').addEventListener('input', function () {
    searchRows('routeSearch', 'routeRow');
});

document.getElementById('driverSearch').addEventListener('input', function () {
    searchRows('driverSearch', 'driverRow');
});

document.getElementById('checkSearch').addEventListener('input', function () {
    searchRows('checkSearch', 'checkRow');
});