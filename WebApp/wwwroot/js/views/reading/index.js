$(document).ready(function () {
    $('#readingTable').DataTable({
        "ajax": {
            "url": "/Reading/GetAll",
            "type": "Get",
            "datatype": "json"
        },
        columns: [
            { data: "date" },
            { data: "type" },
            { data: "name" },
            {
                orderable: false,
                "render": function (data, type, full, meta) {
                    return `<button class="btn btn-info" style="margin-right:1em" onclick="window.location.href='/Reading/Edit/${full.id}'">Editar</button>`;
                }
            },
        ],
        language: {
            url: '/lib/datatables/language_es.json',
        },
    });
});