var Feedback = function() {
    function init() {
        $("#follow-up-question").hide();

        $("#feedback-submit-btn").click(function(e) {
            if ($("#form-update-feedback").data('submitting') === true) {
                e.abort();
                return false;
            } else {
                $("#form-update-feedback").data('submitting', true);
            }

            $.ajax({
                type: "POST",
                url: "http://localhost:59955/Home/_AjaxUpdateFeedback",
                data: getFormData,
                success: updateFeedbackOnSuccess,
                error: updateFeedbackOnFailure,
                done: updateFeedbackOnComplete
            });
        });

        $("#feedback-header").on("click", function () {
            $("#feedback-form").toggleClass("hidden");
        });

        $("#star-rating").on("click", ".rate-star", function (e) {
            var span = $(e.target);
            var rate = parseInt(span.attr("data-val"));
            $("#Rate").val(rate); // set model value
            showQuestion(rate);
        }).on("mouseover", ".rate-star", function (e) {
            var span = $(e.target);
            var rate = parseInt(span.attr("data-val"));
            setStarRate(rate);
        }).on("mouseout", ".rate-star", function () {
            var rate = parseInt($("#Rate").val());
            setStarRate(rate);
        });

        //setStarRate(parseInt($("#Rate").val()));
    }

    function showQuestion(rate) {
        $("#follow-up-question").show();
        var questionDiv = $("#question");
        if (rate <= 0) {
            questionDiv.hide();
            return;
        }

        questionDiv.show();
        questionDiv.find(".question").each(function () {
            var fromRate = parseInt($(this).attr("data-from-rate"));
            var toRate = parseInt($(this).attr("data-to-rate"));
            if (rate < fromRate || rate > toRate) {
                $(this).hide();
            } else {
                $(this).show();
                $("#QuestionId").val(parseInt($(this).attr("data-id")));
            }
        });
    }

    function setStarRate(rate) {
        $("#star-rating span").each(function (index, star) {
            if (index < rate) {
                $(star).addClass("star-glow").removeClass("star-fade");
            } else {
                $(star).addClass("star-fade").removeClass("star-glow");
            }
        });
    }

    function getFormData() {
        return {
            customerId: $("#CustomerId").val(),
            userId: $("#UserId").val(),
            isMainPage: $("#IsMainPage").val(),
            pageUrl: $("#PageUrl").val(),
            rate: $("#Rate").val(),
            questionId: $("#QuestionId").val(),
            userReply: $("#UserReply").val()
        };
    }

    function updateFeedbackOnSuccess(data) {
        if (data.type === "success") {
            $("#form-update-feedback").remove();
        } else {
            console.log(data.message);
        }
    }

    function updateFeedbackOnComplete() {
        $("#form-update-feedback").removeData('submitting');
    }

    function updateFeedbackOnFailure() {
        console.log("error on calling ajax");
    }

    return {
        init: init
    }
}();
