////// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
////// for details on configuring this project to bundle and minify static web assets.

////// Write your JavaScript code.
////var list = document.getElementsByTagName("li");
////var ul = document.getElementsByTagName("ul");
////let createList = document.createElement("list");

////var list1Clicked = 0;
////function GantiList1() {
////    if (list1Clicked != 0) {
////        list[0].innerHTML = 'List 1 ini di klik ' + list1Clicked + ' kali';
////        list1Clicked = list1Clicked + 1;
////    }
////    else {
////        list[0].innerHTML = 'List 1 ini di klik';
////        list1Clicked = list1Clicked + 1;
////    }  
////}
////list[2].addEventListener("click", function () { ul[0].append('List Baru', createList) });

//$.ajax({
//    url: "https://pokeapi.co/api/v2/pokemon/",
//    success: function (result) {
//        console.log(result.results);
//        var listpokemon = "";
//        $.each(result.results, function(key, val) {
//            listpokemon += `<tr>
//                                <td>${key}</td>
//                                <td>${val.name}</td>
//                                <td><button class="btn btn-primary" onclick="alert('${val.url}')">Details </button></td>
//                            </tr>`;
//        });
//        $(`#listpoke`).html(listpokemon)
//    }
//})

//function alertPoke(url) {
//    alert(url);
//}


$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon",
    success: function (result) {
        console.log(result.results);
        var listpokemon = "";
        $.each(result.results, function(key, val) {
            listpokemon += `<tr>
                                <td class="align-middle">${key+1}</td>
                                <td class="align-middle text-capitalize">${val.name}</td>
                                <td><button type="button" class="btn btn-primary" onclick="ModalContent('${val.url}')" data-toggle="modal" data-target="#modalPoke">
                                            Details
                                        </button></td>
                            </tr>`;
        });
        $(`#listpoke`).html(listpokemon)
    }
})

function ModalContent(url) {
    console.log(url);
    $.ajax({
        url: url,
        success: function (result) {
            var pokeDetails = ""
            pokeDetails += `<img class="rounded mx-auto d-block" src="${result.sprites.other.dream_world.front_default}">
                            <h2 class="text-center text-capitalize">${result.name}</h2>
                            <div class="text-center"><span class="font-weight-bold mb-4">Type : </span>`;
            var i = 0;
            $.each(result.types, function (key, val) {
                if (result.types[i].type.name == 'water') {
                    pokeDetails += `<span class="badge badge-primary">${result.types[i].type.name}</span>`;
                }
                else if (result.types[i].type.name == 'fire') {
                    pokeDetails += `<span class="badge badge-danger">${result.types[i].type.name}</span>`;
                }
                else if (result.types[i].type.name == 'grass') {
                    pokeDetails += `<span class="badge badge-success">${result.types[i].type.name}</span>`;
                }
                else if (result.types[i].type.name == 'ground') {
                    pokeDetails += `<span class="badge badge-dark">${result.types[i].type.name}</span>`;
                }
                else if (result.types[i].type.name == 'rock') {
                    pokeDetails += `<span class="badge badge-secondary">${result.types[i].type.name}</span>`;
                }
                else if (result.types[i].type.name == 'steel') {
                    pokeDetails += `<span class="badge badge-secondary">${result.types[i].type.name}</span>`;
                }
                else if (result.types[i].type.name == 'ice') {
                    pokeDetails += `<span class="badge badge-primary">${result.types[i].type.name}</span>`;
                }
                else if (result.types[i].type.name == 'electric') {
                    pokeDetails += `<span class="badge badge-warning">${result.types[i].type.name}</span>`;
                }
                else if (result.types[i].type.name == 'dragon') {
                    pokeDetails += `<span class="badge badge-success">${result.types[i].type.name}</span>`;
                }
                else if (result.types[i].type.name == 'ghost') {
                    pokeDetails += `<span class="badge badge-light">${result.types[i].type.name}</span>`;
                }
                else if (result.types[i].type.name == 'physic') {
                    pokeDetails += `<span class="badge badge-danger">${result.types[i].type.name}</span>`;
                }
                else if (result.types[i].type.name == 'normal') {
                    pokeDetails += `<span class="badge badge-secondary">${result.types[i].type.name}</span>`;
                }
                else if (result.types[i].type.name == 'fighting') {
                    pokeDetails += `<span class="badge badge-danger">${result.types[i].type.name}</span>`;
                }
                else if (result.types[i].type.name == 'poison') {
                    pokeDetails += `<span class="badge badge-danger">${result.types[i].type.name}</span>`;
                }
                else if (result.types[i].type.name == 'bug') {
                    pokeDetails += `<span class="badge badge-success">${result.types[i].type.name}</span>`;
                }
                else if (result.types[i].type.name == "flying") {
                    pokeDetails += `<span class="badge badge-info">${result.types[i].type.name}</span>`;
                }
                else{
                    pokeDetails += `<span class="badge badge-light">${result.types[i].type.name}</span>`;
                }
                i += 1;
            });
            pokeDetails += `</div>`;
            pokeDetails += `<table class="table table-borderless border-top">
                            <tbody><tr><td class="font-weight-bold">Abilities :</td>`;
            $.each(result.abilities, function (key, val) {
                pokeDetails += `<td>${val.ability.name}</td>
                                </tr><tr><td> </td>`;
            });
            pokeDetails += `</tr><tr class="border-top"><td class="font-weight-bold">Moves :</td>`;
            for (var i = 0; i < 3; i++) {
                pokeDetails += `<td>${result.moves[i].move.name}</td>
                                </tr><tr><td> </td>`
            }
            pokeDetails += `<tr></t`
            pokeDetails += `</tbody></table>`;
            $(`#pokeDetails`).html(pokeDetails)
        }
    })
}
//using datatables

