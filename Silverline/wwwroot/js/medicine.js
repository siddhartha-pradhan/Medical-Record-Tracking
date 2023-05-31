var dataTable;

$(document).ready(function () {
    loadDataTable();
    loadDatasTable();
});

function loadDataTable() {
    dataTable = $('#tableData').DataTable({
        "ajax": {
            "url": "/Admin/Medicine/GetAll"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "unitPrice", "width": "10%" },
            { "data": "type", "width": "10%" },
            { "data": "category", "width": "10%" },
            { "data": "manufacturer", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Medicine/Upsert/${data}" class="btn btn-info text-white" style="cursor: pointer">
                                <i class="fa-solid fa-pen-to-square"></i>
                            </a>
                            <a href="/Admin/Medicine/Delete/${data}" class="btn btn-danger text-white" style="cursor: pointer">
                                <i class="fa-solid fa-trash"></i>
                            </a>
                        </div>
                    `;
                }, "width": "10%"
            }
        ]
    });
}

function loadDatasTable() {
    dataTable = $('#tableDatas').DataTable({
        "ajax": {
            "url": "/Pharmacist/Medicine/GetAll"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "unitPrice", "width": "10%" },
            { "data": "type", "width": "10%" },
            { "data": "category", "width": "10%" },
            { "data": "manufacturer", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Pharmacist/Medicine/Upsert/${data}" class="btn btn-info text-white" style="cursor: pointer">
                                <i class="fa-solid fa-pen-to-square"></i>
                            </a>
                            <a href="/Pharmacist/Medicine/Delete/${data}" class="btn btn-danger text-white" style="cursor: pointer">
                                <i class="fa-solid fa-trash"></i>
                            </a>
                        </div>
                    `;
                }, "width": "10%"
            }
        ]
    });
}