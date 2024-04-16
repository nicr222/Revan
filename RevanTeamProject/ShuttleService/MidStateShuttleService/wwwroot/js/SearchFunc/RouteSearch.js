document.getElementById('searchInput').addEventListener('input', function () {
    var searchValue = this.value.toLowerCase();
    var routeRows = document.getElementsByClassName('routeRow');

    for (var i = 0; i < routeRows.length; i++) {
        var pickupLocation = routeRows[i].getElementsByTagName('td')[1].innerText.toLowerCase();
        var dropoffLocation = routeRows[i].getElementsByTagName('td')[2].innerText.toLowerCase();

        if (pickupLocation.includes(searchValue) || dropoffLocation.includes(searchValue)) {
            routeRows[i].style.display = '';
        } else {
            routeRows[i].style.display = 'none';
        }
    }
});