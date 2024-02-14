document.addEventListener('DOMContentLoaded', function () {
    const userId = document.getElementById('UserId'); //const, it means that the variable cannot be reassigned to a different value later in the code
    const firstName = document.getElementById('FirstName');
    const lastName = document.getElementById('LastName');
    const phoneNumber = document.getElementById('PhoneNumber');
    const tripTypeRadios = document.querySelectorAll('input[name="TripType"]');
    const pickUpLocation = document.getElementById('PickUpLocation');
    const dropOffLocation = document.getElementById('DropOffLocation');
    const date = document.getElementById('Date');
    const time = document.getElementById('Time');

    // Real-time validation event listeners
    userId.addEventListener('input', validateStudentIdPhoneNumber);
    phoneNumber.addEventListener('input', validateStudentIdPhoneNumber);
    firstName.addEventListener('input', validateFirstLastName);
    lastName.addEventListener('input', validateFirstLastName);
    tripTypeRadios.forEach(radio => radio.addEventListener('change', validateTripType));
    pickUpLocation.addEventListener('change', validatePickUpLocation);
    dropOffLocation.addEventListener('change', validateDropOffLocation);
    date.addEventListener('change', validateDate);
    time.addEventListener('change', validateTime);

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
        isFormValid &= validateUserIdPhoneNumber();
        isFormValid &= validateFirstLastName();
        isFormValid &= validateTripType();
        isFormValid &= validatePickUpLocation();
        isFormValid &= validateDropOffLocation();
        isFormValid &= validateDate();
        isFormValid &= validateTime();
        return isFormValid;
    }

    function validateUserIdPhoneNumber() {
        let isValid = true;
        [studentId, phoneNumber].forEach(input => {
            const regex = /^[0-9]{10}$/;
            if (!regex.test(input.value)) {
                displayValidationMessage(input, "Must be 10 digits");
                isValid = false;
            } else {
                clearValidationMessage(input);
            }
        });
        return isValid;
    }

    function validateFirstLastName() {
        let isValid = true;
        [firstName, lastName].forEach(input => {
            const regex = /^[A-Za-z\s]{2,}$/;
            if (!regex.test(input.value)) {
                displayValidationMessage(input, "Must contain only characters and be at least 2 characters long");
                isValid = false;
            } else {
                clearValidationMessage(input);
            }
        });
        return isValid;
    }

    function validateTripType() {
        if (![...tripTypeRadios].some(radio => radio.checked)) {
            displayValidationMessage(tripTypeRadios[0], "You must select a trip type", true);
            return false;
        } else {
            clearValidationMessage(tripTypeRadios[0], true);
            return true;
        }
    }

    function validatePickUpLocation() {
        if (!pickUpLocation.value) {
            displayValidationMessage(pickUpLocation, "Please select a pick-up location");
            return false;
        } else {
            clearValidationMessage(pickUpLocation);
            return true;
        }
    }

    function validateDropOffLocation() {
        if (!dropOffLocation.value) {
            displayValidationMessage(dropOffLocation, "Please select a drop-off location");
            return false;
        } else {
            clearValidationMessage(dropOffLocation);
            return true;
        }
    }

    function validateDate() {
        if (!date.value) {
            displayValidationMessage(date, "Please select a date");
            return false;
        } else {
            clearValidationMessage(date);
            return true;
        }
    }

    function validateTime() {
        if (!time.value) {
            displayValidationMessage(time, "Please select a time");
            return false;
        } else {
            clearValidationMessage(time);
            return true;
        }
    }

    function displayValidationMessage(element, message, isRadio = false) {
        let validationMessageElement = isRadio ? element.closest('.radio-options').nextElementSibling : element.nextElementSibling;
        validationMessageElement.innerText = message;
        validationMessageElement.style.display = 'block'; // Show validation message
        element.classList.add('is-invalid');
    }

    function clearValidationMessage(element, isRadio = false) {
        let validationMessageElement = isRadio ? element.closest('.radio-options').nextElementSibling : element.nextElementSibling;
        validationMessageElement.innerText = '';
        validationMessageElement.style.display = 'none'; // Hide validation message
        element.classList.remove('is-invalid');
    }
});
