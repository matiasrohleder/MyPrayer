$(document).ready(function () {
    $('#categoryTable').DataTable({
        "ajax": {
            "url": "/Category/GetAll",
            "type": "Get",
            "datatype": "json"
        },
        columns: [
            { data: "name" },
            { data: "order" },
            {
                data: "active",
                render: function (data, type, row) {
                    if (type === 'myExport') {
                        return data === 'Active' ? "Si" : "No";
                    }
                    if (data) {
                        return "Si";
                    } else {
                        return "No";
                    }
                    return data;
                },
            },
            {
                "render": function (data, type, full, meta) {
                    return '<button class="btn btn-info" href="/Category/Edit/' + full.id + '">Editar</button>';
                }
            },
        ],
        language: {
            url: '//cdn.datatables.net/plug-ins/2.0.0/i18n/es-ES.json',
        },
    });
});