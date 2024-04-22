document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('feedbackForm');
    const comment = document.getElementById('testimonial');
    const satisfaction = document.getElementsByName('satisfaction');
    const displayTestimonialYes = document.getElementById('recommendYes');
    const displayTestimonialNo = document.getElementById('recommendNo');

    const commentValidationMessage = document.getElementById('comment-validation-message');
    const satisfactionValidationMessage = document.getElementById('satisfaction-validation-message');
    const displayTestimonialValidationMessage = document.getElementById('displayTestimonial-validation-message');

    function validateComment() {
        if (comment.value === '' || comment.value.length > 255) {
            commentValidationMessage.style.display = 'block';
            return false;
        } else {
            commentValidationMessage.style.display = 'none';
            return true;
        }
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

    function validateDisplayTestimonial() {
        if (!displayTestimonialYes.checked && !displayTestimonialNo.checked) {
            displayTestimonialValidationMessage.style.display = 'block';
            return false;
        } else {
            displayTestimonialValidationMessage.style.display = 'none';
            return true;
        }
    }


    form.addEventListener('submit', function (e) {
        const isCommentValid = validateComment();
        const isSatisfactionValid = validateSatisfaction();
        const isDisplayTestimonialValid = validateDisplayTestimonial();

        if (!isCommentValid || !isRatingValid || !isSatisfactionValid || !isDisplayTestimonialValid ) {
            e.preventDefault(); // Prevent form submission
            alert('Please complete all the required fields correctly.');
        }
    });

});
