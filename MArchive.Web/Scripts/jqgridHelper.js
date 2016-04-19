// Initialize List



function ListInitialize(params) {
    height = '100%';
    var divPager = $(params.pager);
    jQuery(params.table).jqGrid({
        url: clearURL(params.actionLink),
        width: (params.tableWidth == null || params.tableWidth == '') ? GetMaximumWidth() : params.tableWidth,
        height: height,
        datatype: 'json',
        mtype: 'POST',
        colNames: params.columnNames,
        colModel: params.columnModel,
        pager: divPager,
        rowNum: (params.rowNum == null || params.rowNum == 0) ? 10 : params.rowNum,
        rowList: [5, 10, 20, 50, 100],
        sortname: params.sortName,
        sortorder: params.sortOrder,
        cellsubmit: 'clientArray',
        viewrecords: true,
        caption: params.gridCaption,
        autowidth: false,
        select: true,
        multiselect: params.multiselect,
        onSelectRow: params.onRowSelect,
        editurl: clearURL(params.editurl),
        cellEdit: params.cellEdit,
        afterSaveCell: params.afterSaveCell,
        loadError: function (xhr, status, error) {
            // external dependency 
            showError(xhr.responseText);
        }
    });

    //Nav Items
    $(params.table).jqGrid('navGrid', params.pager, { edit: false, view: false, add: false, del: false, search: true, refresh: true, searchtext: "Search" }, {}, {}, {}, { closeOnEscape: true, multipleSearch: true, closeAfterSearch: true }, {});
    $(params.table).jqGrid('filterToolbar', { stringResult: true, searchOnEnter: true });
    $(params.table).jqGrid('setGridParam', { onSelectAll: params.onSelectAll });
    $('.ui-jqgrid-titlebar-close').remove();
}

function ListInitializeForLocalData(params) {
    height = '100%';
    var divPager = $(params.pager);
    jQuery(params.table).jqGrid({
        width: (params.tableWidth == null || params.tableWidth == '') ? GetMaximumWidth() : params.tableWidth,
        height: height,
        //datatype: 'json',
        datatype: 'jsonstring',
        //data: params.data,
        datastr: params.data,
        datatype: params.datatype,
        colNames: params.columnNames,
        colModel: params.columnModel,
        pager: divPager,
        rowNum: (params.rowNum == null || params.rowNum == 0) ? 10 : params.rowNum,
        rowList: [5, 10, 20, 50, 100],
        sortname: params.sortName,
        sortorder: params.sortOrder,
        cellsubmit: 'clientArray',
        viewrecords: true,
        caption: params.gridCaption,
        autowidth: false,
        select: true,
        multiselect: params.multiselect,
        onSelectRow: params.onRowSelect,
        editurl: clearURL(params.editurl),
        cellEdit: params.cellEdit,
        afterSaveCell: params.afterSaveCell,
        loadError: function (xhr, status, error) {
            // external dependency 
            showError(xhr.responseText);
        }
    });

    //Nav Items
    $(params.table).jqGrid('navGrid', params.pager, { edit: false, view: false, add: false, del: false, search: true, refresh: true, searchtext: "Search" }, {}, {}, {}, { closeOnEscape: true, multipleSearch: true, closeAfterSearch: true }, {});
    $(params.table).jqGrid('filterToolbar', { stringResult: true, searchOnEnter: true });
    $(params.table).jqGrid('setGridParam', { onSelectAll: params.onSelectAll });
    $('.ui-jqgrid-titlebar-close').remove();
}

function GetMaximumWidth() {
    var width = $("#mainPageContainer").width();
    return width;
}



function StandardTextCol(fieldName, width) {
    var col = { name: fieldName, index: fieldName, width: width, searchoptions: { sopt: ['cn', 'bw', 'eq'] } };
    return col;
}

