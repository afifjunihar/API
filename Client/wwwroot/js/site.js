
//using database
$(document).ready(function () {  
    $('#tableEmployee').DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                name: 'excel',
                title: 'Employee',
                sheetName: 'Employee',
                text: '',
                className: 'buttonsToHide fa fa-download btn-default',
                filename: 'Data',
                autoFilter: true,
                exportOptions: {
                    columns: [0, 1, 2, 3, 4]
                }
            }
        ],
        //export option => untuk memilih yang mau di download apa aja
        'ajax': {
            'url':"https://localhost:44319/API/Employees",
            'dataSrc':'result'
        },
        'columnDefs': [
            { "className": "dt-center", "targets": "_all", "orderable": false }
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
            },
            {
                "data": "",
                "render": function (data, type, row, meta) {
                    return `<button type="button" class="btn btn-warning" onclick="Edit('${row['nik']}');" data-toggle="modal" data-target="#formModal"><i class='fas fa-edit'></i></button>
<button type="button" class="btn btn-danger" onclick="hapusPegawai('${row['nik']}');"><i class="fas fa-trash"></i></button>`

                }
            }
        ]
    });

    table.buttons('.buttonsToHide').nodes().addClass('hidden');

    $("#inputForm").validate({
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
        errorPlacement: function (error, element) {
        },
        highlight: function (element) {
            $(element).closest('.form-control').addClass('is-invalid');
            $(element).closest('.form-group').addClass('is-invalid');

            $(element).closest('#group').addClass('invalid-feedback');
            var n = $(element).attr("name");
            if (n === "nik") { $(element).attr("placeholder", "Please enter your NIK"); }
            if (n === "firstName") { $(element).attr("placeholder", "Please enter your First Name"); }
            if (n === "lastName") { $(element).attr("placeholder", "Please enter your Last Name"); }
            if (n === "phone") { $(element).attr("placeholder", "Please enter your Phone Correctly"); }
            if (n === "salary") { $(element).attr("placeholder", "Please enter your Salary"); }
            if (n === "gpa") { $(element).attr("placeholder", "Please enter your Gpa"); }
            if (n === "degree") { $(element).attr("placeholder", "Please provide your Degree"); }
            if (n === "password") { $(element).attr("placeholder", "Please provide a password"); }
            if (n === "email") { $(element).attr("placeholder", "Please enter a valid email address"); }
         },
        unhighlight: function (element) {
            $(element).closest('.form-control').removeClass('is-invalid');
            $(element).closest('.form-group').removeClass('is-invalid');
            $(element).closest('#group').removeClass('invalid-feedback');
        }
    }); 
});

function Validate() {
    var ini = $("#inputForm").valid();
    console.log(ini);

    if (ini === true) {
        get();
    }
    else {
        alertGagal(0);
    }
};

function get()
{
    var NIK = $("#nik").val();
    $.ajax({
        url: `https://localhost:44319/API/Employees/${NIK}`,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: 'GET'
    }).done((result) => {
        pilihUpdate(true);
    }).fail((error) => {
        pilihUpdate(false);
    });
}

function pilihUpdate(bool) {
    if (bool == true) {
        Update();
    }
    else {
        Insert();
    }
}
function Insert() {
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
    obj.Gpa = $("#gpa").val();
    obj.UniversityId = $("#universiyId").val();
    console.log(obj);
    $.ajax({
        url: "https://localhost:44319/API/Employees/Registration",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: 'POST',
        data: JSON.stringify(obj)

    }).done((result) => {
        alertBerhasil(1);
        $('#tableEmployee').DataTable().ajax.reload();

    }).fail((error) => {
        alertGagal(1);
    });
};

function Edit(nik) {      
    $.ajax({
        url: `https://localhost:44319/API/Employees/${nik}`,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: 'GET',
        success: function (data) {
            var gender = pilihOption(`${data.result.gender}`);
            console.log(gender);
            $("#nik").val(`${data.result.nik}`);
            $("#firstName").val(`${data.result.firstName}`);
            $("#lastName").val(`${data.result.lastName}`);
            $("#phone").val(`${data.result.phone}`);
            $("#salary").val(`${data.result.salary}`);
            $("#email").val(`${data.result.email}`);
            $("#gender").val(gender);
        }
    });
    $.ajax({
        url: `https://localhost:44319/API/Employees/Registration/Profile/${nik}`,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: 'GET',
        success: function (data) {
            var universityId = pilihOption(`${data.result.name}`);
            console.log(universityId);
            $("#universiyId").val(universityId);
            $("#degree").val(`${data.result.degree}`);
            $("#gpa").val(`${data.result.gpa}`);
           
        }          
    });
}

function View(nik) { }

function pilihOption(option) {
    if (option === "Male") {
        return 0;
    }
    else if (option === "Female") {
        return 1;
    }
    else if (option === "Universitas Indonesia")
    {
        return 1005;
    }
    else if (option === "Universitas Andalas") {
        return 1006;
    }
    else if (option === "Universitas Trisakti") {
        return 1007;
    }
    else if (option === "Universitas Gajah Mada") {
        return 1008;
    }
}

function Update() {
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
    obj.Gpa = $("#gpa").val();
    obj.UniversityId = $("#universiyId").val();
    console.log(obj);

    $.ajax({
        url: "https://localhost:44319/API/Employees/Update",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: 'PUT',
        data: JSON.stringify(obj)
    }).done((result) => {
        alertBerhasil(2);
        $('#tableEmployee').DataTable().ajax.reload();

    }).fail((error) => {
        alertGagal(2);
    });
};

function alertBerhasil(pilih) {
    if (pilih === 1) {
        Swal.fire(
            'Good job!',
            'Pendaftaran Berhasil!',
            'success'
        );
    }
    else if (pilih === 2) {
        Swal.fire(
            'Good job!',
            'Data Berhasil Diubah! ',
            'success'
        );
    }
    else if (pilih === 3)
    {
        Swal.fire(
            'Good job!',
            'Data Berhasil di Hapus! ',
            'success'
        );
    }
};

function alertGagal(pilih) {
    if (pilih === 1) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Pendaftaran Gagal!',
            footer: '<a href="">Why do I have this issue?</a>'
        });
    }
    else if (pilih === 2) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Data Gagal Diubah!',
            footer: '<a href="">Why do I have this issue?</a>'
        });
    }
    else if (pilih === 3) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Data Gagal DiHapus!',
            footer: '<a href="">Why do I have this issue?</a>'
        });
    }
    else  {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Harap Masukan Seluruh Data',
            footer: '<a href="">Why do I have this issue?</a>'
        });
    }
};

function hapusPegawai(nik)
{
    var obj = new Object();
    obj.nik = nik;
    $.ajax({
        url: `https://localhost:44319/API/Employees/Delete/${nik}`,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: 'DELETE',
        data: JSON.stringify(obj)
    }).done((result) => {
        alertBerhasil(3);
        $('#tableEmployee').DataTable().ajax.reload();

    }).fail((error) => {
        alertGagal(3);
    });
}


