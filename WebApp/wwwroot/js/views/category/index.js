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
                        return data === 'Active' ? "S\xed" : "No";
                    }
                    if (data) {
                        return "S\xed";
                    } else {
                        return "No";
                    }
                    return data;
                },
            },
            {
                orderable: false,
                "render": function (data, type, full, meta) {
                    return `<button class="btn btn-submit" style="margin-right:1em" onclick="window.location.href='/Category/Edit/${full.id}'">Editar</button><button class="btn btn-cancel" onclick="Delete('${full.id}')">Borrar</button>`;
                }
            },
        ],
        language: {
            url: '/lib/datatables/language_es.json',
        },
    });
});

function Delete(id)
{
    $.ajax({
        url: `/Category/Delete/${id}`,
        type: 'GET',
        success: function (result) {
            Swal.fire({
                title: "\xc9xito",
                text: "Categor\xeda eliminada",
                type: "success"
            }).then((result) => {
                window.location.reload();
            });
        },
        error: function () {
            Swal.fire({
                title: "No es posible eliminar",
                text: "Hay contenidos pertenecientes a esta categor\xeda",
                type: "error"
            });
        }
    });
}