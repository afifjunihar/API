//chart
$(document).ready(function () {
    // var options = {
    //     chart: {
    //         type: 'bar'
    //     },
    //     series: [{
    //         name: 'sales',
    //         data: [30, 40, 45, 50, 49, 60, 70, 91, 125]
    //     }],
    //     xaxis: {
    //         categories: [1991, 1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999]
    //     }
    // }

    // var chartStatic = new ApexCharts(document.querySelector("#chart"), options);

    // chartStatic.render();

    // // chart bar dynamic
    // var options = {
    //     chart: {
    //         type: 'bar'
    //     },
    //     plotOptions: {
    //         bar: {
    //             distributed: true
    //         }
    //     },
    //     colors: ['#546E7A', '#d4526e'
    //     ],
    //     dataLabels: {
    //         enabled: false
    //     },
    //     series: [],
    //     title: {
    //         text: 'Ajax Example',
    //     },
    //     xaxis: {
    //         categories: ["Male", "Female"]
    //     },
    //     noData: {
    //         text: 'Loading...'
    //     }
    // }

    // var chartBar = new ApexCharts(
    //     document.querySelector("#chartGender"),
    //     options
    // );

    // chartBar.render();

    // var url = 'https://localhost:44393/API/Employees/Gender';

    // $.getJSON(url, function (response) {
    //     chartBar.updateSeries([{
    //         name: 'Gender',
    //         data: response.x
    //     }])
    // });
    
    //chart donut
    var url = 'https://localhost:44393/API/Employees/';
    let gender = [];
    $.getJSON(url, function (response) {
        var male = 0;
        var female = 0;
        response.result.forEach(element => {
            if (element.gender == "Male") {
                male += 1;
            }
            else if (element.gender == "Female") {
                female += 1;
            }
        });
        gender.push(male);
        gender.push(female);
        // console.log("ini chart donuts")
        // console.log(gender);
        // console.log(typeof(gender));
        // console.log("ini abis gender");
        // chartDonut.updateSeries([{
        //     name: 'DonutGender',
        //     data: gender
        // }]);
    });
        console.log("ini chart donuts")
        console.log(gender);
        console.log(typeof(gender));

    var options = {
        series: gender,
        chart: {
        type: 'donut',
      },
      noData: {
        text: 'Loading...'
      },
      labels: ["Male", "Female"]
      };

      var chartDonut = new ApexCharts(document.querySelector("#chartGender"), options);
      chartDonut.render();


    //coba syauqi
    let dataSeries = [];
    let dataLabel = [];

    var dataProp = $.ajax({
    type: 'GET',
    url: 'https://localhost:44393/API/Employees/chart',
    success: function (data) {
        $.each(data, function (index, value) {
            dataSeries.push(value.count);
            dataLabel.push(value.degree);
		})
	}
    })

    // ApexChart
    var options = {
    chart: {
        type: 'pie'
    },
    series: dataSeries,
    labels: dataLabel
    }

    var chartGede = new ApexCharts(document.querySelector("#chartGenderDonut"), options);
    chartGede.render();
});