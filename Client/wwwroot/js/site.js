
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
        drawCallback: function ()
        {
            $('.buttonsToHide')[0].style.visibility = 'hidden'
            var hasRows = this.api().rows({ filter: 'applied' }).data().length > 0;
            if (hasRows) { document.getElementById("tableEmployee").style.visibility = "disable";}
        },
        'ajax': {
            'url':"/Employees/GetEmployeeAll",
            'dataSrc':''
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
        ],
        language: {
            paginate: {
                next: `<i class="fa fa-arrow-right">`,
                previous: `<i class="fa fa-arrow-left">`
            }
        }
    });

    $.validator.addMethod("strong_password",
        function (value, element)
    {
        let password = value;
        if (!(/^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%&*!])(.{8,20}$)/.test(password)))
        {
            return false;
        }
        return true;
    },

        function (value, element)
    {
        let password = $(element).val();
        var n = $(element).attr("name");
        if (!(/^(.{8,20}$)/.test(password))) {
            return "Password must between 8-20 characters long.";
        }
        else if (!(/^(?=.*[A-Z])/.test(password))) {
            return "Password must contain atleast one uppercase.";
        }
        else if (!(/^(?=.*[a-z])/.test(password))) {
            return  "Password must contain atleast one lowercase.";
        }
        else if (!(/^(?=.*[0-9])/.test(password))) {
            return "Password must contain atleast one digit.";
        }
        else if (!(/^(?=.*[@#$%&*!])/.test(password))) {
            return "Password must contain atleast one special character @#$%&*!.";
        }
        return false;
    });

    $("#inputForm").validate({

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
                strong_password: true
            }
        },
        messages: {
            password: {
                required: "Please provide a password",
                strong_password: "asu masukin password aja gabisa"
            }       
        },
        errorPlacement: function (error, element) {
        },
        highlight: function (element) {
            $(element).closest('.form-control').addClass('is-invalid');
            $(element).closest('.form-group').addClass('is-invalid');      

            var n = $(element).attr("name");
            if (n === "nik") { $(element).attr("placeholder", "Please enter your NIK"); }
            if (n === "firstName") { $(element).attr("placeholder", "Please enter your First Name"); }
            if (n === "lastName") { $(element).attr("placeholder", "Please enter your Last Name"); }
            if (n === "phone") { $(element).attr("placeholder", "Please enter your Phone Correctly"); }
            if (n === "salary") { $(element).attr("placeholder", "Please enter your Salary"); }
            if (n === "gpa") { $(element).attr("placeholder", "Please enter your Gpa"); }
            if (n === "degree") { $(element).attr("placeholder", "Please provide your Degree"); }
          /*  if (n === "password") { $(element).attr("placeholder", "Please provide a password"); }*/
            if (n === "email") { $(element).attr("placeholder", "Please enter a valid email address"); }
         },
        unhighlight: function (element) {
            $(element).closest('.form-control').removeClass('is-invalid');
            $(element).closest('.form-group').removeClass('is-invalid');
        
        }

    }); 
});

function downloadExcel()
{
    $('#tableEmployee').DataTable().buttons().trigger();
}

function Validate() {
    var ini = $("#inputForm").valid();
    console.log(ini);

    if (ini === true) {
        cekEdit();
    }
    else {
        alertGagal(0);
    }
};

function cekEdit() {
    var cek = $("#edit").val();
    if (cek === "true"  ) {
        Update();
    }
    else { Insert(); }
        

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
        url: "/Employees/Register",
        //headers: {
        //    'Accept': 'application/json',
        //    'Content-Type': 'application/json'
        //},
        type: 'POST',
        data: {entity: obj},
        dataType: 'JSON'
    }).done((result) => {
        console.log(result);
        if (result === 200) {
            alertBerhasil(1);
            $('#tableEmployee').DataTable().ajax.reload();
            $('#formModal').modal('toggle');
        }
        else {
            console.log(result);
            alertGagal(1);
        }
    }).fail((error) => {
        console.log(error);
        alertGagal(1);
    });
};

function normalization()
{
    $("#exampleModalLongTitle").empty();
    $("#exampleModalLongTitle").append("Registration");
    $("#nik").attr("disabled", false);

    $("#edit").val("");
    $("#nik").val("");
    $("#firstName").val("");
    $("#lastName").val("");
    $("#phone").val("");
    $("#salary").val("");
    $("#email").val("");  
    $("#birthDate").val("");
    $("#gender").val("");
    $("#password").val("");
    $("#universiyId").val("");
    $("#degree").val("");
    $("#gpa").val("");
}

