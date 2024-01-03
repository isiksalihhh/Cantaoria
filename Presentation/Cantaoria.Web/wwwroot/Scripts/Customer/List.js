
function populateTable(data) {
    var dataTableBody = document.getElementById('dataTableBody');

    data.forEach(function (item) {
        var row = document.createElement('tr');
        row.innerHTML = `
                        <td>${item.id}</td>
                        <td>${item.firstName}</td>
                        <td>${item.lastName}</td>
                        <td>${item.email}</td>
                        <td>${item.phone}</td>
                        <td>` +
            (item.isEnabled == true ? `<span class="badge badge-success p-2">Aktif</span>` : `<span class="badge badge-danger p-2">Pasif</span>`)
            + `</td>
                    `;
        dataTableBody.appendChild(row);
    });
}

getData('/Customer/ListAllData', populateTable);
