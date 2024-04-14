document.addEventListener("DOMContentLoaded", function () {
    const testimonials = document.querySelectorAll('.testimonial');
    const buttons = document.querySelectorAll('.page-btn');
    const testimonialsPerPage = 3;

    function showPage(page) {
        testimonials.forEach((test, index) => {
            test.style.display = 'none'; // Hide all testimonials initially
            if (index >= (page - 1) * testimonialsPerPage && index < page * testimonialsPerPage) {
                test.style.display = 'flex'; // Show testimonials for this page
            }
        });
    }

    buttons.forEach(button => {
        button.addEventListener('click', function () {
            showPage(this.textContent); // Show page based on button text
        });
    });

    showPage(1); // Initialize the view with the first page
});