function StandardEditableCol(fieldName, width) {
    var col = { name: fieldName, index: fieldName, width: width, editable: true, searchoptions: { sopt: ['cn', 'bw', 'eq'] } };
    return col;
}

function StandardHiddenCol(fieldName) {
    var col = { name: fieldName, index: fieldName, hidden: true };
    return col;
}


function StandardIdCol(fieldName, width) {
    var col = { name: fieldName, index: fieldName, width: width, searchoptions: { sopt: ['eq', 'gt', 'lt'] } };

    return col;
}

function StandardUrlCol(fieldName, width) {
    var col = {
        name: fieldName, width: width, searchoptions: { sopt: ['cn'] }, formatter: function (cellvalue, options, rowObject) {
            return '<a style="text-decoration: underline;" target="_blank" href="' + cellvalue + '" >' + cellvalue + '</a>';
        }
    };
    return col;
}

function StandardHtmlCol(fieldName, width, numberOfCharacter) {
    var col = {
        name: fieldName, width: width, searchoptions: { sopt: ['cn'] }, formatter: function (cellvalue, options, rowObject) {
            return $(cellvalue).text().substr(0, numberOfCharacter) + "...";
        }
    };
    return col;
}

function StandardDateCol(fieldName, width) {
    var col = {
        name: fieldName,
        index: fieldName,
        searchoptions: {
            sopt: ['dge', 'deq', 'dlt'],
            dataInit: function (el) {
                $(el).datepicker({
                    dateFormat: 'yy-mm-dd',
                    changeYear: true,
                    changeMonth: true
                });
            }
        },
        width: width
    };
    return col;
}

function GetSelectedRow(tableId) {
    var list = $("#" + tableId);
    var selectedRow = list.getGridParam("selrow");
    var rowData = list.getRowData(selectedRow);
    return rowData;
}

function GetSelectedIdList(tableId) {
    var currentSelection = $('#' + tableId).jqGrid('getGridParam', 'selarrrow');
    return currentSelection;
}

function GetSelectedRowsField(tableId, fieldName) {
    var valueArray = [];
    var currentSelection = $('#' + tableId).jqGrid('getGridParam', 'selarrrow');
    for (var i = 0; i < currentSelection.length; i++) {
        var celValue = $('#' + tableId).jqGrid('getCell', currentSelection[i], fieldName);
        valueArray[i] = celValue;
    }
}


function StandardFormatPrice(fieldName, width, idColIndex) {
    var col = {
        name: fieldName, width: width, searchoptions: { sopt: ['cn'] }, formatter: 'currency', formatoptions: { prefix: '', suffix: '', thousandsSeparator: '' }
    };
    return col;
}

function StandardImagesCol(fieldName, width, imgWidth, imgHeight) {
    var col = {
        name: fieldName, width: width, formatter: function (cellvalue, options, rowObject) {
            if (cellvalue != "")
                return "<img style='max-height:60px;max-width:60px;' src='" + cellvalue + "' title='" + cellvalue + "' />";
            else
                return "";
        }
    }
    return col;
}

function SliderContentCol(fieldName, width, contentWidth, contentHeight) {

    var col = {
        name: fieldName, width: width, formatter: function (cellvalue, options, rowObject) {
            var content = null;

            if (cellvalue.ImageUrl) {
                content = $("<img />")
                .attr("src", cellvalue.ImageUrl)
                .attr("title", cellvalue.Title)
                .attr("alt", cellvalue.AltText);
            }

            if (contentWidth)
                $(content).css("max-width", contentWidth);

            if (contentHeight)
                $(content).css("max-height", contentHeight);

            return $(content).prop("outerHTML");
        }
    }
    return col;
}

function ArrangeUrlWithID(url, btn) {
    if (url != null) {
        $("#" + btn).attr("href", clearURL(url));
    }
}
function ArrangeUrlWithClass(url, btn) {
    if (url != null) {
        $("." + btn).attr("href", clearURL(url));
    }
}