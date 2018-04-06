var Feedback = function() {

    function init() {
        $("#second-part").hide();

        $("#star-rating").on("click", ".rate-star", function(e) {
                var span = $(e.target);
                var score = parseInt(span.attr("data-val"));
                $("#Score").val(score); // set model value
                showQuestion(score);
        }).on("mouseover", ".rate-star", function(e) {
            var span = $(e.target);
            var score = parseInt(span.attr("data-val"));
            //console.log("F:" + score);
            setStarRate(score);
        }).on("mouseout", ".rate-star", function () {
            var score = parseInt($("#Score").val());
            //console.log("F:" + score);
            setStarRate(score);
        });

        setCookie("feedbackUserId", $("#UserId").val());
        setStarRate(parseInt($("#Score").val()));
    }

    function showQuestion(score) {
        $("#second-part").show();
        var questionDiv = $("#question");
        if (score <= 0) {
            questionDiv.hide();
            return;
        }

        questionDiv.find(".question").hide();
        questionDiv.show();
        if (score < 5) {
            questionDiv.find(".rate-low").show();
        } else if (score < 9) {
            questionDiv.find(".rate-mid").show();
        } else {
            questionDiv.find(".rate-high").show();
        }
    }

    function setCookie(name, value) {
        document.cookie = name + "=" + value + ";path=/";
    }

    function editFeedback() {
        //console.log("edit");
        $("#feedback-result").hide();
        $("#star-rating").show();
        $("#star-rating span").addClass("rate-star");
        showQuestion($("#Score").val());
    }

    function setStarRate(score) {
        //$("#star-rating .rate-star.star-glow").removeClass("star-glow").addClass("star-fade");
        $("#star-rating span").each(function(index, star) {
            if (index < score) {
                $(star).addClass("star-glow").removeClass("star-fade");
            } else {
                $(star).addClass("star-fade").removeClass("star-glow");
            }
        });
    }

    return {
        init: init,
        showQuestion: showQuestion,
        setCookie: setCookie,
        editFeedback:editFeedback,
        setStarRate: setStarRate
    }
}();
