function toggleDisplay() {
    const toggle = document.getElementById('responseRequest');
    const section = document.getElementById('contactInfoDiv');
    const toggleChecked = toggle.checked;

    if (toggleChecked) {
        section.style.display = "block";
    }
    else {
        section.style.display = "none";
    }
}

function toggleForm() {
    const toggle = document.getElementById('formToggle');
    const section = document.getElementById('formContainer');
    const section2 = document.getElementById('emergencyContact');
    const toggleChecked = toggle.checked;

    if (toggleChecked) {
        section2.style.display = "block";
        section.style.display = "none";
    }
    else {
        section2.style.display = "none";
        section.style.display = "block";
    }
}