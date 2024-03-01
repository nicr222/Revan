// Resets the form to its default values
function resetForm() {
    // Assuming the form has an ID of 'registrationForm'
    document.getElementById('registrationForm').reset();

    // If you have additional UI elements to reset or hide, add that logic here
    // Example: Hide a dynamically shown section
     $('.other-special-request').hide();
}

// Clears all input fields, ignoring their initial values
function clearFormFields() {
    // Clears text, password, and email fields
    $('#registrationForm input[type="text"], #registrationForm input[type="password"], #registrationForm input[type="email"], #registrationForm textarea').val('');

    // Resets select elements to their first option
    $('#registrationForm select').prop('selectedIndex', 0);

    // Unchecks all checkboxes and radio buttons
    $('#registrationForm input[type="checkbox"], #registrationForm input[type="radio"]').prop('checked', false);

    // Optionally, hide dynamically shown sections
     $('.other-special-request').hide();
}

// Optional: If you need to handle the form reset/clear action on a button click without inline JavaScript
$(document).ready(function () {
    // Bind the clearFormFields function to the 'Cancel' button click event
    $('.btn--cancel').on('click', function () {
        clearFormFields(); // or resetForm() depending on your requirement
    });
});
