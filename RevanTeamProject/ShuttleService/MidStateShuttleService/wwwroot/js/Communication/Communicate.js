// References Phoo's code once again.

document.addEventListener('DOMContentLoaded', function () {
    const message = document.getElementById('message');
    const pickUp = document.getElementById('PickUpLocation');
    const dropOff = document.getElementById('DropOffLocation');

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
        isFormValid &= validatePickUp();
        isFormValid &= validateDropOff();
        return isFormValid;
    }

    // checks if the message box is empty
    function validateMessage() {
        if (!message.value || message.value.length > 160) {
            displayValidationMessage(message, "Please enter a message before sending");
            return false;
        } else {
            clearValidationMessage(message);
            return true;
        }
    }

    function validatePickUp() {
        if (!pickUp.value) {
            displayValidationMessage(pickUp, "Please select a location");
            return false;
        } else {
            clearValidationMessage(pickUp);
            return true;
        }
    }

    function validateDropOff() {
        if (!dropOff.value) {
            displayValidationMessage(dropOff, "Please select a location");
            return false;
        } else {
            clearValidationMessage(dropOff);
            return true;
        }
    }

    function validateRoutes() {
        var formValid = false;

        var i = 0;
        while (!formValid && i < route.length) {
            if (route[i].checked) formValid = true;
            i++;
        }

        if (!formValid) {
            displayValidationMessage(route, "Please select a route")
        };
        return formValid;
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