

//using database
$(document).ready(function () {
  
    $('#tableEmployee').DataTable({

        'ajax': {
            'url':"https://localhost:44319/API/Employees",
            'dataSrc':'result'
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" }
        ],
        'columns': [
            {
                "data": "nik"
            },
            {
                "data": "",
                "render": function (data, type, row, meta) {
                    return row['firstName'] +" "+ row['lastName'];
                }
            },
            {
                "data": "",
                "render": function (data, type, row, meta) {
                    return "Rp." + row['salary'];
                }
            },
            {
                "data": "phone",
                "render": function (data, type, row, meta) {
                    if (data[0] == '0') {
                        return "+62" + data.slice(1);
                    }
                    else if (data.slice(0, 3) == '+62')
                    {
                        return data;
                    }
                    else if (data.slice(0, 2) == '62') {
                        return "+" + data;
                    }
                    else
                    {
                        return "+62" + data;
                    }
                }
            },
            {
                "data": "gender"
            }
        ]
    });

    var check = $("#inputForm").validate({
        // Specify validation rules
        rules: {
            nik: "required",
            firstName: "required",
            lastName: "required",
            phone: "required",
            salary: "required",
            gender: "required",
            birthDate: "required",
            degree: "required",
            gpa: "required",
            universiyId: "required",
            email: {
                required: true,
                email: true
            },
            password: {
                required: true,
                minlength: 5
            }
        },
        messages: {
            nik: "Please enter NIK",
            firstname: "Please enter your First Name",
            lastname: "Please enter your Last Name",
            phone: "Please enter your Phone Correctly",
            salary: "Please enter your Salary ",
            gender: "Please enter your Gender",
            degree: "Please provide your Degree",
            birthDate: "Please provide your Birth Date",
            gpa: "Please enter your Gpa",
            universiyId: "Please provide your university",
            password: {
                required: "Please provide a password",
                minlength: "Your password must be at least 5 characters long"
            },
            email: "Please enter a valid email address"
        }
    });

    //$('#btnsubmit').on('click', function () {
    //    $("#inputForm").valid();
    //})

    $('#btnsubmit').click(function () {
        var ini = $("#inputForm").valid();
        console.log(ini);
        if (ini === true) {
            Insert();
        }
        else
        {
            alertGagal();
        }
    });

    function Insert(obj) {

        var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
        //ini ngambil value dari tiap inputan di form nya
        obj.NIK = $("#nik").val();
        obj.FirstName = $("#firstName").val();
        obj.LastName = $("#lastName").val();
        obj.Phone = $("#phone").val();
        obj.salary = $("#salary").val();
        obj.Birthdate = $("#birthDate").val();
        obj.Email = $("#email").val();
        obj.Gender = $("#gender").val();
        obj.Password = $("#password").val();
        obj.Degree = $("#degree").val();
        obj.UniversityId = $("#universiyId").val();
        console.log(obj);
        $.ajax({
            url: "https://localhost:44319/API/Employees/Registration",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'},
            type: 'POST',
            data: JSON.stringify(obj)

            }).done((result) => {
                alertBerhasil();
                $('#tableEmployee').DataTable().ajax.reload();

            }).fail((error) => {
                alertGagal();
                console.log(error);
        })
    }

    function alertBerhasil() {
        Swal.fire(
            'Good job!',
            'Pendaftaran Berhasil!',
            'success'
         );
    }
    function alertGagal() {
        Swal.fire({
            icon: 'Error',
            title: 'Oops...',
            text: 'Pendaftaran Gagal!',
            footer: '<a href="">Why do I have this issue?</a>'
        });
    }
});





