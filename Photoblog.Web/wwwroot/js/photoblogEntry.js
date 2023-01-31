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
            { "data": "title" },
            { "data": "description" },
            {
                "data": "photoList.[].imageUrl",
                "render": function (data) {
                    var htmlToReturn = "";
                    for (let i = 0; i < data.length; i++) {
                        htmlToReturn += "<div>";
                        htmlToReturn += "   <img src=\"${data[i]}\"\>";
                        htmlToReturn += "<div/>";
                    }
                    return `
                        ${htmlToReturn}
                    `;
                }
            },
            //{
            //    "data": "description",
            //    "render": function (data) {
            //        return `
            //            <div style="overflow-x:hidden;height:${maxRowHeight}px;scrollbar-width: thin">
            //                ${data}
            //            </div>
            //        `
            //    },
            //    "width": "20%"
            //},
            //{
            //    "data": "imageUrl",
            //    "render": function (data) {
            //        return `
            //            <div>
            //                <img src="${data}" style="object-fit:cover;width:100%;" \>
            //            </div>
            //        `
            //    },
            //    "width": "15%"
            //},
            //{
            //    "data": "id",
            //    "render": function (data) {
            //        return `
            //            <div class="w-35 btn-group" role="group" >
            //                <a href="/Admin/Product/Upsert?id=${data}" 
            //                class="btn btn-primary mx-1"><i class="bi bi-pencil-square"></i> Edit</a>
            //                <a onClick=Delete('/Admin/Product/Delete/${data}')
            //                class="btn btn-danger mx-1"><i class="bi bi-pencil-square"></i> Delete</a>
            //            </div>
            //            `
            //    },
            //    "width": "7%"
            //},
        ]
    });
}