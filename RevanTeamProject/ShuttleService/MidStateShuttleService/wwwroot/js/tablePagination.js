// JavaScript file for routes pagination
document.addEventListener("DOMContentLoaded", function () {
    const routesTableBody = document.getElementById('routesTableBody');
    const routesRows = routesTableBody.querySelectorAll('.routeRow');
    const paginationControls = document.getElementById('pagination-controls-routes'); // Unique ID
    const routesPerPage = 15; // Adjust this value as needed
    const maxButtonsToShow = 10;

    // Calculate the number of pages needed
    let pageCount = Math.ceil(routesRows.length / routesPerPage);

    // Limit the number of buttons to maxButtonsToShow (4 in this case)
    pageCount = pageCount > maxButtonsToShow ? maxButtonsToShow : pageCount;

    // Clear existing pagination buttons
    paginationControls.innerHTML = '';

    // Create pagination buttons
    for (let i = 1; i <= pageCount; i++) {
        let button = document.createElement('button');
        button.className = 'page-btn'; // Apply common style class
        button.textContent = i;
        paginationControls.appendChild(button);
    }

    // Function to show the correct routes for the page
    function showPage(page) {
        routesRows.forEach((row, index) => {
            row.style.display = 'none'; // Hide all routes initially
            if (index >= (page - 1) * routesPerPage && index < page * routesPerPage) {
                row.style.display = 'table-row'; // Show routes for this page
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

// JavaScript file for check-ins pagination
document.addEventListener("DOMContentLoaded", function () {
    const checkInsTableBody = document.getElementById('checkInsTableBody');
    const checkInsRows = checkInsTableBody.querySelectorAll('.checkRow');
    const paginationControls = document.getElementById('pagination-controls-checkIns'); // Unique ID
    const checkInsPerPage = 15; // Adjust this value as needed
    const maxButtonsToShow = 10;

    // Calculate the number of pages needed
    let pageCount = Math.ceil(checkInsRows.length / checkInsPerPage);

    // Limit the number of buttons to maxButtonsToShow (4 in this case)
    pageCount = pageCount > maxButtonsToShow ? maxButtonsToShow : pageCount;

    // Clear existing pagination buttons
    paginationControls.innerHTML = '';

    // Create pagination buttons
    for (let i = 1; i <= pageCount; i++) {
        let button = document.createElement('button');
        button.className = 'page-btn'; // Apply common style class
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

// JavaScript file for driver pagination
document.addEventListener("DOMContentLoaded", function () {
    const driverTableBody = document.getElementById('driverTableBody');
    const driverRows = driverTableBody.querySelectorAll('.driverRow');
    const paginationControls = document.getElementById('pagination-controls-driver'); // Unique ID
    const driversPerPage = 15; // Adjust this value as needed
    const maxButtonsToShow = 10;

    // Calculate the number of pages needed
    let pageCount = Math.ceil(driverRows.length / driversPerPage);

    // Limit the number of buttons to maxButtonsToShow (4 in this case)
    pageCount = pageCount > maxButtonsToShow ? maxButtonsToShow : pageCount;

    // Clear existing pagination buttons
    paginationControls.innerHTML = '';

    // Create pagination buttons
    for (let i = 1; i <= pageCount; i++) {
        let button = document.createElement('button');
        button.className = 'page-btn'; // Apply common style class
        button.textContent = i;
        paginationControls.appendChild(button);
    }

    // Function to show the correct drivers for the page
    function showPage(page) {
        driverRows.forEach((row, index) => {
            row.style.display = 'none'; // Hide all drivers initially
            if (index >= (page - 1) * driversPerPage && index < page * driversPerPage) {
                row.style.display = 'table-row'; // Show drivers for this page
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

// JavaScript file for feedback pagination
document.addEventListener("DOMContentLoaded", function () {
    const feedbackTableBody = document.getElementById('feedbackTableBody');
    const feedbackRows = feedbackTableBody.querySelectorAll('#feedbackRow'); // Use class instead of id
    const paginationControls = document.getElementById('pagination-controls-feedback'); // Unique ID
    const feedbackPerPage = 15; // Adjust this value as needed
    const maxButtonsToShow = 10;

    // Calculate the number of pages needed
    let pageCount = Math.ceil(feedbackRows.length / feedbackPerPage);

    // Limit the number of buttons to maxButtonsToShow (4 in this case)
    pageCount = pageCount > maxButtonsToShow ? maxButtonsToShow : pageCount;

    // Clear existing pagination buttons
    paginationControls.innerHTML = '';

    // Create pagination buttons
    for (let i = 1; i <= pageCount; i++) {
        let button = document.createElement('button');
        button.className = 'page-btn'; // Apply common style class
        button.textContent = i;
        paginationControls.appendChild(button);
    }

    // Function to show the correct feedback for the page
    function showPage(page) {
        feedbackRows.forEach((row, index) => {
            row.style.display = 'none'; // Hide all feedback initially
            if (index >= (page - 1) * feedbackPerPage && index < page * feedbackPerPage) {
                row.style.display = 'table-row'; // Show feedback for this page
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


    document.addEventListener("DOMContentLoaded", function () {
        const locationsTableBody = document.getElementById('locationsTableBody');
        const locationRows = locationsTableBody.querySelectorAll('.locationRow');
        const paginationControls = document.getElementById('pagination-controls-location');
        const locationsPerPage = 15; // Adjust this value as needed
        const maxButtonsToShow = 10;

        // Calculate the number of pages needed
        let pageCount = Math.ceil(locationRows.length / locationsPerPage);

        // Limit the number of buttons to maxButtonsToShow
        pageCount = pageCount > maxButtonsToShow ? maxButtonsToShow : pageCount;

        // Clear existing pagination buttons
        paginationControls.innerHTML = '';

        // Create pagination buttons
        for (let i = 1; i <= pageCount; i++) {
            let button = document.createElement('button');
            button.className = 'page-btn'; // Apply common style class
            button.textContent = i;
            paginationControls.appendChild(button);
        }

        // Function to show the correct locations for the page
        function showPage(page) {
            locationRows.forEach((row, index) => {
                row.style.display = 'none'; // Hide all locations initially
                if (index >= (page - 1) * locationsPerPage && index < page * locationsPerPage) {
                    row.style.display = 'table-row'; // Show locations for this page
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

        document.addEventListener("DOMContentLoaded", function () {
            const messageTableBody = document.getElementById('messageTableBody');
            const messageRows = messageTableBody.querySelectorAll('#messageRow');
            const paginationControls = document.getElementById('pagination-controls-message');
            const messagesPerPage = 15; // Adjust this value as needed
            const maxButtonsToShow = 10;

            // Calculate the number of pages needed
            let pageCount = Math.ceil(messageRows.length / messagesPerPage);

            // Limit the number of buttons to maxButtonsToShow
            pageCount = pageCount > maxButtonsToShow ? maxButtonsToShow : pageCount;

            // Clear existing pagination buttons
            paginationControls.innerHTML = '';

            // Create pagination buttons
            for (let i = 1; i <= pageCount; i++) {
                let button = document.createElement('button');
                button.className = 'page-btn'; // Apply common style class
                button.textContent = i;
                paginationControls.appendChild(button);
            }

            // Function to show the correct messages for the page
            function showPage(page) {
                messageRows.forEach((row, index) => {
                    row.style.display = 'none'; // Hide all messages initially
                    if (index >= (page - 1) * messagesPerPage && index < page * messagesPerPage) {
                        row.style.display = 'table-row'; // Show messages for this page
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

        document.addEventListener("DOMContentLoaded", function () {
            const shuttleTableBody = document.getElementById('shuttleTableBody');
            const shuttleRows = shuttleTableBody.querySelectorAll('.shuttleRow');
            const paginationControls = document.getElementById('pagination-controls-shuttle');
            const shuttlesPerPage = 15; // Adjust this value as needed
            const maxButtonsToShow = 10;

            // Calculate the number of pages needed
            let pageCount = Math.ceil(shuttleRows.length / shuttlesPerPage);

            // Limit the number of buttons to maxButtonsToShow
            pageCount = pageCount > maxButtonsToShow ? maxButtonsToShow : pageCount;

            // Clear existing pagination buttons
            paginationControls.innerHTML = '';

            // Create pagination buttons
            for (let i = 1; i <= pageCount; i++) {
                let button = document.createElement('button');
                button.className = 'page-btn'; // Apply common style class
                button.textContent = i;
                paginationControls.appendChild(button);
            }

            // Function to show the correct shuttles for the page
            function showPage(page) {
                shuttleRows.forEach((row, index) => {
                    row.style.display = 'none'; // Hide all shuttles initially
                    if (index >= (page - 1) * shuttlesPerPage && index < page * shuttlesPerPage) {
                        row.style.display = 'table-row'; // Show shuttles for this page
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

// JavaScript for Special Requests pagination
document.addEventListener("DOMContentLoaded", function () {
    const registrationsTableBody = document.getElementById('registrationsTableBody');
    const registrationRows = registrationsTableBody.querySelectorAll('.registrationRow');
    const paginationControls = document.getElementById('pagination-controls-registration');
    const registrationsPerPage = 15; // Adjust this value as needed
    const maxButtonsToShow = 10;

    // Calculate the number of pages needed
    let pageCount = Math.ceil(registrationRows.length / registrationsPerPage);

    // Limit the number of buttons to maxButtonsToShow (4 in this case)
    pageCount = pageCount > maxButtonsToShow ? maxButtonsToShow : pageCount;

    // Clear existing pagination buttons
    paginationControls.innerHTML = '';

    // Create pagination buttons
    for (let i = 1; i <= pageCount; i++) {
        let button = document.createElement('button');
        button.className = 'page-btn'; // Apply common style class
        button.textContent = i;
        paginationControls.appendChild(button);
    }

    // Function to show the correct registrations for the page
    function showPage(page) {
        registrationRows.forEach((row, index) => {
            row.style.display = 'none'; // Hide all registrations initially
            if (index >= (page - 1) * registrationsPerPage && index < page * registrationsPerPage) {
                row.style.display = 'table-row'; // Show registrations for this page
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