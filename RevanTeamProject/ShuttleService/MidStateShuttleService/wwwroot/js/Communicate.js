// References Phoo's code once again.

document.addEventListener('DOMContentLoaded', function () {
    const message = document.getElementById('message'); 
    const shuttles = document.getElementById('shuttleSelect');


    // Real-time validation event listeners
    message.addEventListener('input', validateStudentIdPhoneNumber);
    shuttles.addEventListener('input', validateStudentIdPhoneNumber);

    // Form submission event listener
    document.querySelector('.wrapper').addEventListener('submit', function (event) {
        let isFormValid = validateForm();
        if (!isFormValid) {
            event.preventDefault();
        }
    });

    // Validation functions
    function validateForm() {
        let isFormValid = true;
        isFormValid &= validateMessage();
        isFormValid &= validateShuttles();
        return isFormValid;
    }

    // checks if the message box is empty
    function validateMessage() {
        if (!message.value) {
            displayValidationMessage(message, "Please enter a message before sending");
            return false;
        } else {
            clearValidationMessage(message);
            return true;
        }
    }

    // Checks if the shuttles have been selected or not/
    function validateShuttles() {
        if (!shuttles.value) {
            displayValidationMessage(message, "Please select a group of students to send the message to");
            return false;
        } else {
            clearValidationMessage(shuttles);
            return true;
        }
    }

    // Functions are the same as the ones from Phoo's validation just without the Radio options
    function displayValidationMessage(element, message) {
        validationMessageElement.innerText = message;
        validationMessageElement.style.display = 'block'; // Show validation message
        element.classList.add('is-invalid');
    }

    function clearValidationMessage(element, isRadio) {
        validationMessageElement.innerText = '';
        validationMessageElement.style.display = 'none'; // Hide validation message
        element.classList.remove('is-invalid');
    }
});