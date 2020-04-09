var cart = {
    init: function () {
        cart.registerEven();
    },
    registerEven: function () {
        $('#btnDeleteAll').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                url: '/Cart/DeleteAll/',
                dataType: 'json',
                type: 'POST',
                success: function (response) {
                    if (response.status == true) {
                        window.location.href = '/Home/';
                    }
                }
            });
        });
        $('.btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            var row = $(this).closest("tr");
            var id = $(this).data('id');
            $.ajax({
                url: '/Cart/Delete/',
                dataType: 'json',
                data: { id: id },
                type: 'POST',
                success: function (response) {
                    if (response.status == true) {
                        //window.location.href = '/Cart/';
                        row.remove();
                    }
                }
            });
        });
    }
}
cart.init();
function changequality(value, tt, IDProduct) {
    var id = "quantity+" + value;
    var id2 = "thanhtien+" + value;
    var gt = parseInt(document.getElementById(id).innerText);
    if (gt > 0) {
        if (tt == 'down') {
            gt--;
        }
        else gt++;
    }
    var CartItem = [
        {
            Quantity: gt, //lấy giá trị hiện tại của textbox đó
            ProductModel: {
                ID: IDProduct
            }
        }
    ];
    $.ajax({
        url: '/Cart/Update/',
        dataType: 'json',
        data: { cartModel: JSON.stringify(CartItem) }, //chuyển mảng thành một chuỗi,  
        type: 'POST',
        success: function (response) {
            if (response.status == true) {
                document.getElementById("tongtien").innerHTML = response.Tong;
                document.getElementById(id2).innerHTML = response.Thanhtien;
                document.getElementById(id).innerText = gt;
            }
        }
    });
}