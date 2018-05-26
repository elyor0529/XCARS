$(document).ready(function () {
    var disabled = false;
    if (searchVM.TransportTypeID != null && searchVM.TransportTypeID != 0)
        disabled = true;
    MakeAjaxGetRequest("/AutoTransportType/GetAllAsSelectList", { selected: searchVM.TransportTypeID }, ".searh_res_filter #TransportTypeID", "select", null, Resource.AnyM, null, disabled);
    if (disabled) {
        disabled = false;
        if (searchVM.BodyTypeID != null && (searchVM.BodyTypeID.length == 1 && searchVM.BodyTypeID[0] != 0 || searchVM.BodyTypeID.length > 1))
            disabled = true;
        MakeAjaxGetRequest("/AutoBodyType/GetAsSelectListMultiple", { transportTypeID: searchVM.TransportTypeID, selected: searchVM.BodyTypeID }, ".searh_res_filter #BodyTypeID", "select", null, Resource.AnyM, true, disabled);
    }

    disabled = false;
    if (searchVM.MakeID != null && (searchVM.MakeID.length == 1 && searchVM.MakeID[0] != 0 || searchVM.MakeID.length > 1))
        disabled = true;
    MakeAjaxGetRequest("/AutoMake/GetAsSelectListMultiple", { selected: searchVM.MakeID }, ".searh_res_filter #MakeID", "select", null, Resource.AnyFem, true, disabled);
    if (disabled) {
        disabled = false;
        if (searchVM.ModelID != null && (searchVM.ModelID.length == 1 && searchVM.ModelID[0] != 0 || searchVM.ModelID.length > 1))
            disabled = true;
        MakeAjaxGetRequest("/AutoModel/GetAsSelectListWithParentIDMultiple", { makeID: searchVM.MakeID, selected: searchVM.ModelID }, ".searh_res_filter #ModelID", "select", null, Resource.AnyM, true, disabled, true, ".searh_res_filter #MakeID");
    }

    disabled = false;
    if (searchVM.YearOfIssue != null && (searchVM.YearOfIssue.length == 1 && searchVM.YearOfIssue[0] != 0 || searchVM.YearOfIssue.length > 1))
        disabled = true;
    MakeAjaxGetRequest("/Year/GetAllAsSelectListMultiple", { selected: searchVM.YearOfIssue }, ".searh_res_filter #YearOfIssue", "select", null, Resource.AnyM, true, disabled);

    //disabled = false;
    //if (searchVM.CurrencyID != null && searchVM.CurrencyID != 0)
    //    disabled = true;
    MakeAjaxGetRequest("/Currency/GetAllAsSelectList", { selected: searchVM.CurrencyID }, ".searh_res_filter #CurrencyID");

    disabled = false;
    if (searchVM.RegionID != null && (searchVM.RegionID.length == 1 && searchVM.RegionID[0] != 0 || searchVM.RegionID.length > 1))
        disabled = true;
    MakeAjaxGetRequest("/Region/GetAllAsSelectListMultiple", { selected: searchVM.RegionID }, ".searh_res_filter #RegionID", "select", null, Resource.AnyM, true, disabled);
    if (disabled) {
        disabled = false;
        if (searchVM.CityID != null && (searchVM.CityID.length == 1 && searchVM.CityID[0] != 0 || searchVM.CityID.length > 1))
            disabled = true;
        MakeAjaxGetRequest("/City/GetAsSelectListWithParentIDMultiple", { makeID: searchVM.RegionID, selected: searchVM.CityID }, ".searh_res_filter #CityID", "select", null, Resource.AnyM, true, disabled, true, ".searh_res_filter #RegionID");
    }

    disabled = false;
    if (searchVM.TransmissionTypeID != null && (searchVM.TransmissionTypeID.length == 1 && searchVM.TransmissionTypeID[0] != 0 || searchVM.TransmissionTypeID.length > 1))
        disabled = true;
    MakeAjaxGetRequest("/AutoTransmissionType/GetAllAsSelectListMultiple", { selected: searchVM.TransmissionTypeID }, ".searh_res_filter #TransmissionTypeID", "select", null, Resource.AnyFem, true, disabled);

    disabled = false;
    if (searchVM.DriveTypeID != null && (searchVM.DriveTypeID.length == 1 && searchVM.DriveTypeID[0] != 0 || searchVM.DriveTypeID.length > 1))
        disabled = true;
    MakeAjaxGetRequest("/AutoDriveType/GetAllAsSelectListMultiple", { selected: searchVM.DriveTypeID }, ".searh_res_filter #DriveTypeID", "select", null, Resource.AnyM, true, disabled);

    disabled = false;
    if (searchVM.FuelTypeID != null && (searchVM.FuelTypeID.length == 1 && searchVM.FuelTypeID[0] != 0 || searchVM.FuelTypeID.length > 1))
        disabled = true;
    MakeAjaxGetRequest("/AutoFuelType/GetAllAsSelectListMultiple", { selected: searchVM.FuelTypeID }, ".searh_res_filter #FuelTypeID", "select", null, Resource.AnyN, true, disabled);

    disabled = false;
    if (searchVM.TSRegistrationID != null && (searchVM.TSRegistrationID.length == 1 && searchVM.TSRegistrationID[0] != 0 || searchVM.TSRegistrationID.length > 1))
        disabled = true;
    MakeAjaxGetRequest("/AutoTSRegistration/GetAllAsSelectListMultiple", { selected: searchVM.TSRegistrationID }, ".searh_res_filter #TSRegistrationID", "select", null, Resource.AnyFem, true, disabled);

    disabled = false;
    if (searchVM.States != null && (searchVM.States.length == 1 && searchVM.States[0] != 0 || searchVM.States.length > 1))
        disabled = true;
    MakeAjaxGetRequest("/AutoState/GetAllAsSelectList", { selected: searchVM.States }, ".searh_res_filter #States", "select", null, Resource.AnyN, true, disabled);

    $('.w_search_sel').on('changed.bs.select', function (event, clickedIndex, newValue, oldValue) {
        var selID = $(this).attr("id");
        //console.log(clickedIndex);
        if (clickedIndex > 0) {
            var curVal = $(this).val();
            if (!Array.isArray(curVal)) {
                RedrawList();
                return;
            }

            var index = curVal.indexOf("0");
            if (index !== -1)
                curVal.splice(index, 1);

            if (selID == "YearOfIssue") {
                if (newValue == true) {
                    var arr = $(".searh_res_filter #YearOfIssue > option").toArray();

                    var topCheckedIndex = 0;
                    for (var i = clickedIndex - 1; i > 0; i--) {
                        if (curVal.includes("" + $(arr[i]).attr("value"))) {
                            topCheckedIndex = i;
                            break;
                        }
                    }
                    if (topCheckedIndex > 0) {
                        for (var i = clickedIndex - 1; i > topCheckedIndex; i--)
                            curVal.push("" + $(arr[i]).attr("value"));
                    }

                    var bottomCheckedIndex = arr.length;
                    for (var i = clickedIndex + 1; i < arr.length; i++) {
                        if (curVal.includes("" + $(arr[i]).attr("value"))) {
                            bottomCheckedIndex = i;
                            break;
                        }
                    }
                    if (bottomCheckedIndex < arr.length) {
                        for (var i = clickedIndex + 1; i < bottomCheckedIndex; i++)
                            curVal.push("" + $(arr[i]).attr("value"));
                    }
                }
            }
            //console.log(curVal);
            $(this).selectpicker('val', curVal);
        }
        else {
            $(this).selectpicker('val', ['0']);
        }

        if (selID == "MakeID") {
            var targetSelector = ".searh_res_filter #ModelID";
            //$(targetSelector).attr("disabled", "disabled");
            var data = { makeID: $(".searh_res_filter #MakeID").val(), selected: $(targetSelector).val() };
            if (data.makeID.length == 0 || data.makeID.length == 1 && data.makeID[0] == "0") {
                $(targetSelector).html("<option value='0' selected=selected>" + Resource.AnyFem + "</option>");
                $(targetSelector).selectpicker('refresh');
            }
            else
                MakeAjaxGetRequest("/AutoModel/GetAsSelectListWithParentIDMultiple", data, targetSelector, "select", null, Resource.AnyFem, true, null, true, ".searh_res_filter #MakeID");
        }
        else if (selID == "RegionID") {
            var targetSelector = ".searh_res_filter #CityID";
            //$(targetSelector).attr("disabled", "disabled");
            var data = { regionID: $(".searh_res_filter #RegionID").val(), selected: $(targetSelector).val() };
            if (data.regionID.length == 0 || data.regionID.length == 1 && data.regionID[0] == "0") {
                $(targetSelector).html("<option value='0' selected=selected>" + Resource.AnyM + "</option>");
                $(targetSelector).selectpicker('refresh');
            }
            else
                MakeAjaxGetRequest("/City/GetAsSelectListWithParentIDMultiple", data, targetSelector, "select", null, Resource.AnyM, true, null, true, ".searh_res_filter #RegionID");
        }

        //$(this).selectpicker('refresh');
        RedrawList();
    });

    $(".searh_res_filter input[type='text']").keyup(function () {
        RedrawList()
    });
    $(".searh_res_filter input[type='checkbox']").change(function () {
        //alert($(this).val());
        if ($(this).val() == "false")
            $(this).val("true");
        else
            $(this).val("false");
        RedrawList()
    });
    $(".searh_res_filter #CurrencyID").change(function () {
        RedrawList()
    });

    $(".searh_res_filter select#TransportTypeID").change(function () {
        var targetSelector = ".searh_res_filter #BodyTypeID";
        //$(targetSelector).attr("disabled", "disabled");
        var data = { transportTypeID: $(".searh_res_filter #TransportTypeID").val() };

        if (data.transportTypeID == "")
            $(targetSelector).html("");
        else
            MakeAjaxGetRequest("/AutoBodyType/GetAsSelectListMultiple", data, targetSelector, "select", null, Resource.AnyM, true);
    });

    $(function () {
        $(".view_line").click(function (e) {
            e.preventDefault();
            $('.view_grid').removeClass('active');
            $('.view_line').addClass('active');
            $('.searh_res_ads').removeClass('grid');
            $('.searh_res_ads').addClass('line');
        })
    });
    $(function () {
        $(".view_grid").click(function (e) {
            e.preventDefault();
            $(".view_line").removeClass('active');
            $('.view_grid').addClass('active');
            $('.searh_res_ads').addClass('grid');
            $('.searh_res_ads').removeClass('line');
        })
    });
});

