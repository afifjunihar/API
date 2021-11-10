// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/*const { type } = require("jquery");*/

// Write your JavaScript code.
/*var a = document.getElementById("b2");
a.addEventListener("click", function () {
    document.getElementById("c").style.backgroundColor = "orchid";
    document.getElementById("a").innerHTML = "Coba AddEventListener!!!!";
    document.getElementById("b").style.fontStyle = "oblique";
    document.querySelector("body").style.backgroundColor = "lightblue"
});*/

$("#b1").click(function () {
    alert("Test JQuery!!!!!");
});

/*const animals = [
    { name: 'Nemo', species: 'fish', class: { name: 'invertebrata' } },
    { name: 'Simba', species: 'Cat', class: { name: 'Mamalia' } },
    { name: 'Dory', species: 'fish', class: { name: 'invertebrata' } },
    { name: 'Panther', species: 'Cat', class: { name: 'Mamalia' } },
    { name: 'Budi', species: 'Cat', class: { name: 'Mamalia' } }
]

for (let i = 0; i < animals.length; i++) {
    if (animals[i].species == 'fish') {
        animals[i].class.name = 'non mamalia';
    }
}
console.log(animals);

animals.forEach(fish);
function fish() {
    if (animals.species == 'fish') {
        animals.class.name = 'non mamalia';
    }
}
console.log(animals);

const onlyCat = [];
for (let i = 0; i < animals.length; i++) {
    if (animals[i].species == 'Cat') {
        onlyCat.push(animals[i]);
    }
}
console.log(onlyCat);*/


$.ajax({
    url: "https://swapi.dev/api/people/",
    success: function (result) {
        console.log(result.results);
        var listStarWars = "";
        $.each(result.results, function (key, val) {
            listStarWars += `<tr>
                              <th scope="row">${key+1}</th>
                              <td>${val.name}</td>
                              <td>${val.height}</td>
                              <td>${val.hair_color}</td>
                              <td>${val.skin_color}</td>
                              <td>${val.birth_year}</td>
                            </tr>`
        });
        $("#tableStar").html(listStarWars);
    }
})

$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/",
    success: function (result) {
        console.log(result.results);
        var listPoke = "";
        $.each(result.results, function (key, val) {
            listPoke += `<tr>
                              <th scope="row">${key + 1}</th>
                              <td>${val.name}</td>

                              <td><button type="button" class="btn btn-primary" data-toggle="modal" onclick="getPoke('${val.url}')" data-target="#modalPoke">
                                 Detail
                              </button></td>`
        });
        $("#tablePoke").html(listPoke);
    }
})

