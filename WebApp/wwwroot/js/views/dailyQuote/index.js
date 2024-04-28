$(document).ready(function () {
    $('#quoteTable').DataTable({
        "ajax": {
            "url": "/DailyQuote/GetAll",
            "type": "Get",
            "datatype": "json"
        },
        columns: [
            { data: "date" },
            { data: "name" },
            {
                orderable: false,
                "render": function (data, type, full, meta) {
                    return `<button class="btn btn-submit" style="margin-right:1em" onclick="window.location.href='/DailyQuote/Edit/${full.id}'">Editar</button><button class="btn btn-cancel" onclick="Delete('${full.id}')">Borrar</button>`;
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
        url: `/DailyQuote/Delete/${id}`,
        type: 'GET',
        success: function (result) {
            Swal.fire({
                title: "\xc9xito",
                text: "Frase diaria eliminado",
                type: "success"
            }).then((result) => {
                window.location.reload();
            });
        },
        error: function () {
            Swal.fire({
                title: "No fue posible eliminar",
                text: "Ocurri\xf3 un error inesperado. Intentar nuevamente.",
                type: "error"
            });
        }
    });
}