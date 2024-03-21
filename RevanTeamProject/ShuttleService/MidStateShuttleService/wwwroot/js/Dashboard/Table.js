$(function () {
    // Function to hide all tables
    function hideAllTables() {
        $('.recentItem').hide();
    }

    // Initial setup to show all tables
    hideAllTables();

    // Click event for the Dashboard button
    $('#order').click(function () {
        hideAllTables();
        return false;
    });

    // Click event for the Routes button
    $('#employee').click(function () {
        hideAllTables();
        $('.recentItem.route').show();
        return false;
    });

    // Click event for the Check Ins button
    $('#product').click(function () {
        hideAllTables();
        $('.recentItem.recentFeedback').show();
        return false;
    });

    // Click event for the Locations button
    $('#department').click(function () {
        hideAllTables();
        $('.recentItem.location').show();
        return false;
    });

    // Click event for the Shuttles button
    $('#shuttle').click(function () {
        hideAllTables();
        // Code to show shuttle table if available
        return false;
    });

    // Click event for the Drivers button
    $('#driver').click(function () {
        hideAllTables();
        $('.recentItem.driver').show(); // Show the driver table
        return false;
    });

    // Click event for the Messages button
    $('#messages').click(function () {
        hideAllTables();
        $('.recentItem.recentFeedback').show(); // Show the messages table
        return false;
    });
});
