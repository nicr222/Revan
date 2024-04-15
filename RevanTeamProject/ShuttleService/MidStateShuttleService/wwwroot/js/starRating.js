document.addEventListener('DOMContentLoaded', function () {
    const stars = document.querySelectorAll('.star-rating input');
    stars.forEach(star => {
        star.addEventListener('change', function () {
            console.log(`Star with value ${this.value} selected.`); // Correctly logs the value
            updateStars(this.value);
        });
    });

    function updateStars(value) {
        stars.forEach(star => {
            let label = document.querySelector(`label[for="${star.id}"]`);
            if (star.value <= value) {
                label.style.color = '#f5a623'; // Highlight color
            } else {
                label.style.color = '#ccc'; // Default color
            }
        });
    }
});
