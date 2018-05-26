var deleteProcessing = false;

$(document).ready(function () {
    $("#fileuploader").uploadFile({
        url: uploadPhotoUrl,
        fileName: "photo",
        //autoSubmit: false,
        //showAbort: false,
        showFileCounter: false,
        showFileSize: false,
        allowedTypes: "jpg,jpeg,png",
        sequential: true,
        sequentialCount: 1,
        showPreview: true,
        multiple: true,
        //previewHeight: "30px",
        //previewWidth: "30px",
        showDelete: true,
        formData: {
            "objectID": objectID
        },
        deleteCallback: function (photoID, pd) {
            deleteProcessing = true;
            //console.log("before deleting: " + deleteProcessing);
            $.ajax({
                type: "POST",
                url: deletePhotoUrl,
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({
                    objectID: objectID,
                    photoID: Number(photoID)
                }),
                //dataType: "json",
                success: function (data) {
                    console.log(data);
                    var mainPhotoID = data;
                    if (mainPhotoID > 0)
                        MarkPhotoAsMain(mainPhotoID);
                },
                error: function (data) {
                    //alert("error: " + data);
                },
                complete: function () {
                    deleteProcessing = false;
                }
            });
        },
        onLoad: function (obj) {
            if (existingPhotos.length > 0) {
                for (var i = 0; i < existingPhotos.length; i++) {
                    obj.createProgress(existingPhotos[i].name, existingPhotos[i].path, existingPhotos[i].size, existingPhotos[i].isMain);
                }
            }

            OnLoadExtraCallback();
        },
        onSuccess: function (files, data, xhr, pd) {
            var photoID = data;
            //console.log("marking photo as main: " + photoID);
            var bar = $(".add_cars_photoprev > .photo_prev").not(".existingBar").last();
            $(bar).attr("id", photoID).addClass("existingBar").attr("onclick", "MakePhotoMain(" + photoID + ")");

            if ($(".add_cars_photoprev > .photo_prev.active ").length == 0)
                MarkPhotoAsMain(photoID);
        },
        onError: function (response) {
            //alert(response);
        }
    });
});

function OnLoadExtraCallback()
{
    $(".ajax-file-upload").css("display", "none");

    var dropZoneInnerHtml = dropZoneInnerText;
    $(".dropZone").append(dropZoneInnerHtml);
    $(".dropZone").css("font-weight", "400");
    $(".dropZone").css("margin-bottom", "0");

    if (pageName == "AddAutoExtraInfo")
    {
        var photoNoteBoxHtml = "<div class='note'>" + photoRequirements + "<br /><a href='#'>" + photoMakingSuggestions + "</a></div>";
        $(".dropZone").after(photoNoteBoxHtml);

        var selectMainPhotoHintHtml = "<div class='note'>" + selectMainPhotoHint + "</div>";
        $(".add_cars_photoprev").after(selectMainPhotoHintHtml);
    }
}

function MakePhotoMain(photoID) {
    //console.log("before marking: " + deleteProcessing);
    if (deleteProcessing)
        return;
    if (photoID == 0)
        return;

    $.ajax({
        type: "POST",
        url: makePhotoMainUrl,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            photoID: Number(photoID)
        }),
        //dataType: "json",
        success: function (data) {
            MarkPhotoAsMain(photoID);
        },
        error: function (data) {
            //alert("error: " + data);
        }
    });
}

function MarkPhotoAsMain(photoID) {
    //alert("marking photo: " + photoID);
    $(".photo_prev").removeClass('active');
    $(".photo_prev#" + photoID).addClass('active');
}