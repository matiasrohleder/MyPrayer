$(document).ready(function () {
    $('#meditationTable').DataTable({
        "ajax": {
            "url": "/GuidedMeditation/GetAll",
            "type": "Get",
            "datatype": "json"
        },
        columns: [
            { data: "name" },
            { data: "startDate" },
            { data: "endDate" },
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
                    return `<button class="btn btn-submit" style="margin-right:1em" onclick="window.location.href='/GuidedMeditation/Edit/${full.id}'">Editar</button><button class="btn btn-cancel" onclick="Delete('${full.id}')">Borrar</button>`;
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
    Swal.fire({
        title: "\xbfDesea eliminar el la meditaci\xf3n guiada?",
        text: "Esta acci\xf3n es irreversible",
        type: "info",
        showCancelButton: true,
        cancelButtonText: "No",
        confirmButtonText: "S\xed",
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: `/GuidedMeditation/Delete/${id}`,
                type: 'GET',
                success: function (result) {
                    Swal.fire({
                        title: "\xc9xito",
                        text: "Meditaci\xf3n guiada eliminada",
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
    });
}