function getPoke(url) {
    console.log(url);
    $.ajax({
        url: url,
        success: function (result) {
            var namaPoke = "";
            var ability = "";
            var type = "";
            for (var i = 0; i < result.abilities.length; i++) {
                if (result.abilities[i].ability.name == "overgrow" || result.abilities[i].ability.name == "tinted-lens" || result.abilities[i].ability.name == "keen-eyes" || result.abilities[i].ability.name == "guts") {
                    ability += ` <span class="badge badge-pill badge-info">${result.abilities[i].ability.name}</span>  `
                }
                else if (result.abilities[i].ability.name == "chlorophyll" || result.abilities[i].ability.name == "hustle" || result.abilities[i].ability.name == "rain-dish") {
                    ability += ` <span class="badge badge-pill badge-success">${result.abilities[i].ability.name}</span> `
                }
                else if (result.abilities[i].ability.name == "blaze") {
                    ability += ` <span class="badge badge-pill badge-danger">${result.abilities[i].ability.name}</span> `
                }
                else if (result.abilities[i].ability.name == "solar-power" || result.abilities[i].ability.name == "shield-dust") {
                    ability += ` <span class="badge badge-pill badge-warning">${result.abilities[i].ability.name}</span> `
                }
                else if (result.abilities[i].ability.name == "torrent" || result.abilities[i].ability.name == "big-pecks") {
                    ability += ` <span class="badge badge-pill badge-primary">${result.abilities[i].ability.name}</span> `
                }
                else if (result.abilities[i].ability.name == "run-away" || result.abilities[i].ability.name == "swarm" || result.abilities[i].ability.name == "snipper") {
                    ability += ` <span class="badge badge-pill badge-dark">${result.abilities[i].ability.name}</span> `
                }
                else if (result.abilities[i].ability.name == "shed-skin" || result.abilities[i].ability.name == "compound-eyes") {
                    ability += ` <span class="badge badge-pill badge-secondary">${result.abilities[i].ability.name}</span> `
                }
                else if (result.abilities[i].ability.name == "tangled-feet") {
                    ability += ` <span class="badge badge-pill badge-light">${result.abilities[i].ability.name}</span> `
                }
            }
            for (var i = 0; i < result.types.length; i++) {
                if (result.types[i].type.name == "normal") {
                    type += ` <span class="badge badge-info">Normal</span>`
                }
                else if (result.types[i].type.name == "flying") {
                    type += ` <span class="badge badge-pill badge-secondary">Flying</span>`
                }
                else if (result.types[i].type.name == "bug") {
                    type += ` <span class="badge badge-pill badge-dark">Bug</span>`
                }
                else if (result.types[i].type.name == "poison") {
                    type += ` <span class="badge badge-pill badge-warning">Poison</span>`
                }
                else if (result.types[i].type.name == "water") {
                    type += ` <span class="badge badge-pill badge-primary">Water</span>`
                }
                else if (result.types[i].type.name == "fire") {
                    type += ` <span class="badge badge-pill badge-danger">Fire</span>`
                }
                else if (result.types[i].type.name == "grass") {
                    type += ` <span class="badge badge-pill badge-success">Grass</span>`
                }
            }
            namaPoke = `<img src="${result.sprites.other.dream_world.front_default}" id="poke">
                        <ul class="list-unstyled">
                            <li><strong>Name    :</strong> ${result.name}</li>
                            <li><strong>Height  :</strong> ${result.height}</li>
                            <li><strong>Weight  :</strong> ${result.weight}</li>
                            <li><strong>Abilities :</strong> ${ability}</li>
                            <li><strong>Types     :</strong> ${type}</li>
                        </ul>`
            $(".modal-body").html(namaPoke);
        }
    })
}

$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/1",
    success: function (result) {
        console.log(result);
    }
})

