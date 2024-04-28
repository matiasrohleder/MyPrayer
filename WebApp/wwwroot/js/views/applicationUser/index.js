$(document).ready(function () {
    $('#user-table').DataTable({
        "ajax": {
            "url": "/ApplicationUser/GetAll",
            "type": "Get",
            "datatype": "json"
        },
        columns: [
            { data: "name" },
            {
                orderable: false,
                "render": function (data, type, full, meta) {
                    return `<button class="btn btn-submit" style="margin-right:1em" onclick="window.location.href='/ApplicationUser/Edit/${full.id}'">Editar</button><button class="btn btn-cancel" onclick="Delete('${full.id}')">Borrar</button>`;
                }
            },
        ],
        language: {
            url: '/lib/datatables/language_es.json',
        },
    });
});

function Delete(id) {
    $.ajax({
        url: `/ApplicationUser/Delete/${id}`,
        type: 'GET',
        success: function () {
            Swal.fire({
                title: "\xc9xito",
                text: "Usuario eliminado",
                type: "success"
            }).then(() => {
                window.location.reload();
            });
        },
        error: function () {
            Swal.fire({
                title: "No es posible eliminar",
                text: "No se puede eliminar al usuario administrador",
                type: "error"
            });
        }
    });
}