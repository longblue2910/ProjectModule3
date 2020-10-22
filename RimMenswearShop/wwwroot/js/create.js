var create = create || {};
create.changeProvince = function (id) {
    create.drawDistrict(id);
}
create.drawDistrict = function (TinhThanhId) {
    $.ajax({
        url: `/Orders/Districts/${TinhThanhId}`,
        method: "GET",
        contentType: "json",
        success: function (data) {
            console.log(data);
            $("#DistrictId").empty();
            $.each(data.districts, function (i, v) {
                $("#DistrictId").append(`
                     <option value="${v.id}">${v._name}</option>
                `);
            });
            create.drawWard($("#DistrictId").val());
        }
    });
}
create.changeDistrict = function (id) {
    create.drawWard(id);
}
create.drawWard = function (QuanHuyenId) {
    $.ajax({
        url: `/Orders/Wards/${QuanHuyenId}`,
        method: "GET",
        contentType: "json",
        success: function (data) {
            console.log(data);
            $("#WardId").empty();
            $.each(data.wards, function (i, v) {
                $("#WardId").append(`
                     <option value="${v.id}">${v._name}</option>
                `);
            });
        }
    });
}