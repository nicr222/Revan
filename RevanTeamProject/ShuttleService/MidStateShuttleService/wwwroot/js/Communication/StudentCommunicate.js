// This will probably be able to be combined with the regular communicate form validation.

document.addEventListener('DOMContentLoaded', function () {
    const message = document.getElementById('message');
    const name = document.getElementById('name');
    const response = document.getElementById('responseRequest');
    const contact = document.getElementById('contactInfo')

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
        isFormValid &= validateName(name, 50, /^[A-Za-z\s]{2,}$/);
        isFormValid &= validateMessage(message, 160);
        if (response.checked) {
            isFormValid &= validateEmail(contact, 50, /^[^\s@]+@[^\s@]+\.[^\s@]+$/);
        }
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

    function validateName(element, length, regex) {
        const regexTest = regex.test(element.value)
        if (!element.value || element.value.length > length) {
            displayValidationMessage(element, "Please fill out field before sending");
            return false;
        } else {
            if (regexTest == false) {
                displayValidationMessage(element, "Name must contain only characters");
                return false;
            }
            else {
                clearValidationMessage(element);
                return true;
            }
        }
    }

    function validateEmail(element, length, regex) {
        const regexTest = regex.test(element.value)
        if (!element.value || element.value.length > length || regexTest == false) {
            displayValidationMessage(element, "Please enter a valid Email");
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