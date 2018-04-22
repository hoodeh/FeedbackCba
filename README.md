# FeedbackCba
This is a lightweight application that can be added to web applications and enables collecting optional feedbacks from users 
in application level (main page) and sub level pages in order to calculate the net promoter score.

To use this application first you need a valid customer id which is generated after the agreement. 
To make the application more flexible each account can have customized statement, application level question, sub-level question and also 
feedback menu bar colour. The follow up questions and also their rating can be defined too e.g.
Rating 1-4: What can we do to improve?
Rating 5-8: Is there anything we can do to improve?
Rating: 9-10: Is there anything you particularly like?
You also need to provide the valid hosts that you would use the feedback on them to prevent others using your script and customer id.
To enable the feedback in your application and other pages you just need to add the following script into your page and 
set the variables accordingly.


<script>
    // Set your variables
    var customerId // we will provide you the guid
    var userId;  // set userId if applicable
    var pageUrl;  // set pageUrl (default is the current page url)
    var isMainPage;  // set to true if it is main page (default is true)
 
    $.ajax({
        url:"http://cbafeedback.azurewebsites.net/home/feedback",
        data: {
            customerId: customerId,
            userId: userId,
            pageUrl: pageUrl,
            isMainPage: isMainPage
        },
        method: "get",
        xhrFields: { withCredentials: true },
    }).done(function(feedbackView){
        $("body").append(feedbackView);
    });
</script>
