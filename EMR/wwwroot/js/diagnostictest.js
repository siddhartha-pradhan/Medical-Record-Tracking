var dataTable;

$(document).ready(function () {
    loadDataTable();
    loadDatasTable();
});

function loadDataTable() {
    dataTable = $('#tableData').DataTable({
        "ajax": {
            "url": "/Admin/Test/GetAll"
        },
        "columns": [
            { "data": "title", "width": "20%" },
            { "data": "initialRange", "width": "10%" },
            { "data": "finalRange", "width": "10%" },
            { "data": "unit", "width": "10%" },
            { "data": "unitPrice", "width": "10%" },
            { "data": "testType", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Test/Upsert/${data}" class="btn btn-info text-white" style="cursor: pointer">
                                <i class="fa-solid fa-pen-to-square"></i>
                            </a>
                            <a href="/Admin/Test/Delete/${data}" class="btn btn-info text-white" style="cursor: pointer">
                                <i class="fa-solid fa-trash"></i>
                            </a>
                        </div>
                    `;
                }, "width": "20%"
            }
        ]
    });
}

function loadDatasTable() {
    dataTable = $('#tableDatas').DataTable({
        "ajax": {
            "url": "/LabTechnician/Test/GetAll"
        },
        "columns": [
            { "data": "title", "width": "20%" },
            { "data": "initialRange", "width": "10%" },
            { "data": "finalRange", "width": "10%" },
            { "data": "unit", "width": "10%" },
            { "data": "unitPrice", "width": "10%" },
            { "data": "testType", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/LabTechnician/Test/Upsert/${data}" class="btn btn-info text-white" style="cursor: pointer">
                                <i class="fa-solid fa-pen-to-square"></i>
                            </a>
                            <a href="/LabTechnician/Test/Delete/${data}" class="btn btn-info text-white" style="cursor: pointer">
                                <i class="fa-solid fa-trash"></i>
                            </a>
                        </div>
                    `;
                }, "width": "20%"
            }
        ]
    });
}