
$("#loginform").validate({
    rules: {
        email: "required",
        password: "required",
     
    },
    messages: {
        namelogin: { required: "Mohon masukan Email yang Benar"},
        password: {
            required: "Please provide a password"
        }
    },
    errorPlacement: function (error, element) {
    },
    highlight: function (element) {
        $(element).closest('.form-control').addClass('is-invalid');
        $(element).closest('.form-group').addClass('is-invalid');     
    },
    unhighlight: function (element) {
        $(element).closest('.form-control').removeClass('is-invalid');
        $(element).closest('.form-group').removeClass('is-invalid');
    }
}); 

function validasiLogin() {
    var ini = $("#loginform").valid();
    console.log(ini);

    if (ini === true) {
        Login();
    }
};

function Login() {
    var obj = new Object();
    obj.email = $('#namelogin').val();
    obj.password = $('#passwordlogin').val();
    console.log("==cek data==");
    console.log(obj);

    $.ajax({
        url: "https://localhost:44319/API/Employees/Login",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: 'POST',
        data: JSON.stringify(obj),
        dataType: 'JSON'
    }).done((result) => {
        console.log('==data result==');
        console.log(result);
        Swal.fire(
            'Good job!',
            'Login Berhasil!',
            'success'
        );

        document.getElementById("submitForm").click();
     
    }).fail((error) => {
        console.log('==data error==');
        console.log(error);
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Email/Password Salah !',
            footer: '<a href="">Why do I have this issue?</a>'
        });
    });
}
