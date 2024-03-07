// Setup event handlers for action buttons on server-side mode
if (typeof setupEventHandlersForDatatableButtonsServerSide === 'undefined') {
    setupEventHandlersForDatatableButtonsServerSide = (row, table) => {
        let datatable = table.DataTable();
        row.find(".datatable-action-button").each(function (index, button) {
            $(button).on("click", function () {
                let actionData = $(this).data("action")

                actionHandler(actionData, datatable, table)
            })
        })
    }
}

// Setup event handlers for action buttons on client-side mode
if (typeof setupEventHandlersForDatatableButtonsClientSide === 'undefined') {
    setupEventHandlersForDatatableButtonsClientSide = (table) => {
        let datatable = table.DataTable();
        $(".datatable-action-button").each(function (index, button) {
            $(button).on("click", function () {
                let actionData = $(this).data("action")

                actionHandler(actionData, datatable, table)
            })
        })
    }
}

function actionHandler(actionData, datatable, table) {
    // Redirect buttons
    if (actionData.typeOfButton === "redirect") {
        window.open(actionData.url, actionData.target)
    }

    let ajaxData = null;
    if (actionData.type.toLowerCase() == "post")
        ajaxData = {
            "__RequestVerificationToken": table.children('[name="__RequestVerificationToken"]').val()
        };

    // Decision buttons
    if (actionData.typeOfButton === "decision") {
        swal({
            text: actionData.modal.message,
            type: actionData.modal.typeOfAlert,
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: actionData.modal.confirmLabel,
            cancelButtonText: actionData.modal.cancelLabel
        }).then(function (selection) {
            if (selection.value) {
                $.ajax({
                    dataType: 'json',
                    type: actionData.type,
                    url: actionData.url,
                    data: ajaxData,
                    success: function (result) {
                        swal(actionData.successTitle, result.message, "success").then(function () {
                            datatable.ajax.reload();
                        });
                    },
                    error: function (error) {
                        swal({
                            type: 'warning',
                            text: error.responseJSON && error.responseJSON.message ? error.responseJSON.message : datatableResources["Ignite.Framework.WebTools.Js.Datatables.ExecuteActionFailedEx"],
                            confirmButtonColor: '#28a745'
                        });
                    }
                });
            }
        })
    }

    // Action buttons
    if (actionData.typeOfButton == "action") {
        // Disable button to prevent multiple clicks
        let button = $(this)
        button.attr("disabled", true)
        $.ajax({
            dataType: 'json',
            type: actionData.type,
            url: actionData.url,
            data: ajaxData,
            success: function (result) {
                swal(actionData.successTitle, result.message, "success").then(function () {
                    datatable.ajax.reload();
                });
            },
            error: function (error) {
                swal({
                    type: 'warning',
                    text: error.responseJSON && error.responseJSON.message ? error.responseJSON.message : datatableResources["Ignite.Framework.WebTools.Js.Datatables.ExecuteActionFailedEx"],
                    confirmButtonColor: '#28a745'
                });
            },
            complete: function () {
                button.attr("disabled", false)
            }
        });
    }
}


if (typeof createButtonsForDatatableRows === 'undefined') {
    createButtonsForDatatableRows = (actions, container) => {

        actions.forEach(function (action) {
            let button = `
                <button type="button" class="btn waves-effect waves-light ${action.class} ml-2 btn-sm ${action.internalName}-action datatable-action-button" title="${action.displayName}" id="${action.internalName}">
                    <i class="fa fa-${action.icon}"></i>
                    ${action.showDisplayName ? action.displayName : ''}
                </button>`

            container.append(button)

            // Set data for each action button
            container.find(`.${action.internalName}-action`).first().data({
                "action": action
            })
        })
    }
}

