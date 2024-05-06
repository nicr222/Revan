document.addEventListener("DOMContentLoaded", function () {
    const testimonialsContainer = document.querySelector('.testimonials-container');
    const testimonials = testimonialsContainer.querySelectorAll('.testimonial');
    const paginationControls = document.getElementById('pagination-controls');
    const testimonialsPerPage = 3;
    const maxButtonsToShow = 4;

    // Calculate the number of pages needed
    let pageCount = Math.ceil(testimonials.length / testimonialsPerPage);

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

    // Function to show the correct testimonials for the page
    function showPage(page) {
        testimonials.forEach((test, index) => {
            test.style.display = 'none'; // Hide all testimonials initially
            if (index >= (page - 1) * testimonialsPerPage && index < page * testimonialsPerPage) {
                test.style.display = 'flex'; // Show testimonials for this page
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


