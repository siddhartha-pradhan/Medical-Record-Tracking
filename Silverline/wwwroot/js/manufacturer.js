var dataTable;

$(document).ready(function () {
    loadDataTable();
    loadDatasTable();
});

function loadDataTable() {
    dataTable = $('#tableData').DataTable({
        "ajax": {
            "url": "/Admin/Manufacturer/GetAll"
        },
        "columns": [
            { "data": "name", "width": "40%" },
            { "data": "location", "width": "20%" },
            { "data": "isActive", "width": "10%" },
            {
                "data": {
                    id: "id", isActive: "isActive"
                },
                "render": function (data) {
                    var isActive = data.isActive;
                    if (isActive) {
                        return `
                        <div class="text-center">
                            <a href="/Admin/Manufacturer/Upsert/${data.id}" class="btn btn-info text-white" style="cursor: pointer">
                                <i class="fa-solid fa-pen-to-square"></i>
                            </a>
                            <a href="/Admin/Manufacturer/Delete/${data.id}" class="btn btn-danger text-white" style="cursor: pointer">
                                <i class="fa-solid fa-trash"></i>
                            </a>
                            <a onclick=update('${data.id}') class="btn btn-danger text-white" style="cursor: pointer">
                                <i class="fas fa-lock"></i> Deactivate
                            </a>
                        </div>
                    `;
                    }
                    else {
                        return `
                        <div class="text-center">
                            <a href="/Admin/Manufacturer/Upsert/${data.id}" class="btn btn-info text-white" style="cursor: pointer">
                                <i class="fa-solid fa-pen-to-square"></i>
                            </a>
                            <a href="/Admin/Manufacturer/Delete/${data.id}" class="btn btn-danger text-white" style="cursor: pointer">
                                <i class="fa-solid fa-trash"></i>
                            </a>
                            <a onclick=update('${data.id}') class="btn btn-success text-white" style="cursor: pointer">
                                <i class="fas fa-lock-open"></i> Activate
                            </a>
                        </div>
                        </div>
                    `;
                    }
                    
                }, "width": "30%"
            }
        ]
    });
}

function loadDatasTable() {
    dataTable = $('#tableDatas').DataTable({
        "ajax": {
            "url": "/Pharmacist/Manufacturer/GetAll"
        },
        "columns": [
            { "data": "name", "width": "40%" },
            { "data": "location", "width": "20%" },
            { "data": "isActive", "width": "10%" },
            {
                "data": {
                    id: "id", isActive: "isActive"
                },
                "render": function (data) {
                    var isActive = data.isActive;
                    if (isActive) {
                        return `
                        <div class="text-center">
                            <a href="/Admin/Manufacturer/Upsert/${data.id}" class="btn btn-info text-white" style="cursor: pointer">
                                <i class="fa-solid fa-pen-to-square"></i>
                            </a>
                            <a href="/Admin/Manufacturer/Delete/${data.id}" class="btn btn-danger text-white" style="cursor: pointer">
                                <i class="fa-solid fa-trash"></i>
                            </a>
                            <a onclick=update('${data.id}') class="btn btn-danger text-white" style="cursor: pointer">
                                <i class="fas fa-lock"></i> Deactivate
                            </a>
                        </div>
                    `;
                    }
                    else {
                        return `
                        <div class="text-center">
                            <a href="/Admin/Manufacturer/Upsert/${data.id}" class="btn btn-info text-white" style="cursor: pointer">
                                <i class="fa-solid fa-pen-to-square"></i>
                            </a>
                            <a href="/Admin/Manufacturer/Delete/${data.id}" class="btn btn-danger text-white" style="cursor: pointer">
                                <i class="fa-solid fa-trash"></i>
                            </a>
                            <a onclick=update('${data.id}') class="btn btn-success text-white" style="cursor: pointer">
                                <i class="fas fa-lock-open"></i> Activate
                            </a>
                        </div>
                        </div>
                    `;
                    }

                }, "width": "30%"
            }
        ]
    });
}

function update(id) {
    $.ajax({
        url: 'Manufacturer/UpdateManufacturer/' + id,
        type: 'POST',
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        data: JSON.stringify({ id: id })
    });
    return false;
}