dataTableInitiator = (table) => {

    let options = {
        columnsToDisplay: [],
        columnsToHide: [],
        columnsToExport: [],
        ajaxConfig: undefined,
        serverSide: false,
        deferLoading: table.data("defer-loading") != undefined,
        disableColumnsReorder: table.data("disable-columns-reorder") != undefined,
        disableStateSave: table.data("disable-state-save") != undefined,
        hideActionButtons: table.data("hide-action-buttons") !== undefined,
        hiddenButtons: $("ignite-table").attr('hidden-buttons'),
        sortBy: table.data("sort-by"),
        sortDirection: table.data("sort-direction").toLowerCase(),
        clientSide: table.data("client-side") != undefined,
        defaultButtons: [],
        language: {
            "processing": datatableResources["Ignite.Framework.WebTools.Js.Datatables.LanguageProcessing"],
            "lengthMenu": datatableResources["Ignite.Framework.WebTools.Js.Datatables.LanguageLengthMenu"],
            "zeroRecords": datatableResources["Ignite.Framework.WebTools.Js.Datatables.LanguageZeroRecords"],
            "emptyTable": datatableResources["Ignite.Framework.WebTools.Js.Datatables.LanguageEmptyTable"],
            "info": datatableResources["Ignite.Framework.WebTools.Js.Datatables.LanguageInfo"],
            "infoEmpty": datatableResources["Ignite.Framework.WebTools.Js.Datatables.LanguageInfoEmpty"],
            "infoFiltered": datatableResources["Ignite.Framework.WebTools.Js.Datatables.LanguageInfoFiltered"],
            "search": datatableResources["Ignite.Framework.WebTools.Js.Datatables.LanguageSearch"],
            "thousands": datatableResources[""],
            "loadingRecords": datatableResources["Ignite.Framework.WebTools.Js.Datatables.LanguageLoadingRecords"],
            "paginate": {
                "first": datatableResources["Ignite.Framework.WebTools.Js.Datatables.LanguagePaginateFirst"],
                "last": datatableResources["Ignite.Framework.WebTools.Js.Datatables.LanguagePaginateLast"],
                "next": datatableResources["Ignite.Framework.WebTools.Js.Datatables.LanguagePaginateNext"],
                "previous": datatableResources["Ignite.Framework.WebTools.Js.Datatables.LanguagePaginatePrevious"]
            },
            "aria": {
                "sortAscending": datatableResources["Ignite.Framework.WebTools.Js.Datatables.LanguageAriaSortAscending"],
                "sortDescending": datatableResources["Ignite.Framework.WebTools.Js.Datatables.LanguageAriaSortDescending"]
            }
        }
    };

    const getUrl = table.data("get-url")
    if (!options.clientSide) {
        options.ajaxConfig = {
            url: getUrl,
            type: 'POST',
            data: {
                "__RequestVerificationToken": table.children('[name="__RequestVerificationToken"]').val()
            }
        }
        options.serverSide = true
    }

    if (!options.serverSide) {
        options.hideActionButtons = true;
        options.deferLoading = true;
    }

    table.find("th[data-column-name]").each(function (index, column) {
        const shouldBeHidden = $(this).data("hidden") != undefined
        if (shouldBeHidden) {
            options.columnsToHide.push(index)
        }

        options.columnsToDisplay.push({
            data: $(column).data("columnName")
        })
        options.columnsToExport.push(index)
    })

    options.defaultButtons = [
        { extend: 'csv', text: 'CSV', exportOptions: { columns: options.columnsToExport } },
        { extend: 'excel', text: 'Excel', exportOptions: { columns: options.columnsToExport } },
        { extend: 'pdf', text: 'PDF', exportOptions: { columns: options.columnsToExport } },
        { extend: 'print', text: datatableResources["Ignite.Framework.WebTools.Js.Datatables.ButtonsPrint"], exportOptions: { columns: options.columnsToExport } },
        { extend: 'colvis', columns: '.toggle-visibility-enabled', text: datatableResources["Ignite.Framework.WebTools.Js.Datatables.ButtonsColumnsVisibility"] }
    ];

    if (options.hiddenButtons) {
        options.hiddenButtons = options.hiddenButtons.split(",")
        options.defaultButtons = options.defaultButtons.filter(button => {
            return options.hiddenButtons.indexOf(button.extend) == -1
        })
    }

    // Setup ordering   
    options.sortByIndex = options.columnsToDisplay
        .map(function (item) { return item.data })
        .indexOf(options.sortBy)

    // Column selected for ordering is not available as column within the table
    if (options.sortByIndex == -1) {
        options.sortByIndex = 0
        options.sortDirection = "asc"
    }

    if (!options.hideActionButtons) {
        // Additional column for actions
        options.columnsToDisplay.push({
            render: function () {
                let html = `
				<div class="d-flex datatable-row-actions">
				</div>
				`;
                return html;
            },
            orderable: false
        })
    }

    var datatable = buildDataTable(table, options);

    if (!options.disableColumnsReorder) {
        // Setup column-reorder callback
        datatable.on('column-reorder', function (e, settings, details) {
            const isUserReorder = details.drop == undefined

            if (isUserReorder) {
                setupResponsiveAndActionButtons(table, options.hideActionButtons, options.clientSide)
            }
        });
    }

    if (options.clientSide) {
        setupEventHandlersForDatatableButtonsClientSide(table);
    }

    return datatable
};

