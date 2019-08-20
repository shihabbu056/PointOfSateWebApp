$(document).ready(function() {

    $("#categoryDD").change(function () {
        var selectedCategoryId = $("#categoryDD").val();
        var jsonData = { categoryId: selectedCategoryId };

        $.ajax(
            {
                url: "/Product/GetByProduct",
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

    $("#productDD").change(function() {
        var selectedProduct = $("#productDD").val();
        var jsonData = { productId: selectedProduct };
        $.ajax({
            url: "/Purchase/GetByPurchaseDetail",
            data: jsonData,
            type: "POST",
            success: function(purchaseDetail) {
                $("#previousCostPrice").val("" + purchaseDetail.UnitPrice);
                $("#PreviousMrp").val("" + purchaseDetail.NewMrp);
            },
            error: function(response) {

            }
        });
    });


    //var quantity = $("#Quantity").val();
    //var unitPrice = $("#UnitPrice").val();
    //var totalPrice = quantity * unitPrice;

    $("#UnitPrice").change(function() {
        var quantity = $("#Quantity").val();
        var unitPrice = $("#UnitPrice").val();
        var totalPrice = quantity * unitPrice;
        $("#totalPrice").val("" + totalPrice);
    });


    //$('#addButton').click(() => {
    //    $("#manufacturedDate").val("");
    //    $("#expireDate").val("");
    //    $("#quantity").val("");
    //    $("#unitPrice").val("");
    //    $("#totalPrice").val("");
    //    $("#previousCostPrice").val("");
    //    $("#previousMrp").val("");
    //    $("#newMrp").val("");

    //});


    // Many Product Add Code in Table
    var index = 0;
    var sl = 0;
    $('#addButton').click(() => {
        var product = getProductForForm();

        var row = getRowForProductPurchase(product);

        $('#tbProductPurchase').append(row);

        index++;
        sl++;


        //Text Box Empty.
        $("#ManufacturedDate").val("");
        $("#ExpireDate").val("");
        $("#Quantity").val("");
        $("#UnitPrice").val("");
        $("#NewMrp").val("");
        $("#Remarks").val("");
        $("#totalPrice").val("");
    });

    //$('#iDelete').click(() => {
       
    //    $(this).closest('tr').remove();
    //    return false;

    //});

    $(document).on('click', '#iDelete', function () {
        $(this).closest('tr').remove();
        return false;

    });




    // Product Details Added Value

    function getProductForForm() {
        sl = "";
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

        var productHidden = "<input type='hidden' name='PurchaseDetails[" + index + "].ProductId' value='" + productPurchase.ProductId + "' />";
        var manufacturedDateHidden = "<input type='hidden' name='PurchaseDetails[" + index + "].ManufacturedDate' value='" + productPurchase.ManufacturedDate + "' />";
        var expireDateHidden = "<input type='hidden' name='PurchaseDetails[" + index + "].ExpireDate' value='" + productPurchase.ExpireDate + "' />";
        var quantityHidden = "<input type='hidden' name='PurchaseDetails[" + index + "].Quantity' value='" + productPurchase.Quantity + "' />";
        var unitPriceHidden = "<input type='hidden' name='PurchaseDetails[" + index + "].UnitPrice' value='" + productPurchase.UnitPrice + "' />";
        var totalPriceHidden = "<input type='hidden' name='PurchaseDetails[" + index + "].TotalPrice' value='" + productPurchase.TotalPrice + "' />";
        var newMrpHidden = "<input type='hidden' name='PurchaseDetails[" + index + "].NewMrp' value='" + productPurchase.NewMrp + "' />";
        var remarksHidden = "<input type='hidden' name='PurchaseDetails[" + index + "].Remarks' value='" + productPurchase.Remarks + "' />";

        var tr = "<tr>";
        var slCell = "<td>" + sl + "</td>";
        var codeCell = "<td>" + productHidden + productPurchase.Code + "</td>";
        var manufacturedDateCell = "<td>" + manufacturedDateHidden + productPurchase.ManufacturedDate + "</td>";
        var expireDateCell = "<td>" + expireDateHidden + productPurchase.ExpireDate + "</td>";
        var quantityCell = "<td>" + quantityHidden + productPurchase.Quantity + "</td>";
        var unitPriceCell = "<td>" + unitPriceHidden + productPurchase.UnitPrice + "</td>";
        var totalPriceCell = "<td>" + totalPriceHidden + productPurchase.TotalPrice + "</td>";
        var newMrpCell = "<td>" + newMrpHidden + productPurchase.NewMrp + "</td>";
        var remarksCell = "<td>" + remarksHidden + productPurchase.Remarks + "</td>";
        var actionCell = "<td> <input type='button' value='Delete' name='Delete' id='iDelete'/><input type='button' value='Edite' name='Edite'/> </td>";
        //actionCell[index];
        var endTr = "</tr>";
        var row = tr + slCell + codeCell + manufacturedDateCell + expireDateCell + quantityCell + unitPriceCell + totalPriceCell + newMrpCell + remarksCell + actionCell + endTr;


        return row;

    }


//function getPurchaseProductRecord() {
//    var productCode = $('#')
//}






});
