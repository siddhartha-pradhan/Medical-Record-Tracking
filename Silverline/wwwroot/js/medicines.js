var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tableData').DataTable({
        "ajax": {
            "url": "/Doctor/Medicine/GetAll"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "unitPrice", "width": "10%" },
            { "data": "type", "width": "15%" },
            { "data": "category", "width": "15%" },
            { "data": "manufacturer", "width": "20%" },
        ]
    });
}