const buildDataTable = (table, options) => {
    var datatable = table.DataTable({
        columns: options.columnsToDisplay,
        columnDefs: [
            {
                render: function (data, type, full, meta) {
                    if (data) {
                        if (isObject(data) && data.type === 'DataTableColorColumn') {
                            if (data.data != null) {
                                return `<div title="${data.alt}" class="datatable-color-column" style="background-color:${data.data}"></div>`
                            } else {
                                return `<div class="datatable-color-column"></div>`
                            }
                        } else if (isObject(data)) {
                            let output = '<div class="list d-flex">'
                            let limit = data.limit > 0 && data.data.length > data.limit ? data.limit : data.data.length
                            for (let i = 0; i < limit; i++) {
                                const specificTagStyle = data.stylePerValue ? data.stylePerValue.find(s => s.value == data.data[i]) : null
                                if (specificTagStyle) {
                                    output += `<p class='list-item ${getTagStyle(specificTagStyle.style)}'>${data.data[i]}</p>`
                                } else {
                                    output += `<p class='list-item ${getTagStyle(data.defaultStyle)}'>${data.data[i]}</p>`
                                }
                            }
                            if (data.limit > 0 && data.data.length > data.limit) {
                                output += `<p class='list-item ${data.style} additional'><i class='fa fa-plus'></i> ${data.data.length - data.limit}</p>`
                            }
                            return `${output}</div>`
                        } else if (typeof data === 'string') {
                            return `<div class="wrap">${(data)}</div>`;
                        }
                    }
                    return `<div class="wrap">${(data || datatableResources["Ignite.Framework.WebTools.Js.Datatables.NoData"])}</div>`;
                },
                targets: "_all"
            },
            { "visible": false, "targets": options.columnsToHide }
        ],
        ordering: true,
        serverSide: options.serverSide,
        iDisplayLength: 50,
        lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, datatableResources["Ignite.Framework.WebTools.Js.Datatables.LengthMenuAll"]]],
        buttons: {
            buttons: options.defaultButtons,
            dom: {
                button: {
                    className: "btn btn-outline-secondary mr-2"
                }
            }
        },
        stateSave: !options.disableStateSave,
        stateDuration: 0,
        colReorder: !options.disableColumnsReorder,
        ajax: options.ajaxConfig,
        deferLoading: options.deferLoading,
        order: [
            [options.sortByIndex, options.sortDirection]
        ],
        responsive: {
            details: {
                renderer: function (api, rowIdx, columns) {
                    let actions = api.rows().data()[rowIdx].actions
                    let data = $.map(columns, function (col, i) {
                        if (col.hidden) {
                            if (col.data.indexOf("datatable-row-actions") > -1 && actions != null) {
                                let childRow = $(
                                    `<div class="row-child-container d-flex" data-dt-row="${col.rowIndex}" data-dt-column="${col.columnIndex}">` +
                                    `<p>${col.title}:</p>` +
                                    '<div class="datatable-row-actions"/>' +
                                    '</div>')

                                return childRow.html()

                            } else {
                                return `<div class="row-child-container" data-dt-row="${col.rowIndex}" data-dt-column="${col.columnIndex}">` +
                                    `<p>${col.title}:</p> ${col.data}` +
                                    '</div>'
                            }
                        }
                        return ''
                    }).join('');

                    if (data) {
                        let result = $('<div/>').append(data)

                        if (!options.hideActionButtons && options.serverSide) {
                            // Create buttons
                            createButtonsForDatatableRows(actions, result.find('.datatable-row-actions'))

                            // Setup event handlers
                            setupEventHandlersForDatatableButtonsServerSide(result, table)
                        }

                        if (!options.hideActionButtons && options.clientSide) {
                            // Setup event handlers
                            setupEventHandlersForDatatableButtonsClientSide(table);
                        }

                        return result
                    }
                    return false
                }
            }
        },
        language: options.language,
        dom: '<"mr-2 buttons-container"l>Bfrt<"dataTables_footer"ip>',
        drawCallback: function () {
            setupResponsiveAndActionButtons(table, options.hideActionButtons, options.clientSide)
        },
        pageLength: 50,
        initComplete: function (settings, json) {
            let api = new $.fn.dataTable.Api(settings);
            let columnsVisibilityButton = $(".dt-buttons .buttons-colvis")
            const columnVisibilityEnabled = columnsVisibilityButton && columnsVisibilityButton.length > 0;

            // Adds the user preferences section
            let buttonsContainer = table.parent().find('.buttons-container')
            buttonsContainer.append(`
                <div class="datatable-preferences-container hide">
                    <p class="title">${datatableResources["Ignite.Framework.WebTools.Js.Datatables.UserPreferencesTitle"]}</p>
                </div>
            `)
            let preferencesContainer = buttonsContainer.find(".datatable-preferences-container")

            // Move columns visibility button into user preferences section
            if (columnVisibilityEnabled)
                columnsVisibilityButton.detach().appendTo(preferencesContainer);

            // Adds an icon for the column visibility button
            preferencesContainer.find(".buttons-colvis").prepend(`<i class="fa fa-eye mr-2"></i>`)

            preferencesContainer.find(".buttons-colvis").css("display", "inline-block")

            if (!options.disableStateSave) {
                // Create and append the reset state button within the user preferences section
                const clearStateButton = $(`
                    <button class="mr-2 btn btn-secondary" title="${datatableResources["Ignite.Framework.WebTools.Js.Datatables.CleanStateButtonTooltip"]}">
                        <i class="fa fa-undo"></i>
                        ${datatableResources["Ignite.Framework.WebTools.Js.Datatables.CleanStateButton"]}
                    </button>                
                `)
                clearStateButton.on('click', function () {
                    api.state.clear()
                    window.location.reload();
                })

                preferencesContainer.append(clearStateButton)
            }

            const userPreferencesEnabled = !options.disableStateSave || columnVisibilityEnabled;
            if (userPreferencesEnabled)
                preferencesContainer.removeClass("hide")

            setupResponsiveAndActionButtons(table, options.hideActionButtons, options.clientSide)
        }
    });
    
	setupLoadingSpinner(table, datatable)

    return datatable;
}

