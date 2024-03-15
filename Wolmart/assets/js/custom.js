$(document).on('click', ".ratingStar", function () {
    var starValue = $(this).attr("data-value");

    $("#ratingValue").val(starValue);
})

$(document).on('mouseover', ".ratingStar", function () {
    $(".ratingStar").addClass("far").removeClass("fas");

    $(this).addClass("fas").removeClass("far");
    $(this).prevAll(".ratingStar").addClass("fas").removeClass("far");
});
