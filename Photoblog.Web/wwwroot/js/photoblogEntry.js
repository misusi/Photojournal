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
                        htmlToReturn += `<div class="row">`;
                        htmlToReturn += `   <div style="overflow-x:hidden;height:${maxRowHeight}px;scrollbar-width:thin;width:100%;" 
                                             class="col d-flex mx-2">`;
                        htmlToReturn += `       <img style="object-fit:cover;" src="${data[i]}"\>`;
                        htmlToReturn += "   <div/>";
                        htmlToReturn += "<div/>";
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