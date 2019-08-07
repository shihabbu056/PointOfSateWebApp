$(document).ready(function() {

    $("#categoryDD").change(function () {
        var selectedCategoryId = $("#categoryDD").val();
        var jsonData = { categoryId: selectedCategoryId };

        $.ajax(
            {
                url: "/Product/GetByCategory",
                data: jsonData,
                type: "POST",
                success: function (response) {
                    $("#productDD").empty();
                    var options = "<option >Select...</option>";
                    $.each(response,
                        function (key, product) {
                            options += "<option value='" + product.Id + "'>" + product.Name + "</option>";
                        });

                    $("#productDD").append(options);

                },
                error: function (response) {

                }

            });
    });



    //Product Select and On the Text Box Product Code Show

    $("#productDD").change(function () {
        var selectedProductId = $("#productDD").val();
        var jsonData = { productId: selectedProductId };

        $.ajax(
            {
                url: "/Product/GetByProductCode",
                data: jsonData,
                type: "POST",
                success: function (response) {
                    $("#productCodeTB").empty();
                    var options = "";
                    $.each(response,
                        function (key, product) {
                            options = product.Code;
                        });

                    $("#productCodeTB").append(options);

                },
                error: function (response) {

                }

            });
    });

    $('#addButton').click(() => {

    });
});




//function getPurchaseProductRecord() {
//    var productCode = $('#')
//}