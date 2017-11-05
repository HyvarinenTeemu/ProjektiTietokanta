$(function() {
    $.getJSON("/home/getprojects", null, function (json) {

        var jsonData = JSON.parse(json);

        html = "";

        for (var i = 0; i < jsonData.length; i++) {
            html += "<tr>" +
                        "<td>" + jsonData[i].ProjectName + "</td>" +
                        "<td>" + jsonData[i].ProjectOpened + "</td>" +
                        "<td>" + jsonData[i].ProjectLeader + "</td>" +
                     "</tr>";
        }

        $("#allProjects tbody").html(html);
    });
})