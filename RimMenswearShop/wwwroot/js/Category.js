var role = role || {};

role.delete = function (id) {
    bootbox.confirm({
        title: "Cảnh báo",
        message: "Bạn có muốn xóa vai trò này không?",
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> Không'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> Có'
            }
        },
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: `/Role/Delete/${id}`,
                    method: "GET",
                    contentType: 'json',
                    success: function (data) {
                        if (data.deleteResult) {
                            window.location.href = "/Role/List";
                        }
                        else {
                            bootbox.alert("Sai mã vai trò");
                        }
                    }
                });
            }
        }
    });
}

$(document).ready(function () {
    $("#category").dataTable(
        {
            "columnDefs": [
                {
                    "targets": 2,
                    "orderable": false
                },
            ]
        }
    );
});