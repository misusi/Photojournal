var dataTable;
$(document).ready(function () {
    loadDataTable();
});

var maxRowHeight = 200;

function loadDataTable() {
    dataTable = $('#journalEntryTable').DataTable({
        "ajax": {
            "url": "/Admin/JournalEntry/GetAll"
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
                "data": "id",
                "width": "10%",
                "render": function (data) {
                    return `
                        <div class="btn-group">
                            <a href="/Admin/JournalEntry/Edit?id=${data}" 
                            class="btn btn-primary mx-1"><i class="bi bi-pencil-square"></i></a>
                            <a onClick=Delete('/Admin/JournalEntry/Delete/${data}')
                            class="btn btn-danger mx-1"><i class="bi bi-trash3"></i></a>
                        </div>
                    `
                }
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}