$(document).ready(function () {
    $('#dataTable').DataTable({
        'ajax': {
            'url': 'https://localhost:44349/API/Employees',
            'dataSrc': 'result'
        },
        'dom': '<lf<t>ip>',
        'buttons': [
            {
                extend: 'excelHtml5',
                name: 'excel',
                title: 'Employee',
                sheetName: 'Employee',
                text: '',
                className: 'buttonHide btn-default ',
                filename: 'Data',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            }
            
        ],
        'columnDefs': [{
            'targets': [0,8],
            'orderable': false
        }],
        'columns': [
            {
                'data': null,
                'render': function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            {
                'data': "nik"
            },
            {
                'data': "",
                'render': function (data, type, row, meta) {
                    return row['firstName'] + ' ' + row['lastName'];
                }
            },
            {
                'data': '',
                'render': function (data, type, row, meta) {
                    if (row['phone'].substr(0, 1) == '0') {
                        return '+62' + row['phone'].substr(1);
                    }
                    else {
                        return '+62' + row['phone'];
                    }
                }
            },
            {
                'data': '',
                'render': function (data, type, row, meta) {
                    var date = row['birthDate'].substr(0, 10);
                    var newDate = date.split('-');
                    return newDate[2] + '-' + newDate[1] + '-' + newDate[0];
                }
            },
            {
                'data': '',
                'render': function (data, type, row, meta) {
                    return 'Rp. ' + row['salary'].toLocaleString();
                }
            },
            {
                'data': 'email'
            },
            {
                'data': 'gender'
            },
            {
                'data': null,
                'render': function (data, type, row) {
                    return `<button type="button" class="btn btn-info" data-toggle="tooltip" data-placement="left" title="Update" onclick="getEmployee('${row['nik']}')"><i class="fas fa-edit"></i></button>
                            <button type="button" class="btn btn-danger" data-toggle="tooltip" data-placement="left" title="Delete" onclick="deleteEmployee('${row['nik']}')"><i class="fas fa-trash-alt"></i></button>`;
                }
            }
        ]
    });

    $("#update").validate({
        rules: {
            nik: {
                required: true
            },
            firstName: {
                required: true
            },
            lastName: {
                required: true
            },
            phone: {
                required: true
            },
            birthDate: {
                required: true
            },
            salary: {
                required: true
            },
            email: {
                required: true,
                email: true
            },
            gender: {
                required: true,
            }
        },
        errorPlacement: function (error, element) { },
        highlight: function (element) {
            $(element).closest('.form-control').addClass('is-invalid');
        },
        unhighlight: function (element) {
            $(element).closest('.form-control').removeClass('is-invalid');
        }
    });

    $("#register").validate({
        rules: {
            nik: {
                required: true
            },
            firstName: {
                required: true
            },
            lastName: {
                required: true
            },
            phone: {
                required: true
            },
            birthDate: {
                required: true
            },
            salary: {
                required: true
            },
            email: {
                required: true,
                check_email: true
            },
            password: {
                required: true,
                strong_password: true
            },
            degree: {
                required: true
            },
            gpa: {
                required: true
            },
            univId: {
                required: true
            },
            roleId: {
                required: true
            },
        },
        errorPlacement: function (error, element) { },
        highlight: function (element) {
            $(element).closest('.form-control').addClass('is-invalid');
        },
        unhighlight: function (element) {
            $(element).closest('.form-control').removeClass('is-invalid');
        }
    });

    $.validator.addMethod("check_email", function (value, element) {
        let email = value;
        if (!(/^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/.test(email))) {
            return false;
        }
        return true;
    }, function (value, element) {
        let email = $(element).val();
        if (!(/^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/.test(email))) {
            return "Please enter your valid email."
        }
        return false;
    });

    $.validator.addMethod("strong_password", function (value, element) {
        let password = value;
        if (!(/^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%&*!])(.{8,20}$)/.test(password))) {
            return false;
        }
        return true;
    }, function (value, element) {
        let password = $(element).val();
        if (!(/^(.{8,20}$)/.test(password))) {
            return 'Password must be between 8 and 20 characters long.';
        }
        else if (!(/^(?=.*[A-Z])/.test(password))) {
            return 'Password must contain atleast one uppercase.';
        }
        else if (!(/^(?=.*[a-z])/.test(password))) {
            return 'Password must contain atleast one lowercase.';
        }
        else if (!(/^(?=.*[0-9])/.test(password))) {
            return 'Password must contain atleast one digit.';
        }
        else if (!(/^(?=.*[@#$%&*!])/.test(password))) {
            return "Password must contain special characters from @#$%&*!.";
        }
        return false;
    });
});

function Validate() {
    var ini = $("#register").valid();
    console.log(ini);

    if (ini === true) {
        insertData();
    }
    else {
        Swal.fire(
            'Failed!',
            'Please enter all fields.',
            'error'
        );
    }
};

function ValidateUpdate(id) {
    var ini = $("#update").valid();
    console.log(ini);

    if (ini === true) {
        update(id);
    }
    else {
        Swal.fire(
            'Failed!',
            'Please enter all fields.',
            'error'
        );
    }
};

$.ajax({
    url: "https://localhost:44349/API/Universities",
    success: function (result) {
        console.log(result);
        var namaUniv = "";
        $.each(result.result, function (key, val) {
            namaUniv += `<option value="${val.id}">${val.name}</option>`
        });
        $("#inputUnivId").html(namaUniv);
    }
})

$.ajax({
    url: "https://localhost:44349/API/Roles",
    success: function (result) {
        console.log(result);
        var namaRole = "";
        $.each(result.result, function (key, val) {
            namaRole += `<option value="${val.id}">${val.roleName}</option>`
        });
        $("#inputRoleId").html(namaRole);
    }
})

function clearTextBox() {
    $('#inputNIK').val("");
    $('#inputFirstName').val("");
    $('#inputLastName').val("");
    $('#inputPhone').val("");
    $('#inputBirthDate').val("");
    $('#inputSalary').val("");
    $('#inputEmail').val("");
    $('#inputPassword').val("");
    $('#inputDegree').val("");
    $('#inputGPA').val("");/*
    $('#inputUnivId').val("");
    $('#inputRoleId').val("");*/
    $('#inputNIK').css('border-color', 'lightgrey');
    $('#inputFirstName').css('border-color', 'lightgrey');
    $('#inputLastName').css('border-color', 'lightgrey');
    $('#inputPhone').css('border-color', 'lightgrey');
    $('#inputBirthDate').css('border-color', 'lightgrey');
    $('#inputSalary').css('border-color', 'lightgrey');
    $('#inputEmail').css('border-color', 'lightgrey');
    $('#inputPassword').css('border-color', 'lightgrey');
    $('#inputDegree').css('border-color', 'lightgrey');
    $('#inputGPA').css('border-color', 'lightgrey');
    $('#inputUnivId').css('border-color', 'lightgrey');
    $('#inputRoleId').css('border-color', 'lightgrey');
}

function registerButton() {
    $('#registerEmployee').modal('show');
}

function exportExcel() {
    $('#dataTable').DataTable().buttons('excel:name').trigger();
}

function insertData() {
    var obj = new Object();

    obj.NIK = $('#inputNIK').val();
    obj.FirstName = $('#inputFirstName').val();
    obj.LastName = $('#inputLastName').val();
    obj.Phone = $('#inputPhone').val();
    obj.BirthDate = $('#inputBirthDate').val();
    obj.Salary = parseInt($('#inputSalary').val());
    obj.Email = $('#inputEmail').val();
    obj.Password = $('#inputPassword').val();
    obj.Degree = $('#inputDegree').val();
    obj.GPA = $('#inputGPA').val();
    obj.UniversityId = parseInt($('#inputUnivId').val());
    obj.RoleId = parseInt($('#inputRoleId').val());

    console.log(obj);
    $.ajax({
        url: "https://localhost:44349/API/Employees/Register",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: "POST",
        data: JSON.stringify(obj),
        dataType: 'json'
    }).done((result) => {
        Swal.fire(
            'Added!',
            'Your file has been added.',
            'success'
        );
        $('#dataTable').DataTable().ajax.reload();
        $('#registerEmployee').modal('hide');
        clearTextBox();
    }).fail((error) => {
        if (error.responseJSON.message == 'NIK sudah tersedia') {
            Swal.fire(
                'Failed!',
                'Your NIK has been added before.',
                'error'
            );
        } else if (error.responseJSON.message == 'Email sudah terdaftar') {
            Swal.fire(
                'Failed!',
                'Your Email has been added before.',
                'error'
            );
        } else if (error.responseJSON.message == 'Nomor telepon sudah terdaftar') {
            Swal.fire(
                'Failed!',
                'Your Phone has been added before.',
                'error'
            );
        } else {
            Swal.fire(
                'Failed!',
                'Your file has been fail to added.',
                'error'
            );
        }
        console.log(error);
    });
}

function getEmployee(id) {
    console.log(id);
    $.ajax({
        url: "https://localhost:44349/API/Employees/" + id,
        success: function (result) {
            console.log(result);
            $('#updateEmployee').modal('show');
            $('#nik').val(result.nik);
            $('#firstName').val(result.firstName);
            $('#lastName').val(result.lastName);
            $('#lastName').val(result.lastName);
            $('#phone').val(result.phone);
            var date = result.birthDate.substr(0, 10);
            $('#birthDate').val(date);
            $('#salary').val(result.salary);
            $('#email').val(result.email);
            if (result.gender === 'Male') {
                $('#gender').val(0);
            } else {
                $('#gender').val(1);
            }
        }
    })
}

function update(id) {
    console.log(id);
    var obj = new Object();

    obj.nik = id;
    obj.firstName = $('#firstName').val();
    obj.lastName = $('#lastName').val();
    obj.phone = $('#phone').val();
    obj.birthDate = $('#birthDate').val();
    obj.salary = parseInt($('#salary').val());
    obj.email = $('#email').val();
    obj.gender = parseInt($('#gender').val());

    console.log(obj);
    $.ajax({
        url: "https://localhost:44349/API/Employees/" + id,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: 'json'
    }).done((result) => {
        console.log(result);
        if (result.status == 200) {
            Swal.fire(
                'Updated!',
                'Your file has been updated.',
                'success'
            )
            $('#dataTable').DataTable().ajax.reload();
            $('#updateEmployee').modal('hide');
        }
    }).fail((error) => {
        console.log(error);
        Swal.fire(
            'Failed!',
            'Your file has been fail to updated.',
            'error'
        );
    })
}

function deleteEmployee(id) {
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
            $.ajax({
                url: "https://localhost:44349/API/Employees/" + id,
                type: "Delete"
            }).then((result) => {
                if (result.status == 200) {
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    )
                    $('#tableEmployee').DataTable().ajax.reload();
                }
            })
        }
    })
}




