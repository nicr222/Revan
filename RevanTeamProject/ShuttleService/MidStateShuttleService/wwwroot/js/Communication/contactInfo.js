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