$(document).ready(function () {
    $(".hide_cont_href").click(function () {
        if ($(".hide_cont").is(":hidden")) {

            $(".hide_cont").show("slow");
            $(".hide_cont_href").addClass('open_con');
        } else {

            $(".hide_cont").hide("slow");
            $(".hide_cont_href").removeClass('open_con');
        }
        return false;
    });
});

//datatable
$(document).ready(function () {
    //console.log(searchVM);
    table = $('#resultList').DataTable({
        searching: false,
        ordering: false,
        processing: true,
        serverSide: true,
        lengthChange: false,
        pagingType: "simple_numbers",
        language: {
            infoFiltered: " ",
            info: " ",
            processing: Resource.Loading + "...",
            zeroRecords: " ",
            infoEmpty: " ",
            paginate: {
                previous: " ",
                next: " ",
            }
        },
        ajax:
        {
            url: "/SearchAuto/GetAutosFromElastic",
            type: "POST",
            contentType: "application/json",
            data: function (d) {
                d.modelVM = searchVM;
                return JSON.stringify(d);
            },
            complete: function () {
                StartLoadingImagesAsync();
            }
        },
        lengthMenu: [[10, 50, 100], [10, 50, 100]],
        fnInitComplete: function (oSettings, json) {
            //console.log(json);
            $("#resultList_wrapper > div").first().remove();
        },
        fnDrawCallback: function (oSettings) {
            //console.log(oSettings.json.recordsFiltered);//do whatever with your custom response
            $("#recordsFiltered").text(oSettings.json.recordsFiltered);
        }
    });

    $('#resultList').on('draw.dt', function () {
        RestylePagination();
    });
});

