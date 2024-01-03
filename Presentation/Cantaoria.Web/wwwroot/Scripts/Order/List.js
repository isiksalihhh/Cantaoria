
function populateTable(data) {
    var dataTableBody = document.getElementById('dataTableBody');

    data.forEach(function (item) {
        var row = document.createElement('tr');
        row.innerHTML = `
                        <td>${item.id}</td>
                        <td>${item.name}</td>
                        <td>${item.description}</td>
                        <td>${item.address}</td>
                        <td>${formatDateTimeToNormalDate(item.orderDate)}</td>
                        <td>${item.totalAmount}</td>
                        <td>` +
            (item.isEnabled == true ? `<span class="badge badge-success p-2">Aktif</span>` : `<span class="badge badge-danger p-2">Pasif</span>`)
            + `</td>

                    <td>
                        <a class="btn btn-info btn-icon-split" href="/Order/Update/${item.id}" >
                             <span class="icon text-white-50">
                                <i class="fa-solid fa-pen"></i>
                             </span>
                        </a>

                    </td>
                    `;
        dataTableBody.appendChild(row);
    });
}

getData('/Order/ListAllData', populateTable);
