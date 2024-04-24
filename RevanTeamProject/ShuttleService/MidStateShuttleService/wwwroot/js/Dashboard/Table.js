$(function () {
    // Function to hide all tables
    function hideAllTables() {
        $('.recentItem').hide();
    }

    // Initial setup to show all tables
    hideAllTables();

    // Click event for the Routes button
    $('#order').click(function () {
        hideAllTables();
        return false;
    });

    // Click event for the Employees button
    $('#routes').click(function () {
        hideAllTables();
        $('.recentItem.route').show(); // Show the location table
        return false;
    });

    $('#location').click(function () {
        hideAllTables();
        $('.recentItem.location').show(); // Show the location table
        return false;
    });
    // Click event for the Drivers button
    $('#driver').click(function () {
        hideAllTables();
        $('.recentItem.driver').show(); // Show the driver table
        return false;
    });

    // Click event for the Shuttles button
    $('#shuttle').click(function () {
        hideAllTables();
        $('.recentItem.shuttle').show(); // Show the shuttle table
        return false;
    });

    // Click event for the Check Ins button
    $('#check').click(function () {
        hideAllTables();
        $('.recentItem.check').show(); // Show the check ins table
        return false;
    });

    // Click event for the Feedback button
    $('#feedback').click(function () {
        hideAllTables();
        $('.recentItem.feedback').show(); // Show the feedback table
        return false;
    });

});
