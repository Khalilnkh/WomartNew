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
