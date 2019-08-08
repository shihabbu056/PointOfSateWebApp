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
                    $("#productCodeTB").val("" + response.Code);
                    //$("#productCodeTB").empty();
                    //var options = "";
                    //$.each(response,
                    //    function (key, product) {
                    //        options = product.Code;
                    //    });

                    //$("#productCodeTB").append(options);

                },
                error: function (response) {

                }

            });
    });


    // Many Product Add Code in Table
    var index = 0;
    var sl = 0;
    $('#addButton').click(() => {
        var product = getProductForForm();

        var row = getRowForProductPurchase(product);

        $('#tbProductPurchase').append(row);

        index++;
        sl++;
    });

    $('#iDelete').click(() => {
       row.


    });




    // Product Details Added Value

    function getProductForForm() {
        var sl = "";
        var productId = $("#productDD").val();
        //var productName = $("#productDD option:selected").text();
        var productCode = $("#productCodeTB").val();
        var manufacturedDate = $("#ManufacturedDate").val();
        var expireDate = $("#ExpireDate").val();
        var quantity = $("#Quantity").val();
        var unitPrice = $("#UnitPrice").val();
        var totalPrice = quantity * unitPrice;
        var newMrp = $("#NewMrp").val();
        var remarks = $("#Remarks").val();

        return { '':sl, ProductId: productId, Code: productCode, ManufacturedDate: manufacturedDate, ExpireDate: expireDate, Quantity: quantity, UnitPrice: unitPrice, TotalPrice: totalPrice, NewMrp: newMrp, Remarks: remarks }
    }

    // get Row For Product Purchase

    function getRowForProductPurchase(productPurchase) {

        var codeHidden = "<input type='hidden' name='PurchaseDetails[" + index + "].Code' value='" + productPurchase.Code + "' />";
        var manufacturedDateHidden = "<input type='hidden' name='PurchaseDetails[" + index + "].ManufacturedDate' value='" + productPurchase.ManufacturedDate + "' />";
        var expireDateHidden = "<input type='hidden' name='PurchaseDetails[" + index + "].ExpireDate' value='" + productPurchase.ExpireDate + "' />";
        var quantityHidden = "<input type='hidden' name='PurchaseDetails[" + index + "].Quantity' value='" + productPurchase.Quantity + "' />";
        var unitPriceHidden = "<input type='hidden' name='PurchaseDetails[" + index + "].UnitPrice' value='" + productPurchase.UnitPrice + "' />";
        var totalPriceHidden = "<input type='hidden' name='PurchaseDetails[" + index + "].TotalPrice' value='" + productPurchase.TotalPrice + "' />";
        var newMrpHidden = "<input type='hidden' name='PurchaseDetails[" + index + "].NewMrp' value='" + productPurchase.NewMrp + "' />";
        var remarksHidden = "<input type='hidden' name='PurchaseDetails[" + index + "].Remarks' value='" + productPurchase.Remarks + "' />";

        var tr = "<tr>";
        var slCell = "<td>" + sl + "</td>";
        var codeCell = "<td>" + codeHidden + productPurchase.Code + "</td>";
        var manufacturedDateCell = "<td>" + manufacturedDateHidden + productPurchase.ManufacturedDate + "</td>";
        var expireDateCell = "<td>" + expireDateHidden + productPurchase.ExpireDate + "</td>";
        var quantityCell = "<td>" + quantityHidden + productPurchase.Quantity + "</td>";
        var unitPriceCell = "<td>" + unitPriceHidden + productPurchase.UnitPrice + "</td>";
        var totalPriceCell = "<td>" + totalPriceHidden + productPurchase.TotalPrice + "</td>";
        var newMrpCell = "<td>" + newMrpHidden + productPurchase.NewMrp + "</td>";
        var remarksCell = "<td>" + remarksHidden + productPurchase.Remarks + "</td>";
        var actionCell = "<td> <input type='button' value='Delete' name='Delete' id='iDelete'/><input type='button' value='Edite' name='Edite'/> </td>"
        actionCell[index];
        var endTr = "</tr>";
        var row = tr + slCell + codeCell + manufacturedDateCell + expireDateCell + quantityCell + unitPriceCell + totalPriceCell + newMrpCell + remarksCell + actionCell + endTr;

        return row;
    }


//function getPurchaseProductRecord() {
//    var productCode = $('#')
//}






});
