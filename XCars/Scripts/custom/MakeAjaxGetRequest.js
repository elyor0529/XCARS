function MakeAjaxGetRequest(url, data, targetSelector, type, name, emptyOptionStr, multiple, disabled, showOptGroup, parentSelector) {
    $.ajax({
        type: "GET",
        url: url,
        data: data,
        async: true,
        traditional: true,
        contentType: "application/json; charset=utf-8",
        //dataType: "json",
        success: function (data) {
            //console.log(data);
            var html = "";
            if (type == "checkbox")
                html = BuildCheckboxList(data, targetSelector, name);
            //else if (type == "checkboxForStatesInSearchForm")
            //    html = BuildCheckboxListOptionsForStatesInSearchForm(data, targetSelector, name);
            else if (type == "checkboxForOthersInSearchForm")
                html = BuildCheckboxListOptionsForOthersInSearchForm(data, targetSelector, name);
            else
                html = BuildDropDownListOptions(data, targetSelector, emptyOptionStr, multiple, showOptGroup, parentSelector);
            $(targetSelector).html(html).removeAttr("disabled");
            if (multiple)
                $(targetSelector).selectpicker('refresh');
            if (disabled == true)
            {
                $(targetSelector).attr("disabled", true);
                $(targetSelector).selectpicker('refresh');
            }
        },
        error: function (data) {
            //alert("Error: " + data);
        }
    });
}

function BuildCheckboxList(options, targetSelector, name) {
    var html = "";
    for (var i = 0; i < options.length; i++) {
        html += "<dd>";
        var checkedAttr = "";
        if (options[i].Selected == true)
            checkedAttr = "checked=checked";
        html += "<input type='checkbox' id='" + name + "_" + options[i].Value + "' name='" + name + "' " + checkedAttr + " value='" + options[i].Value + "'>";
        html += "<label for='" + name + "_" + options[i].Value + "'>" + options[i].Text + "</label>";
        //html += "<li><label><input type='checkbox' name='" + name + "' " + checkedAttr + " value='" + options[i].Value + "'> " + options[i].Text + "</label></li>";
        html += "</dd>";
    }

    return html;
}

function BuildCheckboxListOptionsForOthersInSearchForm(options, targetSelector, name) {
    var html = "<dl>";
    for (var i = 0; i < options.length; i++) {
        html += "<dd>";
        var checkedAttr = "";
        if (options[i].Selected == true)
            checkedAttr = "checked=checked";
        html += "<input type='checkbox' id='" + name + "_" + options[i].Value + "' name='" + name + "' " + checkedAttr + " value='" + options[i].Value + "'>";
        html += " <label for='" + name + "_" + options[i].Value + "'>" + options[i].Text + "</label>";
        html += "</dd>";
    }
    html += "</dl>";
    html += "<div class='clearfix'></div>";
    return html;
}

function BuildDropDownListOptions(options, targetSelector, emptyOptionStr, multiple, showOptGroup, parentSelector) {
    //console.log("targetSelector: " + targetSelector);
    var selectedOptionExists = false;
    var optionsStr = "";
    if (Array.isArray(options) && options.length > 0)
    {
        //console.log("options:");
        //console.log(options);
        if (showOptGroup && options[0].ParentID != null)
            optionsStr += "<optgroup label='Condiments'>";
    }
        
    var prevParentID = 0;
    for (var i = 0; i < options.length; i++) {
        if (showOptGroup && options[i].ParentID != null && options[i].ParentID != prevParentID)
        {
            optionsStr += "</optgroup>";
            optionsStr += "<optgroup label='" + $(parentSelector + " > option[value='" + options[i].ParentID + "']").text() + "'>";
            prevParentID = options[i].ParentID;
        }

        var selected = "";
        if (options[i].Selected == true)
        {
            selected = "selected=selected";
            selectedOptionExists = true;
        }
        var parent = "";
        if (options[i].ParentID != null)
            parent = "parentid=" + options[i].ParentID;
        optionsStr += "<option value='" + options[i].Value + "' " + selected + " " + parent + ">" + options[i].Text + "</option>";
    }
    if (showOptGroup && options[0].ParentID != null)
        optionsStr += "</optgroup>";

    var html = "";

    var selectStr = "";
    if (emptyOptionStr != undefined && emptyOptionStr != null)
        selectStr = emptyOptionStr;
    var defaultOptionValue = "";
    if (selectStr != "")
        defaultOptionValue = "0";
    var defaultOptionIsSelected = "";
    if (!selectedOptionExists && selectStr != "")
        defaultOptionIsSelected = "selected=selected";
    var defaultOption = "<option value='" + defaultOptionValue + "' " + defaultOptionIsSelected + ">" + selectStr + "</option>";
    if (targetSelector == "#CurrencyID" || targetSelector == ".searh_res_filter #CurrencyID")
        defaultOption = "";
    html += defaultOption;

    html += optionsStr;

    return html;
}