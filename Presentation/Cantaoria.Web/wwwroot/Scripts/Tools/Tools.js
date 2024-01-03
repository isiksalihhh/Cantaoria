function getData(url, callback) {
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            var data = JSON.parse(xhr.responseText);
            callback(data);
        }
    };
    xhr.open('GET', url, true);
    xhr.send();
}

function deleteRecord(url, id) {
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            window.location.reload();
        }
    };
    xhr.open('DELETE', url + id, true);
    xhr.send();
    location.reload();
}
function formatDateTimeToNormalDate(dateTimeString) {
   
    const dateTimeFormat = new Intl.DateTimeFormat('en', { year: 'numeric', month: 'numeric', day: 'numeric', hour: 'numeric', minute: 'numeric', second: 'numeric', hour12: false });

    const formattedDateTime = dateTimeFormat.format(new Date(dateTimeString));

    return formattedDateTime;
}
