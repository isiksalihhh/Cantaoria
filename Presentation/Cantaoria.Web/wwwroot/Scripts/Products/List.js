document.addEventListener('DOMContentLoaded', function () {
    // Function to make an AJAX request
    function fetchData(url, callback) {
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

    function populateTable(data) {
        var dataTableBody = document.getElementById('dataTableBody');

        data.forEach(function (item) {
            var row = document.createElement('tr');
            row.innerHTML = `
                        <td>${item.id}</td>
                        <td>${item.name}</td>
                        <td>${item.description}</td>
                        <td>${item.price}</td>
                        <td>${item.stockQuantity}</td>
                        <td>${item.categoryID}</td>
                        <td>${item.createdDate}</td>
                        <td>${item.updatedDate}</td>
                        <td>${item.isEnabled}</td>
                    <td>
                        <button onclick="editRow(${item.id})">Edit</button>
                        <a href="/Products/Delete/${item.id}">Delete</a>
                    </td>
                    `;
            dataTableBody.appendChild(row);
        });

        // Initialize DataTable
        var dataTableInstance = $('#dataTable').DataTable({
            // DataTable options
        });
    }

    // Make AJAX request and populate the table
    fetchData('/Products/ListAllData', populateTable);
});