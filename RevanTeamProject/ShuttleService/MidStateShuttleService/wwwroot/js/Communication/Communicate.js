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
        isFormValid &= validateShuttles();
        return isFormValid;
    }

    // checks if the message box is empty
    function validateMessage() {
        if (!message.value || message.value.length > 160) {
            displayValidationMessage(message, "Please enter a message before sending";
            return false;
        } else {
            clearValidationMessage(message);
            return true;
        }
    }

    function validateShuttles() {
        var isChecked = false;

        for (var i = 0; i < shuttles.length; i++) {
            if (shuttles[i].checked) {
                isChecked = true;
                break;
            }
        }

        if (!isChecked) {
            displayShuttleValidation("shuttle-Validation-Message", "Please select at least 1 shuttle")
            return false;
        } else {
            clearShuttleValidation("shuttle-Validation-Message");
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

    function displayShuttleValidation(element, message) {
        let validationMessageElement = document.getElementById(element);
        validationMessageElement.innerText = message;
        validationMessageElement.style.display = 'block'; // Show validation message
    }

    function clearShuttleValidation(element) {
        let validationMessageElement = document.getElementById(element);
        validationMessageElement.innerText = '';
        validationMessageElement.style.display = 'none'; // Show validation message
    }
});