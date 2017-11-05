function modifyDeleteProject() {

    $.getJSON("/home/getprojects", null, function (json) {
        var jsonData = JSON.parse(json);

        html = "";

        if (jsonData.length > 0) {
            for (var i = 0; i < jsonData.length; i++) {
                html += "<tr>" +
                            "<td><a href='#'><span class='glyphicon glyphicon-pencil'></span></a><a href='#'><span class='glyphicon glyphicon-remove'></span></a><a href='#'><span class='glyphicon glyphicon-zoom-in'></span></a><a href='#'><span class='glyphicon glyphicon-hourglass'></span></a></td>" +
                            "<td>" + jsonData[i].ProjectName + "</td>" +
                            "<td>" + jsonData[i].ProjectOpened + "</td>" +
                            "<td>" + jsonData[i].ProjectLeader + "</td>" +
                        "</tr>";
            }

        //if no projects exist then input h2 field
        } else {
            html += "<h2>Ei projekteja käynnissä</h2>"
        }

        $("#tableprojects tbody").html(html);


        //remove project
        $('.glyphicon-remove').click(function () {
            var removebyval = $(this).parent().parent().next().text();

            $.ajax({
                type: 'POST',
                url: '/projects/removeproject',
                data: JSON.stringify({ removestring: removebyval }),
                contentType: 'application/json',
                success: function (status) {
                    modifyDeleteProject();
                },
                error: function (err) {
                    alert("virhe tapahtui poistamisessa" + err);
                }

            });
        });
        

        //claim hours for each employee for project
        $('.glyphicon-zoom-in').click(function () {
            var getValFromDatabase = $(this).parent().parent().next().text();
            var $select = $('#claimhours_employee');

            $.getJSON("/projects/claimhours/", { getval: getValFromDatabase }, function (json) {
                var project1 = JSON.parse(json);

                var projectname = "";
                var listItems = '<option selected="selected">-- valitse -- </option>';

                for (var i = 0; i < project1.length; i++) {
                    listItems += "<option value'" + project1[i].Etunimi + "'>" + project1[i].Etunimi + "</option>";
                    projectname = project1[i].Nimi;
                }

                $("#claimhours_employee").html(listItems);
                $("#claimhours_name").val(projectname);

                $("#claimhours").modal("show");

                $("#claim").click(function () {
                    var Projecti = $("#claimhours_name").val();
                    var Employee = $("#claimhours_employee").val();
                    var hours = $("#claimhours_hours").val();

                    $.ajax({
                        type: 'POST',
                        url: '/projects/claimhourspost',
                        data: JSON.stringify({ projectname: Projecti, employee: Employee, hours: hours }),
                        contentType: 'application/json',
                        success: function (status) {
                            $("#claimhours").modal("hide");
                        },
                        error: function (err) {
                            alert("Virhe tapahtui" + err);
                        }

                    });
                });
            });
        });

        //modify project and who is working on project
        $('.glyphicon-pencil').click(function () {
            var getValFromDatabase = $(this).parent().parent().next().text();

            $.getJSON("/projects/getprojectvalue/", { getval: getValFromDatabase }, function (json) {
                var project1 = JSON.parse(json);

                $("#addproject_name").val(project1.Nimi);
                $("#addproject_esimies").val(project1.Esimies);

                $("#modifyproject").modal("show");

                $("#modify").click(function () {

                    var projectname = $("#addproject_name").val();
                    var esimies = $("#addproject_esimies").val();
                    var employee = $("#addproject_employee").val();

                    $.ajax({
                        type: 'POST',
                        url: '/projects/modifypost',
                        data: JSON.stringify({ projectname: projectname, leader: esimies, employee: employee }),
                        contentType: 'application/json',
                        success: function (status) {
                            $("#modifyproject").modal("hide");
                        },
                        error: function (err) {
                            alert("Virhe tapahtui" + err);
                        }

                    });

                });
            });
            
        });
                
    });
}

$(function () {
    modifyDeleteProject();
});