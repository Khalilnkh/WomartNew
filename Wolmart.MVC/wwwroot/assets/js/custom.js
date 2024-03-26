$(document).ready(function () {
    $('.addToCart').click(function (e) {
        e.preventDefault();

        let url = $(this).attr('href');

        fetch(url).then(res => res.text())
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

$(document).on('click', '.minusCount', function (e) {
    e.preventDefault();

    let inputCount = $(this).closest('.input-group').find('.quantity').val();

    if (inputCount >= 2) {
        inputCount--;
        $(this).next().val(inputCount);
        let url = $(this).attr('href') + '/?count=' + inputCount;

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

    let inputCount = $(this).closest('.input-group').find('.quantity').val();

    if (inputCount > 0) {
        inputCount++;
    }
    else {
        inputCount = 1;
    }

    $(this).prev().val(inputCount); inputCount

    let url = $(this).attr('href') + '/?count=' + inputCount;


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

    $("#colorPrice").text("$" + colorPrice);
    $("#colorOldPrice").text("$" + discountedPrice)
});

