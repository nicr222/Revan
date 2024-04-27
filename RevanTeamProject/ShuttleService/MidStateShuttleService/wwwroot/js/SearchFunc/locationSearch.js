
    document.getElementById('searchInput').addEventListener('input', function () {
        var searchValue = this.value.trim().toLowerCase();
        var locationRows = document.getElementsByClassName('locationRow');

        for (var i = 0; i < locationRows.length; i++) {
            var stopName = locationRows[i].getElementsByTagName('td')[0].innerText.toLowerCase();
            
            var abbreviation = locationRows[i].getElementsByTagName('td')[5].innerText.toLowerCase();

            if (stopName.includes(searchValue) || address.includes(searchValue) || city.includes(searchValue) || state.includes(searchValue) || zipCode.includes(searchValue) || abbreviation.includes(searchValue)) {
                locationRows[i].style.display = '';
            } else {
                locationRows[i].style.display = 'none';
            }
        }
