$(document).ready(function () {
    MakeAjaxGetRequest("/AutoTransportType/GetAllAsSelectList", null, ".home_search_form #TransportTypeID");
    MakeAjaxGetRequest("/AutoMake/GetAsSelectList", null, ".home_search_form #MakeID");
    MakeAjaxGetRequest("/City/GetAsSelectListWithParentIDMultiple", null, ".home_search_form #RegionID");
    MakeAjaxGetRequest("/Year/GetAllAsSelectList", null, ".home_search_form #YearOfIssueFrom", undefined, undefined, Resource.From);
    MakeAjaxGetRequest("/Year/GetAllAsSelectList", null, ".home_search_form #YearOfIssueTo", undefined, undefined, Resource.To);
    MakeAjaxGetRequest("/Currency/GetAllAsSelectList", null, ".home_search_form #CurrencyID");

    $(".home_search_form select#TransportTypeID").change(function () {
        var targetSelector = ".home_search_form #BodyTypeID";
        $(targetSelector).attr("disabled", "disabled");
        var data = { transportTypeID: $(".home_search_form #TransportTypeID").val() };

        if (data.transportTypeID == "")
            $(targetSelector).html("");
        else
            MakeAjaxGetRequest("/AutoBodyType/GetAsSelectList", data, targetSelector);
    });

    $(".home_search_form select#MakeID").change(function () {
        var targetSelector = ".home_search_form #ModelID";
        var data = { makeID: $(".home_search_form #MakeID").val() };
        if (data.makeID.length == "")
            $(targetSelector).html("");
        else
            MakeAjaxGetRequest("/AutoModel/GetAsSelectList", data, targetSelector);
    });

    $(".home_search_form form").submit(function () {
        var html = "";
        html += "<input type='hidden' name='MakeAndModels[0].MakeID' value='" + $(".home_search_form #MakeID").val() + "'>";
        html += "<input type='hidden' name='MakeAndModels[0].ModelID' value='" + $(".home_search_form #ModelID").val() + "'>";
        $(".home_search_form form").append(html);

        html += "<input type='hidden' name='RegionAndCities[0].RegionID' value='" + $(".home_search_form #RegionID > option[value='" + $(".home_search_form #RegionID").val() + "']").attr("parentid") + "'>";
        html += "<input type='hidden' name='RegionAndCities[0].CityID' value='" + $(".home_search_form #RegionID").val() + "'>";
        $(".home_search_form form").append(html);

        return true;
    });
});