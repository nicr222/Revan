// ** Commented out the some validation fields to work witht the rest of the form in next sprint **//

document.addEventListener('DOMContentLoaded', function () {
    const firstName = document.getElementById('FirstName');
    const lastName = document.getElementById('LastName');
    const email = document.getElementById('Email');
    const phoneNumber = document.getElementById('PhoneNumber');
    //const tripTypeRadios = document.querySelectorAll('input[name="TripType"]');
    //const pickUpLocation = document.getElementById('PickLocationID'); // Corrected ID
    //const dropOffLocation = document.getElementById('DropOffLocationID'); // Corrected ID
    //const date = document.getElementById('Date');
    //const pickUpTime = document.getElementById('PickUpTime');
    //const dropOffTime = document.getElementById('DropOffTime');

    // Attach event listeners for real-time validation
    phoneNumber.addEventListener('input', () => validateField(phoneNumber, /^[0-9]{10}$/, 'PhoneNumber'));
    firstName.addEventListener('input', () => validateField(firstName, /^[A-Za-z\s]{2,}$/, 'FirstName'));
    lastName.addEventListener('input', () => validateField(lastName, /^[A-Za-z\s]{2,}$/, 'LastName'));
    email.addEventListener('input', () => validateField(email, /^[^\s@]+@[^\s@]+\.[^\s@]+$/, 'Email')); // Email validation regex
    //pickUpLocation.addEventListener('change', () => validateSelection(pickUpLocation, 'PickLocationID'));
    //dropOffLocation.addEventListener('change', () => validateSelection(dropOffLocation, 'DropOffLocationID'));
    //date.addEventListener('change', () => validateSelection(date, 'Date'));
    //pickUpTime.addEventListener('change', () => validateSelection(pickUpTime, 'PickUpTime'));
    //dropOffTime.addEventListener('change', () => validateSelection(dropOffTime, 'DropOffTime'));
    //tripTypeRadios.forEach(radio => radio.addEventListener('change', validateTripType));

    document.querySelector('#registrationForm').addEventListener('submit', function (event) {
        event.preventDefault(); // Always prevent default submission initially

        if (validateForm()) {
            console.log("Form is valid, showing confirmation modal.");
            $('#confirmationModal').modal('show'); // Correctly show confirmation modal only if valid
        } else {
            console.log("Form is not valid, submission prevented.");
            scrollToFirstInvalid();
        }
    });

    // Make sure it returns false if any validation fails
    function validateForm() {
        const isPhoneNumberValid = validateField(phoneNumber, /^[0-9]{10}$/, 'PhoneNumber');
        const isFirstNameValid = validateField(firstName, /^[A-Za-z\s]{2,}$/, 'FirstName');
        const isLastNameValid = validateField(lastName, /^[A-Za-z\s]{2,}$/, 'LastName');
        const isEmailValid = validateField(email, /^[^\s@]+@[^\s@]+\.[^\s@]+$/, 'Email');
        //const isPickUpLocationValid = validateSelection(pickUpLocation, 'PickLocationID');
        //const isDropOffLocationValid = validateSelection(dropOffLocation, 'DropOffLocationID');
        //const isDateValid = validateSelection(date, 'Date');
        //const isPickUpTimeValid = validateSelection(pickUpTime, 'PickUpTime');
        //const isDropOffTimeValid = validateSelection(dropOffTime, 'DropOffTime');
        //const isTripTypeValid = validateTripType(); // Correctly placed outside the array

        // Combine all validations for overall form validity
        //return isPhoneNumberValid && isFirstNameValid && isLastNameValid && isEmailValid &&
        //    isPickUpLocationValid && isDropOffLocationValid && isDateValid &&
        //    isPickUpTimeValid && isDropOffTimeValid && isTripTypeValid;
    }

    function validateField(element, regex, fieldName) {
        const isValid = regex.test(element.value);
        const validationMessageElement = document.getElementById(`${fieldName}-validation-message`);
        if (isValid) {
            validationMessageElement.style.display = 'none';
            element.classList.remove('is-invalid');
        } else {
            validationMessageElement.style.display = 'block';
            element.classList.add('is-invalid');
        }
        return isValid;
    }

    //function validateSelection(element, fieldName) {
    //    const isValid = element.value !== '';
    //    const validationMessageElement = document.getElementById(`${fieldName}-validation-message`);
    //    if (isValid) {
    //        validationMessageElement.style.display = 'none';
    //        element.classList.remove('is-invalid');
    //    } else {
    //        validationMessageElement.style.display = 'block';
    //        element.classList.add('is-invalid');
    //    }
    //    return isValid;
    //}
    //function validateTripType() {
    //    const isValid = [...tripTypeRadios].some(radio => radio.checked);
    //    const validationMessageElement = document.getElementById('TripType-validation-message');
    //    if (!isValid) {
    //        validationMessageElement.style.display = 'block';
    //        tripTypeRadios.forEach(radio => radio.classList.add('is-invalid'));
    //    } else {
    //        validationMessageElement.style.display = 'none';
    //        tripTypeRadios.forEach(radio => radio.classList.remove('is-invalid'));
    //    }
    //    return isValid;
    //}

    //to improve user experience by bringing the first invalid input into view if the form is not valid.
    function scrollToFirstInvalid() {
        const firstInvalidElement = document.querySelector('.is-invalid');
        if (firstInvalidElement) {
            firstInvalidElement.scrollIntoView({
                behavior: 'smooth',
                block: 'center'
            });
        }
    }
});