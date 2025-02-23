var dataTable;

$(document).ready(function () {
    loadDataTable();
    loadDatasTable();
});

function loadDataTable() {
    dataTable = $('#tableData').DataTable({
        "ajax": {
            "url": "/Admin/Category/GetAll"
        },
        "columns": [
            { "data": "name", "width": "40%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Category/Upsert/${data}" class="btn btn-info text-white" style="cursor: pointer">
                                <i class="fa-solid fa-pen-to-square"></i>
                            </a>
                            <a href="/Admin/Category/Delete/${data}" class="btn btn-danger text-white" style="cursor: pointer">
                                <i class="fa-solid fa-trash"></i>
                            </a>
                        </div>
                    `;
                }, "width": "30%"
            }
        ]
    });
}

function loadDatasTable() {
    dataTable = $('#tableDatas').DataTable({
        "ajax": {
            "url": "/Pharmacist/Category/GetAll"
        },
        "columns": [
            { "data": "name", "width": "40%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Pharmacist/Category/Upsert/${data}" class="btn btn-info text-white" style="cursor: pointer">
                                <i class="fa-solid fa-pen-to-square"></i>
                            </a>
                            <a href="/Pharmacist/Category/Delete/${data}" class="btn btn-danger text-white" style="cursor: pointer">
                                <i class="fa-solid fa-trash"></i>
                            </a>
                        </div>
                    `;
                }, "width": "30%"
            }
        ]
    });
}