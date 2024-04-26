// dashboard-sections.js
$(window).on('load', function () {
    var openSection = globalOpenSection; // Use the global variable
    console.log("OpenSection value on client:", openSection);

    if (openSection === "feedback") {
        $('.recentItem').hide(); // Hide all sections
        $('.recentItem.feedback').show(); // Show only the feedback section
    }
    // Add additional else if branches for other sections if necessary
});

// Re-setup event handlers
setupEventHandlers();