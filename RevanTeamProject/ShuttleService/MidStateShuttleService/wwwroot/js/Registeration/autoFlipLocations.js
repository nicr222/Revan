document.addEventListener("DOMContentLoaded", function () {
    const pickUpLocation = document.getElementById('PickUpLocation');
    const dropOffLocation = document.getElementById('DropOffLocation');
    const returnPickUpLocation = document.getElementById('ReturnPickUpLocation');
    const returnDropOffLocation = document.getElementById('ReturnDropOffLocation');

    // Ensure updateRoutes is defined and accessible here.
    function autoFlipReturnLocations() {
        // Auto-flip logic
        const pickUpValue = pickUpLocation.value;
        const dropOffValue = dropOffLocation.value;

        // Check if both pickup and dropoff locations are selected
        if (pickUpValue && dropOffValue) {
            // Set the return journey locations inversely
            returnPickUpLocation.value = dropOffValue;
            returnDropOffLocation.value = pickUpValue;

            // Assuming updateRoutes is available globally or imported
            updateRoutes(); // Call this function to update the return route options based on the new locations
        }
    }

    // Event listeners to trigger auto-flip when either select element changes
    pickUpLocation.addEventListener('change', autoFlipReturnLocations);
    dropOffLocation.addEventListener('change', autoFlipReturnLocations);
});