// References Phoo's code once again.

document.addEventListener('DOMContentLoaded', function () {
    const message = document.getElementById('message'); 
    const shuttles = document.querySelectorAll('.checkbox');

    // Form submission event listener
    document.querySelector('#CommunicateForm').addEventListener('submit', function (event) {
        let isFormValid = validateForm();
        if (!isFormValid) {
            event.preventDefault();
        }
    });

    // Validation functions
    function validateForm() {
        let isFormValid = true;
        isFormValid &= validateMessage();
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

    // Functions are the same as the ones from Phoo's validation just without the Radio options
    function displayValidationMessage(element, message) {
        const id = element.id;
        const elementId = id.concat("-", "Validation-Message")
        let validationMessageElement = document.getElementById(elementId);
        validationMessageElement.innerText = message;
        validationMessageElement.style.display = 'block'; // Show validation message
        element.classList.add('is-invalid');
    }

    function clearValidationMessage(element) {
        const id = element.id;
        const elementId = id.concat("-", "Validation-Message")
        let validationMessageElement = document.getElementById(elementId);
        validationMessageElement.innerText = '';
        validationMessageElement.style.display = 'none'; // Hide validation message
        element.classList.remove('is-invalid');
    }
});