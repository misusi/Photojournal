var dataTable;
$(document).ready(function () {
    loadDataTable();
});

var maxRowHeight = 200;

function loadDataTable() {
    dataTable = $('#photoblogTable').DataTable({
        "ajax": {
            "url": "/Admin/PhotoblogEntry/GetAll"
        },
        "columns": [
            { "data": "title", "width": "10%" },
            {
                "data": "description",
                "width": "25%",
                "render": function (data) {
                    return `
                        <div style="overflow-x:hidden;height:${maxRowHeight}px;scrollbar-width: thin">
                            ${data}
                        </div>
                    `
                }
            },
            {
                "data": "photoList.[].imageUrl",
                "width": "55%",
                "render": function (data) {
                    var htmlToReturn = "";
                    for (let i = 0; i < data.length; i++) {
                        htmlToReturn += `   <div style="overflow-x:ignore;scrollbar-width:thin;" class="mx-2 d-flex">`;
                        htmlToReturn += `       <img style="height:${maxRowHeight}px" src="${data[i]}"\>`;
                        htmlToReturn += "   <div/>";
                    }
                    return `
                        ${htmlToReturn}
                    `;
                }
            },
            {
                "data": "description",
                "width": "10%",
                "render": function (data) {
                    return `
                        <div class="btn-group">
                            <a class="btn btn-primary mx-1"><i class="bi bi-pencil-square"></i></a>
                            <a class="btn btn-danger mx-1"><i class="bi bi-trash3"></i></a>
                        </div>
                    `
                }
            }
        ]
    });
}