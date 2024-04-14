document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('feedbackForm');
    const comment = document.getElementById('testimonial');
    const rating = document.getElementsByName('rating');
    const satisfaction = document.getElementsByName('satisfaction');

    const commentValidationMessage = document.getElementById('comment-validation-message');
    const ratingValidationMessage = document.getElementById('Star-validation-message');
    const satisfactionValidationMessage = document.getElementById('satisfaction-validation-message');

    function validateComment() {
        if (comment.value === '' || comment.value.length > 255) {
            commentValidationMessage.style.display = 'block';
            return false;
        } else {
            commentValidationMessage.style.display = 'none';
            return true;
        }
    }

    function validateRating() {
        for (let i = 0; i < rating.length; i++) {
            if (rating[i].checked) {
                ratingValidationMessage.style.display = 'none';
                return true;
            }
        }
        ratingValidationMessage.style.display = 'block';
        return false;
    }

    function validateSatisfaction() {
        for (let i = 0; i < satisfaction.length; i++) {
            if (satisfaction[i].checked) {
                satisfactionValidationMessage.style.display = 'none';
                return true;
            }
        }
        satisfactionValidationMessage.style.display = 'block';
        return false;
    }


    form.addEventListener('submit', function (e) {
        const isCommentValid = validateComment();
        const isRatingValid = validateRating();
        const isSatisfactionValid = validateSatisfaction();

        if (!isCommentValid || !isRatingValid || !isSatisfactionValid ) {
            e.preventDefault(); // Prevent form submission
            alert('Please complete all the required fields correctly.');
        }
    });

});
