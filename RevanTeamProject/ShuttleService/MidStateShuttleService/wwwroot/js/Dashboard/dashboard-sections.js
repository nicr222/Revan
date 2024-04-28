// dashboard-sections.js
$(window).on('load', function () {
    var openSection = globalOpenSection; // Use the global variable
    console.log("OpenSection value on client:", openSection);

    if (openSection === "feedback") {
        $('.recentFeedback').hide(); // Hide all sections
        $('.recentFeedback.feedback').show(); // Show only the feedback section
    }

    if (openSection === "message") {
        $('.recentFeedback').hide(); // Hide all sections
        $('.recentFeedback.messages').show(); // Show only the feedback section
    }
    // Add additional else if branches for other sections if necessary
});

// Re-setup event handlers
setupEventHandlers();