function RestylePagination() {
    $("#resultList_paginate").parent().prepend("<div class='clearfix'></div>");
    $("ul.pagination").parent().parent().css("display", "inherit");

    $("#resultList").parent().parent().next().children(".col-sm-5").remove();
    $("#resultList").parent().parent().next().children(".col-sm-7").removeClass("col-sm-7").addClass("col-sm-12");
    $(".pagination").parent().addClass("pag_wrap");
    $(".pagination > .previous").addClass("pag_prev").children().text("");
    $(".pagination > .next").addClass("pag_next").children().text("");
    $("li[id *= ellipsis]").addClass("pag_more");

    if ($("ul.pagination").children().length <= 3)
        $("ul.pagination").parent().parent().css("display", "none");
}

function SetTopFilter(targetInput, value, targetDropdown, text, dontRedraw)//'#SortID', 0, '#sortDropdown', 'Обычная'
{
    $(targetInput).val(value);
    $(targetDropdown).html(text + "<span class='caret'></span>");

    if (targetInput == "#SortID")
        searchVM.SortID = value;
    else if (targetInput == "#PeriodID")
        searchVM.PeriodID = value;
    else if (targetInput == "#length")
        table.page.len(value);

    if (!dontRedraw)
        RedrawList();
}

function RedrawList() {
    //alert("1");
    searchVM.TransportTypeID = $(".searh_res_filter #TransportTypeID").val();
    searchVM.BodyTypeID = $(".searh_res_filter #BodyTypeID").val();
    searchVM.MakeID = $(".searh_res_filter #MakeID").val();
    searchVM.ModelID = $(".searh_res_filter #ModelID").val();

    searchVM.MakeAndModels = [];
    var selectedModels = searchVM.ModelID;
    for (var i = 0; i < selectedModels.length; i++) {
        var obj = {
            MakeID: $(".searh_res_filter #ModelID option[value='" + selectedModels[i] + "']").attr("parentid"),
            ModelID: selectedModels[i]
        };

        searchVM.MakeAndModels.push(obj);
    }

    searchVM.YearOfIssue = $(".searh_res_filter #YearOfIssue").val();
    searchVM.PriceFrom = $(".searh_res_filter #PriceFrom").val();
    searchVM.PriceTo = $(".searh_res_filter #PriceTo").val();
    searchVM.CurrencyID = $(".searh_res_filter #CurrencyID").val();
    searchVM.ProbegFrom = $(".searh_res_filter #ProbegFrom").val();
    searchVM.ProbegTo = $(".searh_res_filter #ProbegTo").val();
    searchVM.RegionID = $(".searh_res_filter #RegionID").val();
    searchVM.CityID = $(".searh_res_filter #CityID").val();

    searchVM.RegionAndCities = [];
    var selectedCities = searchVM.CityID;
    for (var i = 0; i < selectedCities.length; i++) {
        var obj = {
            RegionID: $(".searh_res_filter #CityID option[value='" + selectedCities[i] + "']").attr("parentid"),
            CityID: selectedCities[i]
        };

        searchVM.RegionAndCities.push(obj);
    }

    searchVM.TransmissionTypeID = $(".searh_res_filter #TransmissionTypeID").val();
    searchVM.DriveTypeID = $(".searh_res_filter #DriveTypeID").val();
    searchVM.FuelTypeID = $(".searh_res_filter #FuelTypeID").val();
    searchVM.TSRegistrationID = $(".searh_res_filter #TSRegistrationID").val();
    searchVM.States = $(".searh_res_filter #States").val();
    searchVM.WithPhotoOnly = $(".searh_res_filter #WithPhotoOnly").val();

    table.draw();
}

