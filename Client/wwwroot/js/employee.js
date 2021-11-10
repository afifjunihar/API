$(document).ready(function () {
    $('#dataTable1').DataTable({
        'ajax': {
            'url': ''
        },
        'columns': [
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
            }
        ]
    });
});