function Edit(nik) {
    $("#exampleModalLongTitle").empty();
    $("#exampleModalLongTitle").append("Edit");  
    $("#nik").attr("disabled", true);
    $("#edit").val(true);
   

    $.ajax({
        url: `/employees/getemployee/${nik}`,
        //headers: {
        //    'Accept': 'application/json',
        //    'Content-Type': 'application/json'
        //},
        type: 'GET',
        success: function (data) {
            console.log(data);
            var gender = pilihOption(`${data.gender}`);
            console.log(gender);
            $("#nik").val(`${data.nik}`);
            $("#firstName").val(`${data.firstName}`);
            $("#lastName").val(`${data.lastName}`);
            $("#phone").val(`${data.phone}`);
            $("#salary").val(`${data.salary}`);
            $("#email").val(`${data.email}`);
            $("#gender").val(gender);

        }
    });
    $.ajax({
        url: `/employees/getprofile/${nik}`,
        //headers: {
        //    'Accept': 'application/json',
        //    'Content-Type': 'application/json'
        //},
        type: 'GET',
        success: function (data) {
            var universityId = pilihOption(`${data.name}`);
            console.log(universityId);
            $("#universiyId").val(universityId);
            $("#degree").val(`${data.degree}`);
            $("#gpa").val(`${data.gpa}`);           
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

function Login() {
    var obj = new Object();
    obj.email = $('#namelogin').val();
    obj.password = $('#passwordlogin').val();
    console.log("==cek data==");
    console.log(obj);
    $.ajax({
        url: "/Logins/SignIn",
        type: 'POST',
        data: { entity: obj },
        dataType: 'JSON'
    }).done((result) => {
        console.log('==data result==');
        console.log(result);
        alertBerhasil(2);

    }).fail((error) => {
        console.log('==data error==');
        console.log(error);

    });
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
        url: "/Employees/Update",
        type: 'PUT',
        data: { entity: obj },
        dataType: 'JSON'
    }).done((result) => {
        console.log(result);
        alertBerhasil(2);
        $('#tableEmployee').DataTable().ajax.reload();
        obj.Degree = $("#edit").val("");
    }).fail((error) => {
        console.log(error);
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
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            hapus(nik);
        }
    })

}

function hapus(nik)
{
    var obj = new Object();
    obj.nik = nik;
    $.ajax({
        url: `/Employees/Delete/${nik}`,
        type: 'DELETE',
    }).done((result) => {
        alertBerhasil(3);
        $('#tableEmployee').DataTable().ajax.reload();

    }).fail((error) => {
        alertGagal(3);
    });
}



const seriesgender = [];
const labelgender = [];

var url = '/Employees/ChartGender';
$.getJSON(url, function (response) {
    $.each(response, function (index, value) {
        seriesgender.push(value.count);
        labelgender.push(value.gender);
    })
    
});

// ApexChart
var chartgender = {
    chart: {
        height: 350,
        type: 'donut'
    },
    series: seriesgender,
    labels: labelgender,
    legend: {
        position: 'bottom'
    }
}


var chart = new ApexCharts(document.querySelector("#chart2"), chartgender);
chart.render();



const dataSeries = [];
const dataLabel = [];

var dataProp = $.ajax({
    type: 'GET',
    url: '/Employees/ChartDegree',
    success: function (data) {
        $.each(data, function (index, value) {
            dataSeries.push(value.count);
            dataLabel.push(value.degree);
        })
    }
})

// ApexChart
var chartDegree = {
    chart: {
        height: 350,
        type: 'pie'
    },
    series: dataSeries,
    labels: dataLabel,
    legend: {
        position: 'bottom'
    }
}

var x = new ApexCharts(document.querySelector("#chart1"), chartDegree);
x.render();


const seriesSalary = [];
const labelsalary = [];

var dataProp = $.ajax({
    type: 'GET',
    url: '/Employees/ChartSalary',
    success: function (data) {
        $.each(data, function (index, value) {
            seriesSalary.push(value.count);
            labelsalary.push(value.range);
        })
    }
})

var chartSalary = {
    chart: {
        height: 350,
        type: 'pie'
    },
    series: seriesSalary,
    labels: labelsalary,
    legend: {
        position: 'bottom'
    }
}

var x = new ApexCharts(document.querySelector("#chart3"), chartSalary);
x.render();

