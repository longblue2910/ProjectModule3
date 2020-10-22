var CustomerCart = CustomerCart || {};

CustomerCart.AddItem = function (id) {

    
    //var UserId = $("#UserId").val();
    $.ajax({
        url: `/Cart/AddItem/${id}`,
        method: "GET",
        contentType: 'json',
        success: function (data) {
            console.log(data);
            if (data == -1) {
                bootbox.alert("<span>Đã thêm vào giỏ hàng</span>");
            }
            else {
                bootbox.alert("<span>Đã thêm vào giỏ hàng</p>");
                $("#CartNum").html(parseInt($("#CartNum").text(), 10) + parseInt(1, 10));
            }
        }
    });
}


CustomerCart.OrderByAccount = function (id) {
    $.ajax({
        url: `/Cart/OrderByAccount/${id}`,
        method: "GET",
        contentType: 'json',
        success: function (data) {
            console.log(data);
            if (data > 0) {
                bootbox.alert({
                    message: "<p style='color: green'>Đặt Hàng Thành Công, Xe được giao hàng trong 3 ngày tới</p><p style='color: green'>Xin Cảm Ơn !</p>",
                    callback: function () {
                        window.location.href = "/CustomerHome/Index/";
                    }
                });
            }
        }
    });
}
