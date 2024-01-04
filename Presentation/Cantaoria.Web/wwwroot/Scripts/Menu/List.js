
function populateTable(data) {
    var dataTableBody = document.getElementById('dataTableBody');

    data.forEach(function (item) {
        var row = document.createElement('tr');
        row.innerHTML = `
                        <td>${item.id}</td>
                        <td>${item.name}</td>
                        <td>${item.url}</td>
                        <td>${item.parentMenus.join(', ')}</td>
                        <td>${item.childMenus.join(', ')}</td>
                        <td>${item.order}</td>
                        <td>` +
            (item.isEnabled == true ? `<span class="badge badge-success p-2">Aktif</span>` : `<span class="badge badge-danger p-2">Pasif</span>`)
            + `</td>

                     <td>
                        <a class="btn btn-primary mr-2 btn-icon-split" href="/Menu/Update/${item.id}" >
                             <span class="icon text-white-50">
                                <i class="fa-solid fa-pen"></i>
                             </span>
                        </a>
                        <a class="btn btn-danger btn-icon-split" href="/Menu/Delete/${item.id}">  
                            <span class="icon text-white-50">
                                <i class="fas fa-trash"></i>
                            </span>
                        </a>
                    </td>
                    `;
        dataTableBody.appendChild(row);
    });
}

getData('/Menu/ListAllData', populateTable);
