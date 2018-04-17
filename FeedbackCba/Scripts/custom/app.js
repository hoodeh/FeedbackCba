var Feedback = function() {
    this.init = function(options) {
        this.options = options;
        var me = this;

        $("#follow-up-question").hide();

        $("#feedback-submit-btn").click(function() {
            
            $.ajax({
                type: "POST",
                xhrFields: { withCredentials: true },
                url: me.options.apiUrl + "customers/" + options.customerId + "/feedbacks",
                data: JSON.stringify(me.getFormData()),
                contentType: "application/json",
                success: updateFeedbackOnSuccess,
                error: updateFeedbackOnFailure
            });
        });

        $("#feedback-header").on("click",
            function() {
                $("#feedback-form").toggleClass("hidden");
            });

        $("#star-rating").on("click", ".rate-star", function (e) {
            var span = $(e.target);
                var rate = parseInt(span.attr("data-val"));
                $("#rate").val(rate);
                showQuestion(rate);
        }).on("mouseover", ".rate-star", function (e) {
            var span = $(e.target);
                var rate = parseInt(span.attr("data-val"));
                setStarRate(rate);
        }).on("mouseout", ".rate-star", function () {
            var rate = parseInt($("#rate").val());
                setStarRate(rate);
            });
    };

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
                $("#question-id").val(parseInt($(this).attr("data-id")));
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

    this.getFormData = function() {
        return {
            userId: this.options.userId,
            isMainPage: this.options.isMainPage,
            pageUrl: this.options.pageUrl,
            rate: $("#rate").val(),
            questionId: $("#question-id").val(),
            userReply: $("#user-reply").val()
        };
    }

    function updateFeedbackOnSuccess(result) {
        if (result.type === "success") {
            $("#form-update-feedback").remove();
        } else {
            console.log(result.message);
        }
    }

    function updateFeedbackOnFailure() {
        console.log("error on calling ajax");
    }

};
