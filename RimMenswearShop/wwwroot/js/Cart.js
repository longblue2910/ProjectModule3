var Cart = Cart || {};

Cart.AddItem = function (id) {
    var amount = $(`#${id}`).val();
    //var UserId = $("#UserId").val();
    $.ajax({
        url: `/Cart/AddItem/${id}/${amount}`,
        method: "GET",
        contentType: 'json',
        success: function (data) {
            console.log(data);
            if (data == -1) {
                bootbox.alert("<p style='color: green'>Thêm Sản Phẩm Thành Công</p>");
            }
            else {
                bootbox.alert("<p style='color: green'>Thêm Sản Phẩm Thành Công</p>");
                $("#CartNum").html(parseInt($("#CartNum").text(), 10) + parseInt(1, 10));
            }
        }
    });

}

