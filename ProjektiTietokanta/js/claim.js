function claiminfo() {

    $.getJSON("/claiminfo/getallemployees", null, function (json) {
        var jsonData = JSON.parse(json);

        html = "";

        if (jsonData.length > 0) {
            for (var i = 0; i < jsonData.length; i++) {
                html += "<tr>" +
                            "<td>" + jsonData[i].Employee + "</td>" +
                    "<td>" + jsonData[i].ProjectName + "</td>" +
                    "<td>" + jsonData[i].Hours + "</td>" +
                        "</tr>";
            }
            

        $("#tablehours tbody").html(html);
        
    });
};

$(function () {
    claiminfo();
});