function ResetFilters() {
    //alert("asdff");
    if ($(".searh_res_filter #TransportTypeID").attr("disabled") == undefined)
        $(".searh_res_filter #TransportTypeID").selectpicker('val', ['0']);
    //$(".searh_res_filter #TransportTypeID").removeAttr("disabled");
    //$(".searh_res_filter #TransportTypeID").selectpicker('refresh');


    if ($(".searh_res_filter #BodyTypeID").attr("disabled") == undefined)
        $(".searh_res_filter #BodyTypeID").selectpicker('val', ['0']);
    if ($(".searh_res_filter #MakeID").attr("disabled") == undefined)
        $(".searh_res_filter #MakeID").selectpicker('val', ['0']);
    if ($(".searh_res_filter #ModelID").attr("disabled") == undefined)
        $(".searh_res_filter #ModelID").selectpicker('val', ['0']);
    if ($(".searh_res_filter #YearOfIssue").attr("disabled") == undefined)
        $(".searh_res_filter #YearOfIssue").selectpicker('val', ['0']);
    if ($(".searh_res_filter #PriceFrom").attr("disabled") == undefined)
        $(".searh_res_filter #PriceFrom").val("");
    if ($(".searh_res_filter #PriceTo").attr("disabled") == undefined)
        $(".searh_res_filter #PriceTo").val("");
    if ($(".searh_res_filter #CurrencyID").attr("disabled") == undefined)
        $(".searh_res_filter #CurrencyID").val("1");
    if ($(".searh_res_filter #ProbegFrom").attr("disabled") == undefined)
        $(".searh_res_filter #ProbegFrom").val("");
    if ($(".searh_res_filter #ProbegTo").attr("disabled") == undefined)
        $(".searh_res_filter #ProbegTo").val("");
    if ($(".searh_res_filter #RegionID").attr("disabled") == undefined)
        $(".searh_res_filter #RegionID").selectpicker('val', ['0']);
    if ($(".searh_res_filter #CityID").attr("disabled") == undefined)
        $(".searh_res_filter #CityID").selectpicker('val', ['0']);
    if ($(".searh_res_filter #TransmissionTypeID").attr("disabled") == undefined)
        $(".searh_res_filter #TransmissionTypeID").selectpicker('val', ['0']);
    if ($(".searh_res_filter #DriveTypeID").attr("disabled") == undefined)
        $(".searh_res_filter #DriveTypeID").selectpicker('val', ['0']);
    if ($(".searh_res_filter #FuelTypeID").attr("disabled") == undefined)
        $(".searh_res_filter #FuelTypeID").selectpicker('val', ['0']);
    if ($(".searh_res_filter #TSRegistrationID").attr("disabled") == undefined)
        $(".searh_res_filter #TSRegistrationID").selectpicker('val', ['0']);
    if ($(".searh_res_filter #States").attr("disabled") == undefined)
        $(".searh_res_filter #States").selectpicker('val', ['0']);
    if ($(".searh_res_filter #DriveTypeID").attr("disabled") == undefined)
        $(".searh_res_filter #DriveTypeID").selectpicker('val', ['0']);

    $(".searh_res_filter #WithPhotoOnly").prop('checked', false);
    $(".searh_res_filter #WithPhotoOnly").val(false);

    var dontRedraw = true;
    SetTopFilter('#SortID', 0, '#sortDropdown', Resource.SortUsual, dontRedraw)
    SetTopFilter('#PeriodID', 0, '#periodDropdown', Resource.All, dontRedraw)
    SetTopFilter('#length', 10, '#lengthDropdown', '10', dontRedraw)

    RedrawList();
}