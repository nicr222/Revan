document.addEventListener("DOMContentLoaded", function () {
    document.getElementById('searchInput').addEventListener('input', function () {
        var searchValue = this.value.trim().toLowerCase();
        var messageRows = document.getElementsByClassName('messageRow');

        for (var i = 0; i < messageRows.length; i++) {
            var messageName = messageRows[i].getElementsByTagName('td')[0].innerText.toLowerCase();
            var messageContent = messageRows[i].getElementsByTagName('td')[1].innerText.toLowerCase();

            if (messageName.includes(searchValue) || messageContent.includes(searchValue)) {
                messageRows[i].style.display = '';
            } else {
                messageRows[i].style.display = 'none';
            }
        }
    });
});