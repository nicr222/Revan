document.addEventListener('DOMContentLoaded', function () {
    // Function to update stars
    const updateStars = (value) => {
        for (let i = 1; i <= 5; i++) {
            const star = document.getElementById('star-' + i);
            star.checked = i <= value;
        }
    };

    // Add change event listeners to the star inputs
    const stars = document.querySelectorAll('.star-rating input');
    stars.forEach(star => {
        star.addEventListener('change', function () {
            updateStars(this.value);
        });
    });
});