setupLoadingSpinner = (table, datatable) => {
    datatable.on("preXhr", function () {
		table.find("tbody tr.odd").html(
            `<td class='dataTables_empty' colspan="99">
                <i class="c-inline-spinner"></i>${datatableResources["Ignite.Framework.WebTools.Js.Datatables.LanguageLoadingRecords"]}
             </td>`
            );
        });
        
    datatable.on("xhr", function (e, settings, json, xhr) {
		if (json.data.length == 0) {
			table.find(".dataTables_empty").html(datatableResources["Ignite.Framework.WebTools.Js.Datatables.LanguageEmptyTable"]);
		}
	});
}

setupResponsiveAndActionButtons = (table, hideActionButtons, isClientSide) => {
    let datatable = table.DataTable();
    let rows = table.find("tbody tr")
    let data = datatable.rows().data()

    data.each(function (row, index) {

        // Add expand / collapse buttons for responsive
        let firstCol = $(rows[index]).find("td:first-child");
        firstCol.addClass("responsive-column")
        firstCol.find("i").remove()
        let expandIcon = $("<i class='fa fa-plus'></i>");
        let collapseIcon = $("<i class='fa fa-minus'></i>");
        expandIcon.insertBefore(firstCol.children().first());
        collapseIcon.insertBefore(firstCol.children().first());

        if (!hideActionButtons && !isClientSide) {
            let actionsContainer = $(rows[index]).find(".datatable-row-actions").first()
            actionsContainer.empty()
            // Create buttons
            createButtonsForDatatableRows(row.actions, actionsContainer)

            // Setup event handlers
            setupEventHandlersForDatatableButtonsServerSide(actionsContainer, table)
        }

        if (!hideActionButtons && isClientSide) {
            // Setup event handlers
            setupEventHandlersForDatatableButtonsClientSide(table);
        }
    })
}

isObject = (obj) => {
    return obj != null && obj.constructor.name === "Object"
}
