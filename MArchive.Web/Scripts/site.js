$(document).ready(function () {
    $(".notificationDismiss").click(function () {
        $("#notification").fadeOut("slow");
        $(".validationSummaryContainer").fadeOut("slow");
    });
});

function clearURL(url) {
    if (url == undefined)
        return url;

    return url.replace("/&amp;", "&");
    return url;
}

function makeSilentAjaxForm(formSelector, beforeSubmitFunc, errorFunc, successFunc) {
    var options = {
        beforeSubmit: function (arr, jqForm, options) {
            if (beforeSubmitFunc != null) {
                beforeSubmitFunc(arr, jqForm, options);
            }
            else {
                validateAjaxForm(arr, jqForm, options);
            }
        },
        error: function (responseText, statusText, xhr, $form) {
            if (statusText == "error") {
                try {
                    var response = jQuery.parseJSON(responseText.responseText);
                    notifyUser("ERROR: " + response.data, -1, "error");
                } catch (e) {
                    notifyUser("ERROR: " + responseText.responseText, -1, "error");
                }

            }
        }
        ,
        success: showSuccess,
        target: null,
        replaceTarget: false
    };


    if (errorFunc != null) {
        options.error = errorFunc;
    }
    if (successFunc != null) {
        options.success = successFunc;
    }

    $(formSelector).ajaxForm(options);
}

function validateAjaxForm(arr, jqForm, options) {
    $('.clientValidationMessages').show();
    RebindJqueryValidations(jqForm);
    return jqForm.validate().form();
}

function RebindJqueryValidations(jqForm) {
    /*
    According to :  http://stackoverflow.com/questions/4406291/jquery-validate-unobtrusive-not-working-with-dynamic-injected-elements/5783020#5783020
    Sayfaya sonradan yüklenen formlar için jquery'nin validator'unu tekrar parse ettirmek gerekiyor.  
    Aksi takdirde modal dialog'lardaki form'lar için client side validation çalışmıyor.  
    */
    jqForm.removeData("validator");
    jqForm.removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(jqForm);
}

function notifyUser(message, duration, notificationType) {
    $("#notification>#notificationText").html(message);

    $("#notification").removeClass("yellowBackground");
    $("#notification").removeClass("greenBackground");
    $("#notification").removeClass("redBackground");

    if (notificationType == "error") {
        $("#notification").addClass("redBackground");
    } else if (notificationType == "warning") {
        $("#notification").addClass("yellowBackground");
    } else if (notificationType == "info") {
        $("#notification").addClass("greenBackground");
    }

    if (duration < 0) {
        $("#notification").fadeIn("slow");
    } else {
        $("#notification").fadeIn("fast").delay(duration).fadeOut("fast");
    }

}

function showSuccess() {
    notifyUser("Action(s) performed successfully", 1000, "info");
}