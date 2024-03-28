﻿$(document).ready(function () {
    $('.addToCart').click(function (e) {
        e.preventDefault();

        let colorID = $('.product__color .color.active').data('color-id');
        let sizeID = $('.product__size .size.active').data('size-id');
        let priceText = $('#colorPrice').text();
        let oldPriceText = $('#colorOldPrice').text();

        if (oldPriceText != "" && colorID && sizeID != undefined) {
            let match = priceText.match(/\d+/);

            let match1 = oldPriceText.match(/\d+/);

            if (priceText != "") {
                var priceOriginal = match[0];
            }

            let oldPriceOriginal = match1[0];

            let url = $(this).attr('href') + '?colorID=' + colorID + '&sizeID=' + sizeID + '&price=' + oldPriceOriginal + '&discountedPrice=' + priceOriginal;

            fetch(url).then(res => res.text())
                .then(data => {
                    $('#cart-info').html(data);
                });
        }
        else {
            let url = $(this).attr('href');

            fetch(url).then(res => res.text())
                .then(data => {
                    $('#cart-info').html(data);
                });
        }
    });
});

$(document).ready(function () {
    $('.addToWishlist').click(function (e) {
        alert('hello')
        e.preventDefault();
    });


    $(document).on('click', '.testBtn', function (e) {
        alert('hello')
            e.preventDefault();
        });
});


$(document).on('click', '.clearBtn', function (e) {
    e.preventDefault();

    fetch($(this).attr('href'))
        .then(res => res.text())
        .then(data => {
            $('#cartIndex').html(data);

            fetch('/cart/getcart').then(res => res.text())
                .then(data => {
                    $('#cart-info').html(data);
                })
        })
})

$(document).on('click', '.close-btn', function (e) {
    e.preventDefault();

    fetch($(this).attr('href'))
        .then(res => res.text())
        .then(data => {
            $('#cart-info').html(data);
            fetch('/cart/getcartindex').then(res => res.text())
                .then(data => {
                    $('#cartIndex').html(data);
                })
        })
})

$(document).on('click', '.deleteCart', function (e) {
    e.preventDefault();

    fetch($(this).attr('href'))
        .then(res => res.text())
        .then(data => {
            $('#cartIndex').html(data);
            fetch('/cart/getcart').then(res => res.text())
                .then(data => {
                    $('#cart-info').html(data);
                })
        })
})


$(document).on('click', '.deleteWishlist', function (e) {
    e.preventDefault();

    fetch($(this).attr('href'))
        .then(res => res.text())
        .then(data => {
            $('#wishlistIndex').html(data);
        })
})

$(document).on('click', '.minusCount', function (e) {
    e.preventDefault();

    let inputCount = $(this).closest('.input-group').find('.quantity').val();

    var colorID = $(this).attr("data-color-id")

    var sizeID = $(this).attr("data-size-id")

    if (inputCount >= 2) {
        inputCount--;
        $(this).next().val(inputCount);
        let url = $(this).attr('href') + '/?count=' + inputCount + '&colorID=' + colorID + '&sizeID=' + sizeID;

        fetch(url)
            .then(res => res.text())
            .then(data => {
                $('#cartIndex').html(data);

                fetch('/cart/getcart').then(res => res.text())
                    .then(data => {
                        $('#cart-info').html(data);
                    });
            });
    }
})

$(document).on('click', '.plusCount', function (e) {
    e.preventDefault();

    debugger

    let inputCount = $(this).closest('.input-group').find('.quantity').val();

    var colorID = $(this).attr("data-color-id")

    var sizeID = $(this).attr("data-size-id")

    if (inputCount > 0) {
        inputCount++;
    }
    else {
        inputCount = 1;
    }

    $(this).prev().val(inputCount); inputCount

    let url = $(this).attr('href') + '/?count=' + inputCount + '&colorID=' + colorID + '&sizeID=' + sizeID;


    fetch(url)
        .then(res => res.text())
        .then(data => {
            $('#cartIndex').html(data);

            fetch('/cart/getcart').then(res => res.text())
                .then(data => {
                    $('#cart-info').html(data);
                });
        });

})

$(document).on('mouseover click', ".ratingStar", function (event) {
    event.stopPropagation();
    var starValue = $(this).attr("data-value");
    $("#ratingValue").val(starValue);

    $(".ratingStar").addClass("far").removeClass("fas");
    $(this).addClass("fas").removeClass("far");
    $(this).prevAll(".ratingStar").addClass("fas").removeClass("far");
});


$(document).on('click', ".color", function () {
    var colorPrice = $(this).attr("data-price");
    var discountedPrice = $(this).attr("data-old-price")

    $("#colorPrice").text("$" + discountedPrice);
    $("#colorOldPrice").text("$" + colorPrice)
});

