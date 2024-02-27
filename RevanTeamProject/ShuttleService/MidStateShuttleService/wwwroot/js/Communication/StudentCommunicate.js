// This will probably be able to be combined with the regular communicate form validation.

document.addEventListener('DOMContentLoaded', function () {
    const message = document.getElementById('message');
    const name = document.getElementById('name');

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
        isFormValid &= validateMessage(name, 50);
        isFormValid &= validateMessage(message, 160);
        return isFormValid;
    }

    // checks if the message box is empty
    function validateMessage(element, length) {
        if (!element.value || element.value.length > length) {
            displayValidationMessage(element, "Please fill out field before sending");
            return false;
        } else {
            clearValidationMessage(element);
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