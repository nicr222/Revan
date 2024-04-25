document.getElementById('searchInput').addEventListener('input', function () {
    var searchValue = this.value.toLowerCase();
    var shuttleRows = document.getElementsByClassName('shuttleRow');

    for (var i = 0; i < shuttleRows.length; i++) {
        var shuttleNumber = shuttleRows[i].getElementsByTagName('td')[0].innerText.toLowerCase();
        var passengerCapacity = shuttleRows[i].getElementsByTagName('td')[1].innerText.toLowerCase();

        if (shuttleNumber.includes(searchValue) || passengerCapacity.includes(searchValue)) {
            shuttleRows[i].style.display = '';
        } else {
            shuttleRows[i].style.display = 'none';
        }
    }
});