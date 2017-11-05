
$(document).ready(function () {
    $(function () {

        $("#createnewproject").click(function () {
            $("#addprojectdialog").modal("show");

            $("#addnew").click(function () {

                var projectname = $("#addproject_name").val();
                var esimies = $("#addproject_esimies").val();
                var avattu = $("#addproject_avattu").val();


                $.ajax({
                    type: 'POST',
                    url: '/projects/addproject',
                    data: JSON.stringify({ projectname: projectname, leader: esimies, opened: avattu }),
                    contentType: 'application/json',
                    success: function (status) {
                        $("#addprojectdialog").modal("hide");
                    },
                    error: function (err) {
                        alert("Virhe tapahtui" + err);
                    }

                });

            });
        });

    });
});



