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
                "width": "65%",
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
        ]
    });
}