// Create our number formatter.
var formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'IDR',
  
    // These options are needed to round to whole numbers if that's what you want.
    //minimumFractionDigits: 0, // (this suffices for whole numbers, but will print 2500.10 as $2,500.1)
    //maximumFractionDigits: 0, // (causes 2500.99 to be printed as $2,501)
  });
  
$(document).ready(function () {
    $("#employeeData").DataTable({
        'responsive': true,
        'ajax': {
            'url': "https://localhost:44393/API/Employees",
            'dataSrc': ''
        },
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                name: 'excel',
                title: 'Employee',
                sheetName: 'Employee',
                text: '',
                className: 'buttonHide fa fa-download btn-default',
                filename: 'Data',
                autoFilter: true,
                exportOptions: {
                    columns: [0, 1, 2, 3]
                }
            }
        ],
        'columns': [
            {
                "data": "nik"
            },
            {
                "data": "",
                "render": function (data, type, row, meta) {
                    return row['firstName'] +' '+ row['lastName'];
                }
            },
            {
                "data": "gender"
            },
            {
                "data": "",
                "render": function (data, type, row, meta) {
                    if (row['phone'].substr(0,1) != 0) {
                        return '(+62)' + row['phone'];
                    }
                    else { return '(+62)' +row['phone'].substr(1);}
                   /* return '(+62) ' + row['phone'];*/
                }
            },
            {
                "data": "",
                "render": function(data,type, row, meta) {
                    return formatter.format(row['salary'])
                }
            },
            {
                "data": "",
                "render": function (data, type, row, meta) {
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalRegister" onclick="prefillEmployee('${row['nik']}')" data-toggle="tooltip" data-placement="top" title="Hapus data karyawan dari database"><i class="fa fa-pencil-square-o"></i></button>
                            <button type = "button" class="btn btn-circle btn-danger" onclick = "deleteEmployee('${row['nik']}')" data-toggle="tooltip" data-placement="top" title="Ubah data karyawan"> <i class="fas fa-trash"></i></button >`;
                },
                "orderable": false
            }
        ]
    });

    var check = $("#register").validate({
        // Specify validation rules
        rules: {
            nik: "required",
            firstName: "required",
            lastName: "required",
            phone: "required",
            salary: "required",
            genderOptions: "required",
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
            genderOptions: "Please enter your Gender",
            degree: "Please provide your Degree",
            birthDate: "Please provide your Birth Date",
            gpa: "Please enter your Gpa",
            universiyId: "Please provide your university",
            password: {
                required: "Please provide a password",
                minlength: "Your password must be at least 5 characters long"
            },
            email: "Please enter a valid email address"
        },
        errorPlacement: function (error, element) {
            if (element.attr("name") == "genderOptions") {
                error.insertAfter("#gender");
            } else if (element.attr("name") == "phone") {
                error.insertAfter("#phone");
            } else {
                error.insertAfter(element);
            }
        },
        highlight: function (element) {
            $(element).closest('.form-control').addClass('is-invalid');
            $(element).closest('.form-group').addClass('is-invalid');
        },
        unhighlight: function (element) {
            $(element).closest('.form-control').removeClass('is-invalid');
        }
    });
});


function refreshTable() {
    $("#employeeData").DataTable().ajax.reload();
}
function InsertData() {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.NIK = $("#inputNIK").val();
    obj.FirstName = $("#inputFirstName").val();
    obj.LastName = $("#inputLastName").val();
    obj.Email = $("#inputEmail").val();
    obj.Phone = $("#inputPhone").val();
    obj.BirthDate = $("#inputBOD").val();
    obj.Salary = Number($("#inputSalary").val());
    obj.Password = $("#inputPassword").val();
    obj.Degree = $("#inputDegree").val();
    obj.GPA = $("#inputGPA").val();
    obj.UniversityId = Number($("#inputUniversity").val());
    obj.Gender = $('input[name="genderOptions"]:checked').val();
    console.log(obj);

    $.ajax({
        // headers: {
        //     'Accept': 'application/json',
        //     'Content-Type': 'application/json'
        // },
        'url': "/Employees/Register",
        'type': "POST",
        'data': {entity:obj},
        'dataType': 'json'
    }).done((result) => {
        Swal.fire(
            'Success',
            'Registrasi brhasil',
            'success'
        );
        $('#modalRegister').modal('toggle');
        $("#employeeData").DataTable().ajax.reload();
    }).fail((error) => {
        Swal.fire(
            'Error',
            'Terjadi kesalahan',
            'error'
        );
    })

    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
}

function submitEmployee() {
    var ini = $("#register").valid();
    var isedit = $("#isEdit").val()
    console.log(ini);
    console.log(isedit);
    if (ini === true) {
        if (isedit === "") {
            console.log("masuk ke insert biasa")
            InsertData();   
        }
        else if (isedit === 'true') {
            console.log("masuk ke put")
            PutData();

        }
    }
    else {
        Swal.fire(
            'Error',
            'Terjadi kesalahan',
            'error'
        );
    }
    console.log("kagak masuk kemana mana")
}

function prefillEmployee(nikdata) {
    $.ajax({
        url: "https://localhost:44393/API/Employees/Profile/"+nikdata,
        success: function (result) {
            console.log(result);
            $("#inputNIK").val(result[0].nik);
            $("#inputFirstName").val(result[0].firstname);
            $("#inputLastName").val(result[0].lastname);
            $("#inputEmail").val(result[0].email);
            $("#inputPhone").val(result[0].phone);
            $("#inputBOD").val(result[0].birthdate.substr(0,10));
            $("#inputSalary").val(result[0].salary);
            $("#inputPassword").val("");
            $("#inputDegree").val(result[0].degree);
            $("#inputGPA").val(result[0].gpa);
            $("#inputUniversity").val();
            $("input[name='genderOptions'][value=" + result[0].gender + "]").prop('checked', true);
            $("#inputNIK").prop("readonly", true);
            $('#isEdit').val(true);
        }
    })
}

function clearEmployee() {
    $("#inputNIK").val("");
    $("#inputFirstName").val("");
    $("#inputLastName").val("");
    $("#inputEmail").val("");
    $("#inputPhone").val("");
    $("#inputBOD").val("");
    $("#inputSalary").val("");
    $("#inputPassword").val("");
    $("#inputDegree").val("");
    $("#inputGPA").val("");
    $("#inputUniversity").val();
    $("input[name='genderOptions']").prop("checked", false);
    $("#inputNIK").prop("readonly", false);
    $('#isEdit').val("");
}

function deleteEmployee(nik) {
    Swal.fire({
        title: 'Do you really want to delete this data?',
        showCancelButton: true,
        confirmButtonText: 'Delete',
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                'url': "https://localhost:44393/API/Employees/" + nik,
                'type': "DELETE"
            }).done((result) => {
                Swal.fire(
                    'Success',
                    'Data brhasil dihapus',
                    'success'
                );
                $("#employeeData").DataTable().ajax.reload();
            }).fail((error) => {
                Swal.fire(
                    'Error',
                    'Terjadi kesalahan',
                    'error'
                );
            })
        } else if (result.isDenied) {

        }
    })
}

function PutData() {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.NIK = $("#inputNIK").val();
    obj.FirstName = $("#inputFirstName").val();
    obj.LastName = $("#inputLastName").val();
    obj.Email = $("#inputEmail").val();
    obj.Phone = $("#inputPhone").val();
    obj.BirthDate = $("#inputBOD").val();
    obj.Salary = Number($("#inputSalary").val());
    obj.Password = $("#inputPassword").val();
    obj.Degree = $("#inputDegree").val();
    obj.GPA = $("#inputGPA").val();
    obj.UniversityId = Number($("#inputUniversity").val());
    obj.Gender = $('input[name="genderOptions"]').val();
    console.log(obj);

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        'url': "https://localhost:44393/API/Employees/Register/",
        'type': "PUT",
        'data': JSON.stringify(obj),
        'dataType': 'json'
    }).done((result) => {
        Swal.fire(
            'Success',
            'Edit brhasil',
            'success'
        );
        $('#modalRegister').modal('toggle');
        $("#employeeData").DataTable().ajax.reload();
    }).fail((error) => {
        Swal.fire(
            'Error',
            'Terjadi kesalahan',
            'error'
        );
    })

    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
}


////login
//function Login() {
//    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
//    //ini ngambil value dari tiap inputan di form nya
//    obj.Email = $("#loginEmail").val();
//    obj.Password = $("#loginPassword").val();
//    $.ajax({
//        //headers: {
//        //    'Accept': 'application/json',
//        //    'Content-Type': 'application/json'
//        //},
//        'url': "/Employees/Auth",
//        'type': "POST",
//        'data': { login: obj },
//        'dataType': 'json'
//    }).done((result) => {
//        Swal.fire(
//            'Success',
//            'Edit brhasil',
//            'success'
//        );
//    }).fail((error) => {
//        Swal.fire(
//            'Error',
//            'terjadi kesalahan',
//            'error'
//        );
//    })
//}
